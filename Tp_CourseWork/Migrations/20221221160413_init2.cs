using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpCourseWork.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Localities",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Localities",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Localities",
                columns: new[] { "id", "Budget", "Mayor", "Name", "NumberResidants", "Type" },
                values: new object[] { 1, 100000, "Владимир Васильевич Марченко", "Волгоград", 100000, "City" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Localities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Localities",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Localities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
