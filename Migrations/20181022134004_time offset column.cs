using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsItBeerOclock.API.Migrations
{
    public partial class timeoffsetcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeOffset",
                table: "PushSubscriptions",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOffset",
                table: "PushSubscriptions");
        }
    }
}
