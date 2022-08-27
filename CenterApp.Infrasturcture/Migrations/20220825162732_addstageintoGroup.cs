using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addstageintoGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 25, 18, 27, 32, 86, DateTimeKind.Local).AddTicks(9927),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 13, 1, 29, 39, 464, DateTimeKind.Local).AddTicks(3386));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Stages_Stage_Id1",
                table: "Group");

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
                defaultValue: new DateTime(2022, 8, 13, 1, 29, 39, 464, DateTimeKind.Local).AddTicks(3386),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 25, 18, 27, 32, 86, DateTimeKind.Local).AddTicks(9927));
        }
    }
}
