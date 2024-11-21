using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KCIAOGS24.NET.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_endereco",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tipoResidencial = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tarifa = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    gastoMensal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    economia = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    fk_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_endereco", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_endereco_tb_usuario_fk_usuario",
                        column: x => x.fk_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_energia_eolica",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    potencialNominal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    alturaTorre = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    diametroRotor = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    energiaEstimadaGerada = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    fk_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_energia_eolica", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_energia_eolica_tb_endereco_fk_endereco",
                        column: x => x.fk_endereco,
                        principalTable: "tb_endereco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_energia_solar",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    areaPlaca = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    irradiacaoSolar = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    energiaEstimadaGerada = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    fk_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_energia_solar", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_energia_solar_tb_endereco_fk_endereco",
                        column: x => x.fk_endereco,
                        principalTable: "tb_endereco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_endereco_fk_usuario",
                table: "tb_endereco",
                column: "fk_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_energia_eolica_fk_endereco",
                table: "tb_energia_eolica",
                column: "fk_endereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_energia_solar_fk_endereco",
                table: "tb_energia_solar",
                column: "fk_endereco",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_energia_eolica");

            migrationBuilder.DropTable(
                name: "tb_energia_solar");

            migrationBuilder.DropTable(
                name: "tb_endereco");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
