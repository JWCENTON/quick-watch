using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTablesWithMissingCreationTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "BookedEquipments");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "WorksOn",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Reservations",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Reservations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Equipment",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "BookedEquipments",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "BookedEquipments");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "WorksOn",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Reservations",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Reservations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "BookedEquipments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
