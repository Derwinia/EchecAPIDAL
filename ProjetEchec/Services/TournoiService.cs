using Microsoft.EntityFrameworkCore;
using ProjetEchec.Commands;
using ProjetEchec.DTO;
using ProjetEchecDAL;
using ProjetEchecDAL.Entities;
using ProjetEchecDAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProjetEchec.Services
{
    public class TournoiService
    {
        private readonly EchecContext _echecContext;

        public TournoiService(EchecContext echecContext)
        {
            _echecContext = echecContext;
        }

        public IEnumerable<TournoiDTO> Get()
        {
            IEnumerable<TournoiDTO> result = _echecContext.Tournois.Include(t=>t.Categories).Include(t=>t.Joueurs).Select(t => new TournoiDTO
            {
                Id = t.Id,
                Nom = t.Nom,
                Lieu = t.Lieu??"non précisé",
                NbJoueurActuel = t.Joueurs.Count,
                NbJoueurMin = t.MinJoueur,
                NbJoueurMax = t.MaxJoueur,
                EloMin = t.MinElo,
                EloMax = t.MaxElo,
                DateLimiteInscription = formatTime(t.InscriptionLimit),
                Statut = t.Statut,
                Categories = t.Categories.Select(c=>c.Nom).ToArray(),
                femmeOnly = t.FemmeOnly,
                Ronde = t.Ronde,
                Joueurs = t.Joueurs.Select(j => new JoueurDTO
                {
                    Id=j.Id,
                    Pseudo = j.Pseudo,
                    Email = j.Email,
                    Birthday = j.Birthday,
                    Elo = j.Elo,
                    Genre = j.Genre,
                    Droit = j.Droit,
                })
            }).Take(10);
                return result;
        }


        private static double formatTime(DateTime datenontransforme)
        {

            return datenontransforme.Subtract(new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc)).TotalMilliseconds;

        }
        public void Add(AddTournoiCommand cmd)
        {
            #region VerificationDesDonnees
            if (cmd.MinJoueur > cmd.MaxJoueur)
            {
                throw new ValidationException("Le nombre minimum de joueurs est inférieur au nombre maximum de joueurs");
            }
            if (cmd.MinJoueur < 2)
            {
                throw new ValidationException("le nombre de joueurs minimum doit etre égale ou supérieur à 2");
            }
            if (cmd.MaxJoueur > 32)
            {
                throw new ValidationException("le nombre de joueurs maximum doit etre égale ou inférieur à 32");
            }
            if (cmd.MinElo > cmd.MaxElo)
            {
                throw new ValidationException("Le nombre minimum d'Elo est inférieur au nombre maximum d'Elo");
            }
            if (cmd.MinElo < 0)
            {
                throw new ValidationException("L'Elo minimum est de 0");
            }
            if (cmd.MaxElo > 3000)
            {
                throw new ValidationException("L'elo maximum est de 3000");
            }
            if (cmd.InscriptionLimit < DateTime.Now.AddDays(cmd.MinJoueur))
            {
                throw new ValidationException("La date d'inscription est trop courte par rapport au nombre de joueurs minimum");
            }
            #endregion
            Tournoi nouveauTournoi = new Tournoi
            {
                Id = Guid.NewGuid(),
                Nom = cmd.Nom,
                Lieu = cmd.Lieu,
                MinJoueur = cmd.MinJoueur,
                MaxJoueur = cmd.MaxJoueur,
                MinElo = cmd.MinElo,
                MaxElo = cmd.MaxElo,
                Categories = cmd.Categories.Select(name => _echecContext.Categories.FirstOrDefault(c => c.Nom == name)).OfType<Categorie>().ToList(),
                Statut = 0,
                Ronde = 0,
                FemmeOnly = cmd.FemmeOnly,
                InscriptionLimit = cmd.InscriptionLimit,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,

            };
            _echecContext.Add(nouveauTournoi);
            _echecContext.SaveChanges();
        }

        public void AddJoueur(Guid tournoi , Guid joueur)
        {
            Tournoi? tournoiActuel = _echecContext.Tournois.Find(tournoi);
            if(tournoiActuel == null)
            {
                throw new InvalidOperationException("le tournoi existe pas");
            }
            Joueur? joueurActuel = _echecContext.Joueurs.Find(joueur);
            if (joueurActuel == null)
            {
                throw new InvalidOperationException("le joueur existe pas");
            }
            tournoiActuel.Joueurs.Add(joueurActuel);
            _echecContext.SaveChanges();
        }

        public void Remove(Guid id)
        {
            Tournoi? aSupprimer = _echecContext.Tournois.FirstOrDefault(tournoi => tournoi.Id == id);
            if (aSupprimer == null)
            {
                throw new KeyNotFoundException();
            }
            _echecContext.Remove(aSupprimer);
            _echecContext.SaveChanges();
        }
    }
}

