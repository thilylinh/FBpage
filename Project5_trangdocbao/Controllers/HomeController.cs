using Model.DAO;
using Model.EntityFramework;
using System.Web.Mvc;
namespace Project5_trangdocbao.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //Lấy ra bài đăng mới nhất đưa lên trang chủ
            ViewBag._KLastestNews = new PostDao().LastestNews();
            ViewBag._K_LastestNews25 = new PostDao().SecondToFourthNews();
            //Lấy các bài đăng thể loại giaitri
            ViewBag.giaitri = new PostDao().postGiaiTri();

            //Lấy các bài đăng thể loại đời sống
            ViewBag.doisong = new PostDao().postDoiSong();

            //Lấy các bài đăng thể loại phap luat
            ViewBag.thethao = new PostDao().postTheThao();
          
            //top 10 xem nhieu
            ViewBag._k_relative = new PostDao().top10VIew();

            return View();

        }
        public ActionResult Register()
        {
            return View();
        }

        //Gửi thông tin về sever và chờ sử ý
        [HttpPost]
        public ActionResult Register(TAIKHOAN tk)
        {
            DocBaoDataContext db = new DocBaoDataContext();

            THELOAI item = new THELOAI
            {
                TenTheLoai = "thien",
                UrlRequire = "google.comn"
            };
            db.THELOAIs.Add(item);
            db.SaveChanges();


            //var DAO = new AccountDao();
            //var passmd5 = Encryptor.MD5Hash(tk.MatKhau);
            //tk.MatKhau = passmd5;
            //tk.QuyenHan = "U";
            //tk.TrangThaiNguoiDung = "bình thường";
            //long id = DAO.addAccount(tk);
            //if (id > 0)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Tạo tài khoản thất bại");
            //}
            return View("Index");
        }
        //Xử lý menu
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            //var model = new CateDao().ListCate();
            //return PartialView(model);
            var model = new TypeDao().ListTypeForCreatePost();
            return PartialView(model);
        }


    }
}
