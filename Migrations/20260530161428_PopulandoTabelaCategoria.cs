using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoTabelaCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO categorias(nome, image_url) VALUES('sobremesa', 'sobremesa.jpg')");
            migrationBuilder.Sql(@"INSERT INTO categorias(nome, image_url) VALUES('Bebidas', 'bebidas.jpg')");
            migrationBuilder.Sql(@"INSERT INTO categorias(nome, image_url) VALUES('Lanches', 'lanches.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM categorias");

        }
    }
}
