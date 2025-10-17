using Microsoft.EntityFrameworkCore;

namespace Lab09.Models
{
    public partial class vdlDbContext : DbContext
    {
        public vdlDbContext(DbContextOptions<vdlDbContext> options) : base(options)
        {
        }
        public DbSet<vdlSanPham> vdlSanPhams { get; set; }
        public DbSet<vdlLoaiSanPham> vdlLoaiSanPhams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vdlLoaiSanPham>(entity =>
            {
                entity.HasMany(lsp => lsp.SanPhams)
                      .WithOne(sp => sp.LoaiSanPham)
                      .HasForeignKey(sp => sp.vdlLoaiId);
            });
        }
    }
}