using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroServiceClientProduit.Migrations
{
    /// <inheritdoc />
    public partial class Update_1_Produit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantite",
                table: "Produits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantite",
                table: "Produits");
        }
    }
}
