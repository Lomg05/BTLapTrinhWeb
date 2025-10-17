using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab09.Models
{
    [Table("vdl_LoaiSanPham")]
    public class vdlLoaiSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long vdlId { get; set; }

        [Required]
        [Display(Name = "Mã Loại")]
        public string vdlMaLoai { get; set; }

        [Required]
        [Display(Name = "Tên Loại")]
        public string vdlTenLoai { get; set; }

        [Display(Name = "Trạng Thái")]
        public bool vdlTrangThai { get; set; }

        [ValidateNever]
        public virtual ICollection<vdlSanPham> SanPhams { get; set; }

    }
}