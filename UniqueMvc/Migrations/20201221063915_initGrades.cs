using Microsoft.EntityFrameworkCore.Migrations;

namespace UniqueMvc.Migrations
{
    public partial class initGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizOrAssignments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Grade = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizOrAssignments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TermGrades",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(nullable: true),
                    Grade = table.Column<double>(nullable: false),
                    Quiz1ID = table.Column<int>(nullable: true),
                    Quiz2ID = table.Column<int>(nullable: true),
                    Quiz3ID = table.Column<int>(nullable: true),
                    Assignment1ID = table.Column<int>(nullable: true),
                    Assignment2ID = table.Column<int>(nullable: true),
                    Assignment3ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermGrades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TermGrades_QuizOrAssignments_Assignment1ID",
                        column: x => x.Assignment1ID,
                        principalTable: "QuizOrAssignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermGrades_QuizOrAssignments_Assignment2ID",
                        column: x => x.Assignment2ID,
                        principalTable: "QuizOrAssignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermGrades_QuizOrAssignments_Assignment3ID",
                        column: x => x.Assignment3ID,
                        principalTable: "QuizOrAssignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermGrades_QuizOrAssignments_Quiz1ID",
                        column: x => x.Quiz1ID,
                        principalTable: "QuizOrAssignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermGrades_QuizOrAssignments_Quiz2ID",
                        column: x => x.Quiz2ID,
                        principalTable: "QuizOrAssignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermGrades_QuizOrAssignments_Quiz3ID",
                        column: x => x.Quiz3ID,
                        principalTable: "QuizOrAssignments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserID = table.Column<string>(nullable: true),
                    SubjectGrade = table.Column<double>(nullable: false),
                    PrelimID = table.Column<int>(nullable: true),
                    MidtermID = table.Column<int>(nullable: true),
                    PrefinalID = table.Column<int>(nullable: true),
                    FinalID = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grades_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_TermGrades_FinalID",
                        column: x => x.FinalID,
                        principalTable: "TermGrades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_TermGrades_MidtermID",
                        column: x => x.MidtermID,
                        principalTable: "TermGrades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_TermGrades_PrefinalID",
                        column: x => x.PrefinalID,
                        principalTable: "TermGrades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_TermGrades_PrelimID",
                        column: x => x.PrelimID,
                        principalTable: "TermGrades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16446968-4cef-46d6-83c5-d3a00de4f250",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c96de3c-3f99-4668-b5e9-13c8ce6bb8fc", "AQAAAAEAACcQAAAAEKjaW3Ey6sabsna1ofoe3AWbbN4LyHCWBdlnhI2MeIyUraBbVPz8oTY4KXTfrgj++A==", "7d74ba2c-771e-4497-bebb-4c0fe07d21d1" });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ApplicationUserId",
                table: "Grades",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FinalID",
                table: "Grades",
                column: "FinalID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_MidtermID",
                table: "Grades",
                column: "MidtermID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_PrefinalID",
                table: "Grades",
                column: "PrefinalID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_PrelimID",
                table: "Grades",
                column: "PrelimID");

            migrationBuilder.CreateIndex(
                name: "IX_TermGrades_Assignment1ID",
                table: "TermGrades",
                column: "Assignment1ID");

            migrationBuilder.CreateIndex(
                name: "IX_TermGrades_Assignment2ID",
                table: "TermGrades",
                column: "Assignment2ID");

            migrationBuilder.CreateIndex(
                name: "IX_TermGrades_Assignment3ID",
                table: "TermGrades",
                column: "Assignment3ID");

            migrationBuilder.CreateIndex(
                name: "IX_TermGrades_Quiz1ID",
                table: "TermGrades",
                column: "Quiz1ID");

            migrationBuilder.CreateIndex(
                name: "IX_TermGrades_Quiz2ID",
                table: "TermGrades",
                column: "Quiz2ID");

            migrationBuilder.CreateIndex(
                name: "IX_TermGrades_Quiz3ID",
                table: "TermGrades",
                column: "Quiz3ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "TermGrades");

            migrationBuilder.DropTable(
                name: "QuizOrAssignments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16446968-4cef-46d6-83c5-d3a00de4f250",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6548ad42-dbf0-41ee-b25d-8fd9750657c3", "AQAAAAEAACcQAAAAEPj8NbZrHiPxTgTWqYvfS9Udr8rieJiX6jTg+dlEeRgF1eHBmHs9SzuyE8mwVTfO0w==", "dad6efa6-5f90-4cd6-9abb-34a6992d8e25" });
        }
    }
}
