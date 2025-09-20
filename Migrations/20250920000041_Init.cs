using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parking_manager.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Plate = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                                   name: "IX_Vehicles_Plate",
                                   table: "Vehicles",
                                   column: "Plate",
                                   unique: true
                               );

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntryDate = table.Column<DateTime>(type: "datetime(2)", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime(2)", nullable: true),
                    TotalPrice = table.Column<double>(type: "double", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Parking_Vehicles_VehicleId",
                      column: x => x.VehicleId,
                      principalTable: "Vehicles",
                      principalColumn: "Plate",
                      onDelete: ReferentialAction.Cascade);

                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
           name: "IX_Parking_VehicleId",
           table: "Parking",
           column: "VehicleId",
           unique: true);


            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ValidFrom = table.Column<DateTime>(type: "datetime(2)", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime(2)", nullable: false),
                    PricePerHour = table.Column<double>(type: "double", nullable: false),
                    FirstHourPrice = table.Column<double>(type: "double", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
