using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendedorId = table.Column<long>(type: "bigint", nullable: false),
                    NomeArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConteudoArquivo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projecoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<long>(type: "bigint", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projecoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendedorId = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentoTipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentoNumero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relacionamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Situacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Projecoes");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Vendedores");
        }
    }
}
