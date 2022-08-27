using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Stages_Stage_Id1",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Matrials_Matrial_Id",
                table: "StudentPayments");

            migrationBuilder.DropIndex(
                name: "IX_Group_Stage_Id1",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Stage_Id",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Stage_Id1",
                table: "Group");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 27, 3, 29, 38, 870, DateTimeKind.Local).AddTicks(5361),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 25, 18, 27, 32, 86, DateTimeKind.Local).AddTicks(9927));

            migrationBuilder.AlterColumn<int>(
                name: "Matrial_Id",
                table: "StudentPayments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Group_Id",
                table: "StudentPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StudentAttends",
                columns: table => new
                {
                    Attend_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<int>(type: "int", nullable: true),
                    Stage_Id = table.Column<int>(type: "int", nullable: true),
                    AttendDate = table.Column<DateTime>(type: "datetime2", maxLength: 40, nullable: false),
                    IsAttend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttends", x => x.Attend_Id);
                    table.ForeignKey(
                        name: "FK_StudentAttends_Stages_Stage_Id",
                        column: x => x.Stage_Id,
                        principalTable: "Stages",
                        principalColumn: "Stage_Id");
                    table.ForeignKey(
                        name: "FK_StudentAttends_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Student_Id");
                });

            migrationBuilder.CreateTable(
                name: "Stuff",
                columns: table => new
                {
                    Stuff_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stuff_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stuff_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stuff_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stuff_Specilist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stuff_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stuff_BirthOfDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stuff", x => x.Stuff_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_Group_Id",
                table: "StudentPayments",
                column: "Group_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttends_Stage_Id",
                table: "StudentAttends",
                column: "Stage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttends_Student_Id",
                table: "StudentAttends",
                column: "Student_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Group_Group_Id",
                table: "StudentPayments",
                column: "Group_Id",
                principalTable: "Group",
                principalColumn: "Group_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Matrials_Matrial_Id",
                table: "StudentPayments",
                column: "Matrial_Id",
                principalTable: "Matrials",
                principalColumn: "Matrial_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Group_Group_Id",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Matrials_Matrial_Id",
                table: "StudentPayments");

            migrationBuilder.DropTable(
                name: "StudentAttends");

            migrationBuilder.DropTable(
                name: "Stuff");

            migrationBuilder.DropIndex(
                name: "IX_StudentPayments_Group_Id",
                table: "StudentPayments");

            migrationBuilder.DropColumn(
                name: "Group_Id",
                table: "StudentPayments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 25, 18, 27, 32, 86, DateTimeKind.Local).AddTicks(9927),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 27, 3, 29, 38, 870, DateTimeKind.Local).AddTicks(5361));

            migrationBuilder.AlterColumn<int>(
                name: "Matrial_Id",
                table: "StudentPayments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stage_Id",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stage_Id1",
                table: "Group",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_Stage_Id1",
                table: "Group",
                column: "Stage_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Stages_Stage_Id1",
                table: "Group",
                column: "Stage_Id1",
                principalTable: "Stages",
                principalColumn: "Stage_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Matrials_Matrial_Id",
                table: "StudentPayments",
                column: "Matrial_Id",
                principalTable: "Matrials",
                principalColumn: "Matrial_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
