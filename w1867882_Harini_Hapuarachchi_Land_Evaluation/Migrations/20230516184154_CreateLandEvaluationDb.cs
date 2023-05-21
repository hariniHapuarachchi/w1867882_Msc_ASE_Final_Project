using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Migrations
{
    /// <inheritdoc />
    public partial class CreateLandEvaluationDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassOfLandUnit",
                table: "Lands");

            migrationBuilder.DropColumn(
                name: "EvaluationLow",
                table: "Lands");

            migrationBuilder.DropColumn(
                name: "EvaluationMis",
                table: "Lands");

            migrationBuilder.DropColumn(
                name: "EvaluationUp",
                table: "Lands");

            migrationBuilder.DropColumn(
                name: "PastErosion",
                table: "Lands");

            migrationBuilder.DropColumn(
                name: "SlopeAngle",
                table: "Lands");

            migrationBuilder.DropColumn(
                name: "SoilTexture",
                table: "Lands");

            migrationBuilder.DropColumn(
                name: "StonesAndGrovels",
                table: "Lands");

            migrationBuilder.CreateTable(
                name: "CoconutLands",
                columns: table => new
                {
                    CoconutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evaluation = table.Column<float>(type: "real", nullable: false),
                    MeanAnualTemp = table.Column<float>(type: "real", nullable: false),
                    TotalSunshine = table.Column<float>(type: "real", nullable: false),
                    MinimumHumidity = table.Column<float>(type: "real", nullable: false),
                    SoilTexture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterDepth = table.Column<float>(type: "real", nullable: false),
                    Ec = table.Column<float>(type: "real", nullable: false),
                    SlopeAngle = table.Column<float>(type: "real", nullable: false),
                    LandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoconutLands", x => x.CoconutId);
                    table.ForeignKey(
                        name: "FK_CoconutLands_Lands_LandId",
                        column: x => x.LandId,
                        principalTable: "Lands",
                        principalColumn: "LandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RubberLands",
                columns: table => new
                {
                    RubberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evaluation = table.Column<float>(type: "real", nullable: false),
                    MeanAnualTemp = table.Column<float>(type: "real", nullable: false),
                    LandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubberLands", x => x.RubberId);
                    table.ForeignKey(
                        name: "FK_RubberLands_Lands_LandId",
                        column: x => x.LandId,
                        principalTable: "Lands",
                        principalColumn: "LandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeaLands",
                columns: table => new
                {
                    TeaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationUp = table.Column<float>(type: "real", nullable: false),
                    EvaluationMis = table.Column<float>(type: "real", nullable: false),
                    EvaluationLow = table.Column<float>(type: "real", nullable: false),
                    SoilTexture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StonesAndGrovels = table.Column<float>(type: "real", nullable: false),
                    SlopeAngle = table.Column<float>(type: "real", nullable: false),
                    PastErosion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaLands", x => x.TeaId);
                    table.ForeignKey(
                        name: "FK_TeaLands_Lands_LandId",
                        column: x => x.LandId,
                        principalTable: "Lands",
                        principalColumn: "LandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoconutLands_LandId",
                table: "CoconutLands",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_RubberLands_LandId",
                table: "RubberLands",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_TeaLands_LandId",
                table: "TeaLands",
                column: "LandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoconutLands");

            migrationBuilder.DropTable(
                name: "RubberLands");

            migrationBuilder.DropTable(
                name: "TeaLands");

            migrationBuilder.AddColumn<int>(
                name: "ClassOfLandUnit",
                table: "Lands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "EvaluationLow",
                table: "Lands",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EvaluationMis",
                table: "Lands",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EvaluationUp",
                table: "Lands",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "PastErosion",
                table: "Lands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "SlopeAngle",
                table: "Lands",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "SoilTexture",
                table: "Lands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "StonesAndGrovels",
                table: "Lands",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
