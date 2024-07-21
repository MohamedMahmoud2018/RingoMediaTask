using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMediaTask.Migrations
{
    public partial class dataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentLogo", "DepartmentName", "ParentDepartmentId" },
                values: new object[] { 1, "", "Development", 2 });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentLogo", "DepartmentName", "ParentDepartmentId" },
                values: new object[] { 2, "", "Management", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);
        }
    }
}
