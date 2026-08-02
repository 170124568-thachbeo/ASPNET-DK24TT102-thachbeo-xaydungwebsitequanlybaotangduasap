
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBaoTangDuaSap.Data;
using QuanLyBaoTangDuaSap.Models;

public class HienVatsController : Controller
{
    private readonly BaoTangContext _context;

    public HienVatsController(BaoTangContext context)
    {
        _context = context;
    }

    // GET: HIENVATS
    public async Task<IActionResult> Index()
    {
        // Thêm .Include vào để lấy kèm dữ liệu tên danh mục sang giao diện
        var danhSachHienVat = _context.HienVats.Include(h => h.DanhMuc);
        return View(await danhSachHienVat.ToListAsync());
    }

    // GET: HIENVATS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var hienVat = await _context.HienVats
            .Include(h => h.DanhMuc)
            .FirstOrDefaultAsync(m => m.MaHienVat == id);

        if (hienVat == null)
        {
            return NotFound();
        }

        return View(hienVat);
    }
    // GET: HIENVATS/Create
    public IActionResult Create()
    {
        ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc");
        return View();
    }
    // POST: HIENVATS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaHienVat,TenHienVat,MoTa,NgayTiepNhan,SoLuong,MaDanhMuc")] HienVat hienVat, IFormFile fileAnh)
    {
        if (ModelState.IsValid)
        {
            if (fileAnh != null && fileAnh.Length > 0)
            {
                // Tạo tên file ngẫu nhiên để không bị trùng (Ví dụ: 3012a-duasap.jpg)
                string tenFileUnique = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileAnh.FileName);

                // Đường dẫn lưu ảnh vào thư mục wwwroot/images của dự án
                string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                // Nếu chưa có thư mục images thì hệ thống tự tạo mới
                if (!Directory.Exists(thuMucAnh)) Directory.CreateDirectory(thuMucAnh);
                string duongDanFile = Path.Combine(thuMucAnh, tenFileUnique);
                using (var luongFile = new FileStream(duongDanFile, FileMode.Create))
                {
                    await fileAnh.CopyToAsync(luongFile);
                }
                // Lưu tên file vào cơ sở dữ liệu
                hienVat.HinhAnh = tenFileUnique;
            }
            _context.Add(hienVat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc", hienVat.MaDanhMuc);
        return View(hienVat);
    }
    // GET: HIENVATS/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var hienVat = await _context.HienVats.FindAsync(id);
        if (hienVat == null) return NotFound();

        ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc", hienVat.MaDanhMuc);
        return View(hienVat);
    }

    // 2. Hàm xử lý lưu dữ liệu khi bấm nút (XÓA BỎ dòng [Route] nếu có)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MaHienVat,TenHienVat,MoTa,NgayTiepNhan,SoLuong,MaDanhMuc,HinhAnh")] HienVat hienVat, IFormFile? fileAnh)
    {
        if (id != hienVat.MaHienVat) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                if (fileAnh != null && fileAnh.Length > 0)
                {
                    string tenFileUnique = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileAnh.FileName);
                    string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(thuMucAnh)) Directory.CreateDirectory(thuMucAnh);

                    string duongDanFile = Path.Combine(thuMucAnh, tenFileUnique);
                    using (var luongFile = new FileStream(duongDanFile, FileMode.Create))
                    {
                        await fileAnh.CopyToAsync(luongFile);
                    }
                    hienVat.HinhAnh = tenFileUnique;
                }

                _context.Update(hienVat);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.HienVats.Any(e => e.MaHienVat == hienVat.MaHienVat)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc", hienVat.MaDanhMuc);
        return View(hienVat);
    }
    // cảnh báo xóa
    [HttpGet]
    [Route("HienVats/Delete")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hienVat = await _context.HienVats
            .Include(h => h.DanhMuc)
            .FirstOrDefaultAsync(m => m.MaHienVat == id);

        if (hienVat == null)
        {
            return NotFound();
        }

        return View(hienVat);
    }
    //nút xác nhận
    [HttpPost]
    [Route("HienVats/XoaHienVatThucSu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> XoaHienVatThucSu(int MaHienVat)
    {
        var hienVat = await _context.HienVats.FindAsync(MaHienVat);
        if (hienVat != null)
        {
            _context.HienVats.Remove(hienVat);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    private bool HienVatExists(int? mahienvat)
    {
        return _context.HienVats.Any(e => e.MaHienVat == mahienvat);
    }
}
