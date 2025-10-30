using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapGames.Infra.Migrations
{
    /// <inheritdoc />
    public partial class usuarioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    SENHA = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DT_ALTERACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    TOKEN = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ROLE = table.Column<int>(type: "int", nullable: false),
                    ULTIMO_ACESSO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_Usuario",
                        column: x => x.ID_USUARIO,
                        principalTable: "TB_USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_IDENTITY_ID_USUARIO",
                table: "TB_IDENTITY",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_IDENTITY");

            migrationBuilder.DropTable(
                name: "TB_USUARIO");
        }
    }
}
