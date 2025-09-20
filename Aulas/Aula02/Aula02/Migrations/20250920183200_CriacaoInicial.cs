using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aula02.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDate",
                table: "StudentCourses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignDate",
                table: "StudentCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelDate",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "SignDate",
                table: "StudentCourses");
        }
    }
}
