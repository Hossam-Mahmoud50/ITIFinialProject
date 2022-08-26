using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterApp.Infrasturcture.Migrations
{
    public partial class addStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 23, 16, 45, 51, 760, DateTimeKind.Local).AddTicks(5856),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 13, 1, 29, 39, 464, DateTimeKind.Local).AddTicks(3386));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stuff");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Student_RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 13, 1, 29, 39, 464, DateTimeKind.Local).AddTicks(3386),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 23, 16, 45, 51, 760, DateTimeKind.Local).AddTicks(5856));
        }
    }
}
