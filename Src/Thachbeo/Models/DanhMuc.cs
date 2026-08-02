using System.ComponentModel.DataAnnotations;

namespace QuanLyBaoTangDuaSap.Models
{
    public class DanhMuc
    {
        [Key]
        public int MaDanhMuc { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống tên danh mục")]
        public string TenDanhMuc { get; set; }

        public ICollection<HienVat>? HienVats { get; set; }
    }
}