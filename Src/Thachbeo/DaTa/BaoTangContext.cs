using Microsoft.EntityFrameworkCore;
using QuanLyBaoTangDuaSap.Models;
using System.Reflection.Emit;

namespace QuanLyBaoTangDuaSap.Data
{
    public class BaoTangContext : DbContext
    {
        public BaoTangContext(DbContextOptions<BaoTangContext> options) : base(options) { }

        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<HienVat> HienVats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa tên bảng chuẩn cấu trúc SQL đã tạo ở Bước 1
            modelBuilder.Entity<DanhMuc>().ToTable("DanhMuc");
            modelBuilder.Entity<HienVat>().ToTable("HienVat");
        }
    }
}