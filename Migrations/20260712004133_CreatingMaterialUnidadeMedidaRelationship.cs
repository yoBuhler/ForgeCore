using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeCore.Migrations
{
    /// <inheritdoc />
    public partial class CreatingMaterialUnidadeMedidaRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnidadeMedida",
                table: "MaterialsUnidadesMedida",
                newName: "UnidadeMedidaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialsUnidadesMedida",
                table: "MaterialsUnidadesMedida",
                columns: new[] { "MaterialId", "UnidadeMedidaId" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsUnidadesMedida_UnidadeMedidaId",
                table: "MaterialsUnidadesMedida",
                column: "UnidadeMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialsUnidadesMedida_Materials_MaterialId",
                table: "MaterialsUnidadesMedida",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialsUnidadesMedida_UnidadesMedida_UnidadeMedidaId",
                table: "MaterialsUnidadesMedida",
                column: "UnidadeMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialsUnidadesMedida_Materials_MaterialId",
                table: "MaterialsUnidadesMedida");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialsUnidadesMedida_UnidadesMedida_UnidadeMedidaId",
                table: "MaterialsUnidadesMedida");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialsUnidadesMedida",
                table: "MaterialsUnidadesMedida");

            migrationBuilder.DropIndex(
                name: "IX_MaterialsUnidadesMedida_UnidadeMedidaId",
                table: "MaterialsUnidadesMedida");

            migrationBuilder.RenameColumn(
                name: "UnidadeMedidaId",
                table: "MaterialsUnidadesMedida",
                newName: "UnidadeMedida");
        }
    }
}
