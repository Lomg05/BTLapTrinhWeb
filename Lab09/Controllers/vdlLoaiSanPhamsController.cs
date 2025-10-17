using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab09.Models;

namespace Lab09.Controllers
{
    public class vdlLoaiSanPhamsController : Controller
    {
        private readonly vdlDbContext _context;

        public vdlLoaiSanPhamsController(vdlDbContext context)
        {
            _context = context;
        }

        // GET: vdlLoaiSanPhams/vdlIndex
        public async Task<IActionResult> vdlIndex()
        {
            return View(await _context.vdlLoaiSanPhams.ToListAsync());
        }

        // GET: vdlLoaiSanPhams/vdlDetails/5
        public async Task<IActionResult> vdlDetails(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vdlLoaiSanPham = await _context.vdlLoaiSanPhams
                .FirstOrDefaultAsync(m => m.vdlId == id);
            if (vdlLoaiSanPham == null)
            {
                return NotFound();
            }

            return View(vdlLoaiSanPham);
        }

        // GET: vdlLoaiSanPhams/vdlCreate
        public IActionResult vdlCreate()
        {
            return View();
        }

        // POST: vdlLoaiSanPhams/vdlCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vdlCreate([Bind("vdlId,vdlMaLoai,vdlTenLoai,vdlTrangThai")] vdlLoaiSanPham vdlLoaiSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vdlLoaiSanPham);
                await _context.SaveChangesAsync();
                // Sửa RedirectToAction để trỏ về action mới
                return RedirectToAction(nameof(vdlIndex));
            }
            return View(vdlLoaiSanPham);
        }

        // GET: vdlLoaiSanPhams/vdlEdit/5
        public async Task<IActionResult> vdlEdit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vdlLoaiSanPham = await _context.vdlLoaiSanPhams.FindAsync(id);
            if (vdlLoaiSanPham == null)
            {
                return NotFound();
            }
            return View(vdlLoaiSanPham);
        }

        // POST: vdlLoaiSanPhams/vdlEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vdlEdit(long id, [Bind("vdlId,vdlMaLoai,vdlTenLoai,vdlTrangThai")] vdlLoaiSanPham vdlLoaiSanPham)
        {
            if (id != vdlLoaiSanPham.vdlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vdlLoaiSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!vdlLoaiSanPhamExists(vdlLoaiSanPham.vdlId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Sửa RedirectToAction để trỏ về action mới
                return RedirectToAction(nameof(vdlIndex));
            }
            return View(vdlLoaiSanPham);
        }

        // GET: vdlLoaiSanPhams/vdlDelete/5
        public async Task<IActionResult> vdlDelete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vdlLoaiSanPham = await _context.vdlLoaiSanPhams
                .FirstOrDefaultAsync(m => m.vdlId == id);
            if (vdlLoaiSanPham == null)
            {
                return NotFound();
            }

            return View(vdlLoaiSanPham);
        }

        // POST: vdlLoaiSanPhams/vdlDelete/5
        // Sửa ActionName để khớp với action GET ở trên
        [HttpPost, ActionName("vdlDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vdlDeleteConfirmed(long id)
        {
            var vdlLoaiSanPham = await _context.vdlLoaiSanPhams.FindAsync(id);
            if (vdlLoaiSanPham != null)
            {
                _context.vdlLoaiSanPhams.Remove(vdlLoaiSanPham);
            }

            await _context.SaveChangesAsync();
            // Sửa RedirectToAction để trỏ về action mới
            return RedirectToAction(nameof(vdlIndex));
        }

        private bool vdlLoaiSanPhamExists(long id)
        {
            return _context.vdlLoaiSanPhams.Any(e => e.vdlId == id);
        }
    }
}