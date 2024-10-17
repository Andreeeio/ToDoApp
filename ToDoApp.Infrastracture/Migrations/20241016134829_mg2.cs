using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ConfirmationTokenExpiration",
                table: "Users",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmationTokenExpiration",
                table: "Users");
        }
    }
}
