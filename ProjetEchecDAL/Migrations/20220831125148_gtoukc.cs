using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetEchecDAL.Migrations
{
    public partial class gtoukc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Joueurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pseudo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elo = table.Column<int>(type: "int", nullable: false),
                    Droit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joueurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournois",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinJoueur = table.Column<int>(type: "int", nullable: false),
                    MaxJoueur = table.Column<int>(type: "int", nullable: false),
                    MinElo = table.Column<int>(type: "int", nullable: false),
                    MaxElo = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ronde = table.Column<int>(type: "int", nullable: false),
                    FemmeOnly = table.Column<bool>(type: "bit", nullable: false),
                    InscriptionLimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournois", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategorieTournoi",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    TournoisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieTournoi", x => new { x.CategoriesId, x.TournoisId });
                    table.ForeignKey(
                        name: "FK_CategorieTournoi_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorieTournoi_Tournois_TournoisId",
                        column: x => x.TournoisId,
                        principalTable: "Tournois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoueurTournoi",
                columns: table => new
                {
                    JoueursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TournoisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoueurTournoi", x => new { x.JoueursId, x.TournoisId });
                    table.ForeignKey(
                        name: "FK_JoueurTournoi_Joueurs_JoueursId",
                        column: x => x.JoueursId,
                        principalTable: "Joueurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoueurTournoi_Tournois_TournoisId",
                        column: x => x.TournoisId,
                        principalTable: "Tournois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorieTournoi_TournoisId",
                table: "CategorieTournoi",
                column: "TournoisId");

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_Email",
                table: "Joueurs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_Pseudo",
                table: "Joueurs",
                column: "Pseudo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JoueurTournoi_TournoisId",
                table: "JoueurTournoi",
                column: "TournoisId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournois_Nom",
                table: "Tournois",
                column: "Nom",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorieTournoi");

            migrationBuilder.DropTable(
                name: "JoueurTournoi");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Joueurs");

            migrationBuilder.DropTable(
                name: "Tournois");
        }
    }
}
