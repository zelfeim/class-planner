using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToEventAndChangeLengthToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Event",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.DropColumn("Length", "Classes");
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Title", table: "Event");

            migrationBuilder.DropColumn("Length", "Classes");
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Length",
                table: "Classes",
                type: "interval",
                nullable: false
            );
        }
    }
}
