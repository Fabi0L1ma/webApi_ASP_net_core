using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO produtos
                (
                    nome,
                    descricao,
                    preco,
                    imagem_url,
                    estoque,
                    data_cadastro,
                    categoria_id
                )
                VALUES
                (
                    'Bolo de Chocolate',
                    'Bolo recheado com cobertura de chocolate',
                    25.90,
                    'bolo.jpg',
                    10,
                    CURRENT_TIMESTAMP,
                    7
                )
            ");

            migrationBuilder.Sql(@"
                INSERT INTO produtos
                (
                    nome,
                    descricao,
                    preco,
                    imagem_url,
                    estoque,
                    data_cadastro,
                    categoria_id
                )
                VALUES
                (
                    'coca cola',
                    'coca cola 350 ml',
                    5.90,
                    'coca.jpg',
                    120,
                    CURRENT_TIMESTAMP,
                    8
                )
            ");

            migrationBuilder.Sql(@"
                INSERT INTO produtos
                (
                    nome,
                    descricao,
                    preco,
                    imagem_url,
                    estoque,
                    data_cadastro,
                    categoria_id
                )
                VALUES
                (
                    'Sanduiche',
                    'Sanduche de frango 350g',
                    15.90,
                    'sanduiche_frango.jpg',
                    50,
                    CURRENT_TIMESTAMP,
                    9
                )
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM produtos");

        }
    }
}
