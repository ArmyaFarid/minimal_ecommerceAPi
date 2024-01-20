using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    codearticle = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    numcommande = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    quantite = table.Column<decimal>(type: "numeric(20,3)", precision: 20, scale: 3, nullable: true),
                    prix = table.Column<decimal>(type: "numeric(20,3)", precision: 20, scale: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article", x => x.codearticle);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userid = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    img = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "commande",
                columns: table => new
                {
                    numcommande = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    userid = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    datecommande = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    datelivraison = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    prixttc = table.Column<decimal>(type: "numeric(20,3)", precision: 20, scale: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commande", x => x.numcommande);
                    table.ForeignKey(
                        name: "fk_commande_user",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "commandeArticle",
                columns: table => new
                {
                    numcommande = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    codearticle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commande_article", x => new { x.numcommande, x.codearticle });
                    table.ForeignKey(
                        name: "fk_commande_article_article",
                        column: x => x.codearticle,
                        principalTable: "article",
                        principalColumn: "codearticle");
                    table.ForeignKey(
                        name: "fk_commande_article_commande",
                        column: x => x.numcommande,
                        principalTable: "commande",
                        principalColumn: "numcommande");
                });

            migrationBuilder.CreateIndex(
                name: "IX_commande_userid",
                table: "commande",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_commandeArticle_codearticle",
                table: "commandeArticle",
                column: "codearticle");

            migrationBuilder.CreateIndex(
                name: "user_password_key",
                table: "user",
                column: "password",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commandeArticle");

            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "commande");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
