using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstone.Data.Migrations
{
    public partial class participant_update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a3c04a2-e35e-46bc-afe9-bd10fd9b5007");

            migrationBuilder.CreateTable(
                name: "ParticipantAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<string>(nullable: true),
                    AddressLabel = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10, 8)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(11, 8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantAddress", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f22ece3-8368-4648-91fa-025261bb0f35", "5726406d-c749-492c-a70f-b54260272d0a", "Participant", "PARTICIPANT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantAddress");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f22ece3-8368-4648-91fa-025261bb0f35");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7a3c04a2-e35e-46bc-afe9-bd10fd9b5007", "ba146100-b6ad-4383-84f4-f89d35679389", "Participant", "PARTICIPANT" });
        }
    }
}
