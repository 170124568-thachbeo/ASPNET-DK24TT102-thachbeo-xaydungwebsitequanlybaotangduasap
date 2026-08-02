
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBaoTangDuaSap.Models;
using QuanLyBaoTangDuaSap.Data;

public class DanhMucsController : Controller
{
    private readonly BaoTangContext _context;

    public DanhMucsController(BaoTangContext context)
    {
        _context = context;
    }

    // GET: DANHMUCS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.DanhMucs.ToListAsync());
    }

    // GET: DANHMUCS/Details/5
    public async Task<IActionResult> Details(int? madanhmuc)
    {
        if (madanhmuc == null)
        {
            return NotFound();
        }

        var danhmuc = await _context.DanhMucs
            .FirstOrDefaultAsync(m => m.MaDanhMuc == madanhmuc);
        if (danhmuc == null)
        {
            return NotFound();
        }

        return View(danhmuc);
    }

    // GET: DANHMUCS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DANHMUCS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaDanhMuc,TenDanhMuc,HienVats")] DanhMuc danhmuc)
    {
        if (ModelState.IsValid)
        {
            _context.Add(danhmuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(danhmuc);
    }

    // GET: DANHMUCS/Edit/5
    public async Task<IActionResult> Edit(int? madanhmuc)
    {
        if (madanhmuc == null)
        {
            return NotFound();
        }

        var danhmuc = await _context.DanhMucs.FindAsync(madanhmuc);
        if (danhmuc == null)
        {
            return NotFound();
        }
        return View(danhmuc);
    }

    // POST: DANHMUCS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? madanhmuc, [Bind("MaDanhMuc,TenDanhMuc,HienVats")] DanhMuc danhmuc)
    {
        if (madanhmuc != danhmuc.MaDanhMuc)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(danhmuc);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucExists(danhmuc.MaDanhMuc))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(danhmuc);
    }

    // GET: DANHMUCS/Delete/5
    public async Task<IActionResult> Delete(int? madanhmuc)
    {
        if (madanhmuc == null)
        {
            return NotFound();
        }

        var danhmuc = await _context.DanhMucs
            .FirstOrDefaultAsync(m => m.MaDanhMuc == madanhmuc);
        if (danhmuc == null)
        {
            return NotFound();
        }

        return View(danhmuc);
    }

    // POST: DANHMUCS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? madanhmuc)
    {
        var danhmuc = await _context.DanhMucs.FindAsync(madanhmuc);
        if (danhmuc != null)
        {
            _context.DanhMucs.Remove(danhmuc);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DanhMucExists(int? madanhmuc)
    {
        return _context.DanhMucs.Any(e => e.MaDanhMuc == madanhmuc);
    }
}
