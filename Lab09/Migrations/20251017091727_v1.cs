using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab09.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vdl_LoaiSanPham",
                columns: table => new
                {
                    vdlId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vdlMaLoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vdlTenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vdlTrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vdl_LoaiSanPham", x => x.vdlId);
                });

            migrationBuilder.CreateTable(
                name: "vdl_SanPham",
                columns: table => new
                {
                    vdlId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vdlMaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vdlTenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vdlHinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vdlSoLuong = table.Column<int>(type: "int", nullable: false),
                    vdlDonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vdlLoaiId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vdl_SanPham", x => x.vdlId);
                    table.ForeignKey(
                        name: "FK_vdl_SanPham_vdl_LoaiSanPham_vdlLoaiId",
                        column: x => x.vdlLoaiId,
                        principalTable: "vdl_LoaiSanPham",
                        principalColumn: "vdlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vdl_SanPham_vdlLoaiId",
                table: "vdl_SanPham",
                column: "vdlLoaiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vdl_SanPham");

            migrationBuilder.DropTable(
                name: "vdl_LoaiSanPham");
        }
    }
}
