using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab09.Models
{
    [Table("vdl_SanPham")]
    public class vdlSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long vdlId { get; set; }

        [Required]
        [Display(Name = "Mã Sản Phẩm")]
        public string vdlMaSanPham { get; set; }

        [Required]
        [Display(Name = "Tên Sản Phẩm")]
        public string vdlTenSanPham { get; set; }

        [Display(Name = "Hình Ảnh")]
        public string vdlHinhAnh { get; set; }

        [Display(Name = "Số Lượng")]
        public int vdlSoLuong { get; set; }

        [Display(Name = "Đơn Giá")]
        public decimal vdlDonGia { get; set; }

        public long vdlLoaiId { get; set; }

        [ForeignKey("vdlLoaiId")]
        [ValidateNever]
        public virtual vdlLoaiSanPham LoaiSanPham { get; set; }

    }
}