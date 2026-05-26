using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into \"Categorias\"(\"Nome\", \"ImageUrl\") values('Sobremesa', 'sobremesa.jpg')\r\n");
            //migrationBuilder.Sql("insert into Categorias(Nome, ImageUrl) values('Lanche', 'lanche.jpg')");
            //migrationBuilder.Sql("insert into Categorias(Nome, ImageUrl) values('Sobremesa', 'sobremesa.jpg')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Categorias");
        }
    }
}
