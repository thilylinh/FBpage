using System.ComponentModel.DataAnnotations;

namespace Project5_trangdocbao.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nhập tên tài khoản!")]
        public string username { set; get; }
        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string password { set; get; }
        public bool remember { set; get; }
    }
}