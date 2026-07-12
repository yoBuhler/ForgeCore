using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Migrations
{
    /// <inheritdoc />
    public partial class CreatingMaterialCaracteristicsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Caracteristics_MaterialId",
                table: "Caracteristics",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caracteristics_Materials_MaterialId",
                table: "Caracteristics",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caracteristics_Materials_MaterialId",
                table: "Caracteristics");

            migrationBuilder.DropIndex(
                name: "IX_Caracteristics_MaterialId",
                table: "Caracteristics");
        }
    }
}
