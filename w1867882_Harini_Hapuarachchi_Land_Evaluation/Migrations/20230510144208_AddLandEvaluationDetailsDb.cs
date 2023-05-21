using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Migrations
{
    /// <inheritdoc />
    public partial class AddLandEvaluationDetailsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Lands",
                columns: table => new
                {
                    LandId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluationUp = table.Column<float>(type: "real", nullable: false),
                    EvaluationMis = table.Column<float>(type: "real", nullable: false),
                    EvaluationLow = table.Column<float>(type: "real", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    MeanAnualRF = table.Column<float>(type: "real", nullable: false),
                    SoilDepth = table.Column<float>(type: "real", nullable: false),
                    SoilTexture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StonesAndGrovels = table.Column<float>(type: "real", nullable: false),
                    SoilDrainageClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoilPH = table.Column<float>(type: "real", nullable: false),
                    SlopeAngle = table.Column<float>(type: "real", nullable: false),
                    PastErosion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RockOutcrops = table.Column<float>(type: "real", nullable: false),
                    ClassOfLandUnit = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lands", x => x.LandId);
                    table.ForeignKey(
                        name: "FK_Lands_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prediction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.EvaluationId);
                    table.ForeignKey(
                        name: "FK_Evaluations_Lands_LandId",
                        column: x => x.LandId,
                        principalTable: "Lands",
                        principalColumn: "LandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_LandId",
                table: "Evaluations",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_Lands_UserId",
                table: "Lands",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Lands");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
