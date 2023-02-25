using System;
using System.ComponentModel.DataAnnotations;

namespace Project5_trangdocbao.Areas.Admin.ViewModels
{
    public class BAIDANGVM
    {
        public BAIDANGVM()
        {
           
        }

        [Key]

 
        public int IDBaiDang { get; set; }

     

        [Display(Name = "Tên bài đăng")]
        public string TenBaiDang { get; set; }


        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }

        [StringLength(200)]
        public string UrlRequire { get; set; }

        [StringLength(500)]
        [Display(Name = "Ảnh đại diện")]
        public string AnhDaiDien { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        public string NoiDungBanDau { get; set; }
        public string TieuDeBanDau { get; set; }
        public string TieuDeResult { get; set; }
        public string PhuDeBanDau { get; set; }
        public string PhuDeResult { get; set; }
        public int IDTaiKhoan { get; set; }
        public int IDThe { get; set; }
      
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayDang { get; set; }

        [StringLength(50)]
        public string TrangThaiBaiDang { get; set; }
        [Display(Name = "Thể loại")]
        public int IDTheLoai { get; set; }
        public bool IsDangBai { get; set; }
    }
}