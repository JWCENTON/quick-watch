using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedDatabaseDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedEquipments_Equipment_EquipmentId",
                table: "BookedEquipments");

            migrationBuilder.RenameColumn(
                name: "IsCheckedOut",
                table: "Equipment",
                newName: "InWarehouse");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "CheckOuts",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "CheckIns",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "BookedEquipments",
                newName: "EquipmentInUseId");

            migrationBuilder.RenameIndex(
                name: "IX_BookedEquipments_EquipmentId",
                table: "BookedEquipments",
                newName: "IX_BookedEquipments_EquipmentInUseId");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Equipment",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Commissions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "WarehouseDelivery",
                table: "CheckOuts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WarehouseDelivery",
                table: "CheckIns",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "BookedEquipments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "BookedEquipments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "EquipmentInUse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EquipmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInUse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentInUse_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EquipmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BookedEquipments_ReservationId",
                table: "BookedEquipments",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInUse_EquipmentId",
                table: "EquipmentInUse",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EquipmentId",
                table: "Reservations",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedEquipments_EquipmentInUse_EquipmentInUseId",
                table: "BookedEquipments",
                column: "EquipmentInUseId",
                principalTable: "EquipmentInUse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedEquipments_Reservations_ReservationId",
                table: "BookedEquipments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedEquipments_EquipmentInUse_EquipmentInUseId",
                table: "BookedEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedEquipments_Reservations_ReservationId",
                table: "BookedEquipments");

            migrationBuilder.DropTable(
                name: "EquipmentInUse");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_BookedEquipments_ReservationId",
                table: "BookedEquipments");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "WarehouseDelivery",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "WarehouseDelivery",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "BookedEquipments");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "BookedEquipments");

            migrationBuilder.RenameColumn(
                name: "InWarehouse",
                table: "Equipment",
                newName: "IsCheckedOut");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "CheckOuts",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "CheckIns",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "EquipmentInUseId",
                table: "BookedEquipments",
                newName: "EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_BookedEquipments_EquipmentInUseId",
                table: "BookedEquipments",
                newName: "IX_BookedEquipments_EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedEquipments_Equipment_EquipmentId",
                table: "BookedEquipments",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
