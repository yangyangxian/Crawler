using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class InitialCreate07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Community_AdministrativeDistrictId",
                table: "Community");

            migrationBuilder.AlterColumn<string>(
                name: "AdministrativeDistrictName",
                table: "AdministrativeDistrict",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AdministrativeDistrict",
                columns: new[] { "AdministrativeDistrictId", "AdministrativeDistrictName" },
                values: new object[,]
                {
                    { 1, "碑林区" },
                    { 2, "新城区" },
                    { 3, "莲湖区" },
                    { 4, "雁塔区" },
                    { 5, "未央区" },
                    { 6, "灞桥区" },
                    { 7, "长安区" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Community_AdministrativeDistrictId",
                table: "Community",
                column: "AdministrativeDistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Community_AdministrativeDistrictId",
                table: "Community");

            migrationBuilder.DeleteData(
                table: "AdministrativeDistrict",
                keyColumn: "AdministrativeDistrictId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AdministrativeDistrict",
                keyColumn: "AdministrativeDistrictId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AdministrativeDistrict",
                keyColumn: "AdministrativeDistrictId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AdministrativeDistrict",
                keyColumn: "AdministrativeDistrictId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AdministrativeDistrict",
                keyColumn: "AdministrativeDistrictId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AdministrativeDistrict",
                keyColumn: "AdministrativeDistrictId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AdministrativeDistrict",
                keyColumn: "AdministrativeDistrictId",
                keyValue: 7);

            migrationBuilder.AlterColumn<int>(
                name: "AdministrativeDistrictName",
                table: "AdministrativeDistrict",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Community_AdministrativeDistrictId",
                table: "Community",
                column: "AdministrativeDistrictId",
                unique: true,
                filter: "[AdministrativeDistrictId] IS NOT NULL");
        }
    }
}
