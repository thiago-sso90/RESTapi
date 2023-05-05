using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovoprojetoApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Data_Inicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Final = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DevEventsSpeaker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TituloPalestra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TiuloDescricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerfilLinkedin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DevEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevEventsSpeaker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevEventsSpeaker_DevEvents_DevEventId",
                        column: x => x.DevEventId,
                        principalTable: "DevEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevEventsSpeaker_DevEventId",
                table: "DevEventsSpeaker",
                column: "DevEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevEventsSpeaker");

            migrationBuilder.DropTable(
                name: "DevEvents");
        }
    }
}
