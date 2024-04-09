using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.Migrations
{
    /// <inheritdoc />
    public partial class DropDeviceID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Games_GameId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_GameId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Devices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Devices",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 1,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 3,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 4,
                column: "GameId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GameId",
                table: "Devices",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Games_GameId",
                table: "Devices",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
