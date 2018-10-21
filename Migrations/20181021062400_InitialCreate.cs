using Microsoft.EntityFrameworkCore.Migrations;

namespace IsItBeerOclock.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PushSubscriptions",
                columns: table => new
                {
                    Endpoint = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushSubscriptions", x => x.Endpoint);
                });

            migrationBuilder.CreateTable(
                name: "PushSubscriptionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Endpoint = table.Column<string>(nullable: true),
                    KeyType = table.Column<string>(nullable: true),
                    KeyValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushSubscriptionKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushSubscriptionKeys_PushSubscriptions_Endpoint",
                        column: x => x.Endpoint,
                        principalTable: "PushSubscriptions",
                        principalColumn: "Endpoint",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PushSubscriptionKeys_Endpoint",
                table: "PushSubscriptionKeys",
                column: "Endpoint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushSubscriptionKeys");

            migrationBuilder.DropTable(
                name: "PushSubscriptions");
        }
    }
}
