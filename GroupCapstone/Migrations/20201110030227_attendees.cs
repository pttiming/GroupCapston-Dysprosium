using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstone.Migrations
{
    public partial class attendees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9248911f-0b96-4e7f-88a2-cf533ccd9cfa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f283d5a-d2b0-41d4-a3fd-0b9654423692", "6e61d233-e126-4dee-9cf9-382bebbc8ce9", "Participant", "PARTICIPANT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f283d5a-d2b0-41d4-a3fd-0b9654423692");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9248911f-0b96-4e7f-88a2-cf533ccd9cfa", "326dc571-3602-4dfd-b813-da949dfbe025", "Participant", "PARTICIPANT" });
        }
    }
}
