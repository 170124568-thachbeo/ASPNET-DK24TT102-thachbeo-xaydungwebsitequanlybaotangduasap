using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBaoTangDuaSap.Models
{
    public class HienVat
    {
        [Key]
        public int MaHienVat { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống tên hiện vật")]
        [Display(Name = "Tên Hiện Vật")] 
        public string TenHienVat { get; set; }

        [Display(Name = "Mô Tả")] 
        public string? MoTa { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Tiếp Nhận")] 
        public DateTime? NgayTiepNhan { get; set; }

        [Display(Name = "Số Lượng")] 
        public int SoLuong { get; set; }

        [Display(Name = "Mã Danh Mục")] 
        public int MaDanhMuc { get; set; }

        [ForeignKey("MaDanhMuc")]
        [Display(Name = "Danh Mục")] 
        public DanhMuc? DanhMuc { get; set; }

        [Display(Name = "Hình Ảnh")]
        public string? HinhAnh { get; set; }

    }
}