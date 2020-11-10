using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstone.Migrations
{
    public partial class init : Migration
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
                values: new object[] { "c694d0c7-a644-45da-9e35-497c176c5daa", "5a5cede3-faef-4752-9266-c0b7746807d8", "Participant", "PARTICIPANT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c694d0c7-a644-45da-9e35-497c176c5daa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9248911f-0b96-4e7f-88a2-cf533ccd9cfa", "326dc571-3602-4dfd-b813-da949dfbe025", "Participant", "PARTICIPANT" });
        }
    }
}
