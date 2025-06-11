using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpiryDateinJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expiry_date",
                table: "job",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2025, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expiry_date",
                table: "job");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2025, 6, 6, 11, 29, 9, 758, DateTimeKind.Utc).AddTicks(2429), new DateTime(2025, 6, 6, 11, 29, 9, 758, DateTimeKind.Utc).AddTicks(2430) });
        }
    }
}
