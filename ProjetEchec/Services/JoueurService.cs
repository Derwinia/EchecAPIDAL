using ProjetEchec.Commands;
using ProjetEchec.DTO;
using ProjetEchecDAL;
using ProjetEchecDAL.Entities;
using System.ComponentModel.DataAnnotations;
using ProjetEchecDAL.Enum;

namespace ProjetEchec.Services
{
    public class JoueurService
    {
        private readonly EchecContext _echecContext;

        public JoueurService(EchecContext echecContext)
        {
            _echecContext = echecContext;
        }

        public IEnumerable<JoueurDTO> Get()
        {
            IEnumerable<JoueurDTO> result = _echecContext.Joueurs.Select(t => new JoueurDTO
            {
                Id = t.Id,
                Pseudo = t.Pseudo,
                Email = t.Email,
                Password = t.Password,
                Birthday = t.Birthday,
                Genre = t.Genre,
                Elo = t.Elo,
        }).Take(10);
            return result;
        }

        public JoueurDTO? GetById(Guid id)
        {
            Joueur? result = _echecContext.Joueurs.SingleOrDefault(t => t.Id == id);
            if(result is null)
                return null;

            return new JoueurDTO()
            {
                Id = result.Id,
                Pseudo = result.Pseudo,
                Email = result.Email,
                Password = result.Password,
                Birthday = result.Birthday,
                Genre = result.Genre,
                Elo = result.Elo,
            };
        }

        public JoueurDTO? Login(string pseudo, string password)
        {
            JoueurDTO? result = _echecContext.Joueurs.Where(u => u.Pseudo== pseudo && u.Password == password).Select(t => new JoueurDTO
            {
                Id = t.Id,
                Pseudo = t.Pseudo,
                Email = t.Email,
                Password = t.Password,
                Birthday = t.Birthday,
                Genre = t.Genre,
                Elo = t.Elo,
                Droit = t.Droit,
            }).FirstOrDefault();
            return result;
        }

        public void Add(AddJoueurCommand cmd)
        {
            #region VerificationDesDonnees
            if (_echecContext.Joueurs.Any(entry => entry.Pseudo == cmd.Pseudo))
            {
                throw new ValidationException("Ce Pseudo est déja utilisé par un autre utilisateur");
            }
            if (_echecContext.Joueurs.Any(entry => entry.Email == cmd.Email))
            {
                throw new ValidationException("Cette adresse mail est déja enregistrer");
            }
            #endregion
            Joueur nouveauJoueur = new Joueur
            {
                Id = Guid.NewGuid(),
                Pseudo = cmd.Pseudo,
                Email = cmd.Email,
                Password = cmd.Password,
                Birthday = cmd.Birthday,
                Genre = cmd.Genre,
                Elo = cmd.Elo??1200,
                Droit = 0
            };
            _echecContext.Add(nouveauJoueur);
            _echecContext.SaveChanges();
        }

        //public void Remove(Guid id)
        //{
        //    Todo? aSupprimer = _todoContext.Todos.FirstOrDefault(tache => tache.Id == id);
        //    if (aSupprimer == null)
        //    {
        //        throw new KeyNotFoundException();
        //    }
        //    _todoContext.Remove(aSupprimer);
        //    _todoContext.SaveChanges();
        //}
    }
}
