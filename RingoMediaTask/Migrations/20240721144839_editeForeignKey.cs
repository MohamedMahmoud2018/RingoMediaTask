using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMediaTask.Migrations
{
    public partial class editeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentDepartmentId",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDepartmentId",
                table: "Departments",
                column: "ParentDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_ParentDepartmentId",
                table: "Departments",
                column: "ParentDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_ParentDepartmentId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ParentDepartmentId",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "ParentDepartmentId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
