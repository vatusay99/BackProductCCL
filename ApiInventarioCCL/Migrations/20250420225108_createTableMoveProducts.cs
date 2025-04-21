using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiInventarioCCL.Migrations
{
    /// <inheritdoc />
    public partial class createTableMoveProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoveProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Moveproduct = table.Column<int>(type: "integer", nullable: false),
                    date_move = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    productId = table.Column<int>(type: "integer", nullable: false),
                    amountMove = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveProducts_Productos_productId",
                        column: x => x.productId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoveProducts_productId",
                table: "MoveProducts",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoveProducts");
        }
    }
}
