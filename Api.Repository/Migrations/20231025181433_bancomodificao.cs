using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Repository.Migrations
{
    /// <inheritdoc />
    public partial class bancomodificao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DtAlteracao",
                table: "Produtos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCadastro",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtAlteracao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "DtCadastro",
                table: "Produtos");
        }
    }
}
