using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addexaam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 27, 4, 15, 55, 136, DateTimeKind.Local).AddTicks(5771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 27, 3, 29, 38, 870, DateTimeKind.Local).AddTicks(5361));

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Exam_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Exam_Start_Date = table.Column<DateTime>(type: "datetime2", maxLength: 30, nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Group_Id = table.Column<int>(type: "int", nullable: true),
                    Matrial_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Exam_Id);
                    table.ForeignKey(
                        name: "FK_Exams_Group_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "Group",
                        principalColumn: "Group_Id");
                    table.ForeignKey(
                        name: "FK_Exams_Matrials_Matrial_Id",
                        column: x => x.Matrial_Id,
                        principalTable: "Matrials",
                        principalColumn: "Matrial_Id");
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Grade_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade_Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Grade_Point = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    Percentage_From = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Percentage_Upto = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Exam_Id = table.Column<int>(type: "int", nullable: true),
                    Student_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Grade_Id);
                    table.ForeignKey(
                        name: "FK_Grade_Exams_Exam_Id",
                        column: x => x.Exam_Id,
                        principalTable: "Exams",
                        principalColumn: "Exam_Id");
                    table.ForeignKey(
                        name: "FK_Grade_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Student_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_Group_Id",
                table: "Exams",
                column: "Group_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_Matrial_Id",
                table: "Exams",
                column: "Matrial_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Exam_Id",
                table: "Grade",
                column: "Exam_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Student_Id",
                table: "Grade",
                column: "Student_Id",
                unique: true,
                filter: "[Student_Id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 27, 3, 29, 38, 870, DateTimeKind.Local).AddTicks(5361),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 27, 4, 15, 55, 136, DateTimeKind.Local).AddTicks(5771));
        }
    }
}
