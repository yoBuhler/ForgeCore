using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Migrations
{
    /// <inheritdoc />
    public partial class ConvertMaterialNumeratorToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Numerator",
                table: "MaterialsUnidadesMedida",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Denominator",
                table: "MaterialsUnidadesMedida",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Numerator",
                table: "MaterialsUnidadesMedida",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Denominator",
                table: "MaterialsUnidadesMedida",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
