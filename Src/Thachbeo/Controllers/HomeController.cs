using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBaoTangDuaSap.Data;
using System.Diagnostics;
using Thachbeo.Models;

namespace Thachbeo.Controllers
{
    public class HomeController : Controller
    {
        private readonly BaoTangContext _context;

        // Hàm khởi tạo nạp dữ liệu database
        public HomeController(BaoTangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string chuoiTimKiem)
        {
            var danhSach = _context.HienVats.Include(h => h.DanhMuc).AsQueryable();

            if (!string.IsNullOrWhiteSpace(chuoiTimKiem))
            {
                danhSach = danhSach.Where(h => h.TenHienVat.Contains(chuoiTimKiem) || h.MoTa.Contains(chuoiTimKiem));
            }

            return View(await danhSach.ToListAsync());
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
