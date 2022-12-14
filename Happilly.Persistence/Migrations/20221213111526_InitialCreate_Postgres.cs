using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Happilly.Persistence.Migrations
{
    public partial class InitialCreate_Postgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Group = table.Column<int>(type: "integer", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reminder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Registered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineReminder",
                columns: table => new
                {
                    MedicinesId = table.Column<Guid>(type: "uuid", nullable: false),
                    RemindersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineReminder", x => new { x.MedicinesId, x.RemindersId });
                    table.ForeignKey(
                        name: "FK_MedicineReminder_Medicine_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineReminder_Reminder_RemindersId",
                        column: x => x.RemindersId,
                        principalTable: "Reminder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Name",
                table: "Medicine",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicineReminder_RemindersId",
                table: "MedicineReminder",
                column: "RemindersId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_Name",
                table: "Reminder",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineReminder");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Reminder");
        }
    }
}
