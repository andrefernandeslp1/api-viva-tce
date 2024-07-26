using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FornecedorId",
                table: "Usuarios",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Fornecedores_FornecedorId",
                table: "Usuarios",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Fornecedores_FornecedorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_FornecedorId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "Usuarios");
        }
    }
}
