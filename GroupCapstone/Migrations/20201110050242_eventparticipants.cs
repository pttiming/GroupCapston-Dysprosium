using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstone.Migrations
{
    public partial class eventparticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f283d5a-d2b0-41d4-a3fd-0b9654423692");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ebb018d-0aea-4d11-a745-4d8ed0f75cca", "944672d5-9570-43d7-b7ae-4952282bc2d4", "Participant", "PARTICIPANT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ebb018d-0aea-4d11-a745-4d8ed0f75cca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f283d5a-d2b0-41d4-a3fd-0b9654423692", "6e61d233-e126-4dee-9cf9-382bebbc8ce9", "Participant", "PARTICIPANT" });
        }
    }
}
