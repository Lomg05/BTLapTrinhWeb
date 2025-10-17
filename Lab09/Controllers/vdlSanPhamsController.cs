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
    public class vdlSanPhamsController : Controller
    {
        private readonly vdlDbContext _context;

        public vdlSanPhamsController(vdlDbContext context)
        {
            _context = context;
        }

        // GET: vdlSanPhams/vdlIndex
        public async Task<IActionResult> vdlIndex()
        {
            var vdlDbContext = _context.vdlSanPhams.Include(n => n.LoaiSanPham);
            return View(await vdlDbContext.ToListAsync());
        }

        // GET: vdlSanPhams/vdlDetails/5
        public async Task<IActionResult> vdlDetails(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vdlSanPham = await _context.vdlSanPhams
                .Include(n => n.LoaiSanPham)
                .FirstOrDefaultAsync(m => m.vdlId == id);
            if (vdlSanPham == null)
            {
                return NotFound();
            }

            return View(vdlSanPham);
        }

        // GET: vdlSanPhams/vdlCreate
        public IActionResult vdlCreate()
        {
            ViewData["vdlLoaiId"] = new SelectList(_context.vdlLoaiSanPhams, "vdlId", "vdlTenLoai");
            return View();
        }

        // POST: vdlSanPhams/vdlCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vdlCreate(vdlSanPham vdlSanPham, IFormFile imageFile)
        {
            // Bỏ qua validation của trường Hình Ảnh vì ta sẽ xử lý nó thủ công
            ModelState.Remove("vdlHinhAnh");

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // 1. Lấy đường dẫn đến thư mục lưu file (wwwroot/images)
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // 2. Tạo tên file duy nhất để tránh trùng lặp
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // 3. Lưu file vào thư mục
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // 4. GÁN TÊN FILE VÀO MODEL - ĐÂY LÀ BƯỚC QUAN TRỌNG NHẤT
                    vdlSanPham.vdlHinhAnh = uniqueFileName;
                }
                else
                {
                    // Gán ảnh mặc định nếu người dùng không upload
                    // vdlSanPham.vdlHinhAnh = "default.png"; 

                    // Hoặc báo lỗi nếu ảnh là bắt buộc
                    ModelState.AddModelError("vdlHinhAnh", "Vui lòng chọn hình ảnh sản phẩm.");
                    ViewData["vdlLoaiId"] = new SelectList(_context.vdlLoaiSanPhams, "vdlId", "vdlTenLoai", vdlSanPham.vdlLoaiId);
                    return View(vdlSanPham);
                }

                _context.Add(vdlSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(vdlIndex));
            }

            // Nếu ModelState không hợp lệ, tạo lại SelectList và trả về View
            ViewData["vdlLoaiId"] = new SelectList(_context.vdlLoaiSanPhams, "vdlId", "vdlTenLoai", vdlSanPham.vdlLoaiId);
            return View(vdlSanPham);
        }
        // GET: vdlSanPhams/vdlEdit/5
        public async Task<IActionResult> vdlEdit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vdlSanPham = await _context.vdlSanPhams.FindAsync(id);
            if (vdlSanPham == null)
            {
                return NotFound();
            }
            ViewData["vdlLoaiId"] = new SelectList(_context.vdlLoaiSanPhams, "vdlId", "vdlTenLoai", vdlSanPham.vdlLoaiId);
            return View(vdlSanPham);
        }

        // POST: vdlSanPhams/vdlEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vdlEdit(long id, [Bind("vdlId,vdlMaSanPham,vdlTenSanPham,vdlHinhAnh,vdlSoLuong,vdlDonGia,vdlLoaiId")] vdlSanPham vdlSanPham)
        {
            if (id != vdlSanPham.vdlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vdlSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!vdlSanPhamExists(vdlSanPham.vdlId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Sửa RedirectToAction
                return RedirectToAction(nameof(vdlIndex));
            }
            ViewData["vdlLoaiId"] = new SelectList(_context.vdlLoaiSanPhams, "vdlId", "vdlTenLoai", vdlSanPham.vdlLoaiId);
            return View(vdlSanPham);
        }

        // GET: vdlSanPhams/vdlDelete/5
        public async Task<IActionResult> vdlDelete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vdlSanPham = await _context.vdlSanPhams
                .Include(n => n.LoaiSanPham)
                .FirstOrDefaultAsync(m => m.vdlId == id);
            if (vdlSanPham == null)
            {
                return NotFound();
            }

            return View(vdlSanPham);
        }

        // POST: vdlSanPhams/vdlDelete/5
        // Sửa ActionName
        [HttpPost, ActionName("vdlDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vdlDeleteConfirmed(long id)
        {
            var vdlSanPham = await _context.vdlSanPhams.FindAsync(id);
            if (vdlSanPham != null)
            {
                _context.vdlSanPhams.Remove(vdlSanPham);
            }

            await _context.SaveChangesAsync();
            // Sửa RedirectToAction
            return RedirectToAction(nameof(vdlIndex));
        }

        private bool vdlSanPhamExists(long id)
        {
            return _context.vdlSanPhams.Any(e => e.vdlId == id);
        }
    }
}