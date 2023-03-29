using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NG6_R51.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trainers",
                columns: table => new
                {
                    trainerid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    trainername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainers", x => x.trainerid);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    playercode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    playername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trainerid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    traincost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    earned = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    matchdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.playercode);
                    table.ForeignKey(
                        name: "FK_players_trainers_trainerid",
                        column: x => x.trainerid,
                        principalTable: "trainers",
                        principalColumn: "trainerid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_players_trainerid",
                table: "players",
                column: "trainerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "trainers");
        }
    }
}
