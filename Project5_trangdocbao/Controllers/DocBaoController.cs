using Model.DAO;
using System;
using System.Web.Mvc;

namespace Project5_trangdocbao.Controllers
{
    public class DocBaoController : Controller
    {
        //
        // GET: /DocBao/

        public ActionResult Index(long id)
        {
            var dao = new PostDao();

            ViewBag._k_news = dao.NewsFindByID(id);

            //Xử lý thêm quảng cáo vào đoạn văn
            string noiDung = ViewBag._k_news[0].NoiDung;

            //noiDung = "sdhfjsdhf</p>sdsdsdas</p>fsdfsdfsdf</p>";
            var maQC1 = "<div id =\"M869115ScriptRootC1429040\"></div>  <script src =\"https://jsc.mgid.com/l/i/lifenews247.com.1429040.js\" async></script>    <amp-embed width =\"600\" height=\"600\" layout=\"responsive\" type=\"mgid\" data-publisher=\"lifenews247.com\" data-widget=\"1429040\" data-container=\"M869115ScriptRootC1429040\" data-block-on-consent=\"_till_responded\" > </amp-embed>";
            var maQC2 = "<div id =\"M869115ScriptRootC1429539\"></div>  <script src =\"https://jsc.mgid.com/l/i/lifenews247.com.1429539.js\" async></script>    <amp-embed width =\"600\" height=\"600\" layout=\"responsive\" type=\"mgid\" data-publisher=\"lifenews247.com\" data-widget=\"1429539\" data-container=\"M869115ScriptRootC1429539\" data-block-on-consent=\"_till_responded\" > </amp-embed>";
           
            //maQC = "<div>sdsd</div>";
            var noiDungs = noiDung.Split(new string[] { "</p>" }, StringSplitOptions.None);
            string result = "";
            var count = noiDungs.Length;
            var pageIndex1 = 3;
            var pageIndex2 = 8;

            for (int i = 0; i < count; i++)
            {
                if (i < count-1)
                    result += noiDungs[i] + "</p>";
                else
                {
                    result += noiDungs[i];
                }
                if (i==pageIndex1 &&i<count-1)
                {
                    result += maQC1;
                }else if (i == pageIndex2 && i < count - 1)
                {
                    result += maQC2;
                }
            }
            ViewBag._k_news[0].NoiDung = result;
            //Lấy 10 bài đăng cùng thể loại
            ViewBag._k_relative = dao.top10Relative(id);


            //Lấy 10 bài đăng cùng người đăng
            ViewBag._k_poster = dao.top10CungNguoiDang(id);

            //Lấy tên thể loại để hiển thị ra trang đọc báo
            ViewBag.vbtentheloai = dao.theloaibaidang(id);

            //Lấy thông tin người đăng
            ViewBag.getinfoposter = dao.GetInfoPoster(id);


            //var dao1 = new ViewsDao();
            //dao1.AddViews(id);
            return View();
        }


        //Bài báo theo thể loại
        public ActionResult DocBaoTheoNguoiDang(int id, int page = 1, int pageSize = 10)
        {
            var dao = new PostDao();
            var model = dao.ListAllPostForPoster(id, page, pageSize);
            //top 10 xem nhieu
            ViewBag._k_relative = new PostDao().top10VIew();
            return View(model);
        }
    }
}
