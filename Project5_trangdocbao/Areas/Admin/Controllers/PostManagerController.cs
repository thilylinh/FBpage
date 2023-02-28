using Model.DAO;
using Model.EntityFramework;
using PagedList;
using Project5_trangdocbao.Areas.Admin.Models;
using Project5_trangdocbao.Areas.Admin.ViewModels;
using Project5_trangdocbao.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project5_trangdocbao.Areas.Admin.Controllers
{
    public class PostManagerController : BaseAdminController
    {
        //
        // GET: /Admin/PostManager/
        //Tất cả bài đăng đã duyệt
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            //Ghi file
            var dao = new PostDao();
            var model = dao.ListAll(searchString, page, pageSize);
            ViewBag.searchstring = searchString;
            return View(model);
        }

        [HttpGet]
        //Tất cả bài đăng chờ duyệt
        public ActionResult PostWaiting(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new PostDao();
            var model = dao.ListAllWaiting(searchString, page, pageSize);
            ViewBag.searchstring = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            GetTypeForPost();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create2(BAIDANGVM bd)
        {
            if (ModelState.IsValid)
            {
                var model = new BAIDANG();
                model.AnhDaiDien = bd.AnhDaiDien;
                var DAO = new PostDao();
                model.IDBaiDang = bd.IDBaiDang;
                model.TenBaiDang = bd.TenBaiDang;
                model.TieuDe = bd.TieuDe;
                model.AnhDaiDien = bd.AnhDaiDien;
                model.IDTheLoai = bd.IDTheLoai;
                if (bd.IDThe <= -1)
                {
                    model.IdThe = null;
                }
                else
                {
                    model.IdThe = bd.IDThe;
                }
                model.UrlRequire = RewriteURL.RewriteUrl(bd.TenBaiDang);
                if (bd.IsDangBai)
                    model.TrangThaiBaiDang = "đã duyệt";
                else
                    model.TrangThaiBaiDang = "chờ duyệt";

                int idtk = int.Parse(Session["USER_ID"].ToString());
                model.IDTaiKhoan = idtk;
                model.NgayDang = DateTime.Now;
                model.NoiDung = bd.NoiDung.Replace("<p>&nbsp;</p>", "").Replace("\n", "").Replace("<p><figure>", "").Replace("</figure></p>", "").Replace("<p><img", "<img").Replace("/></p>", "/>");
                bd.IDBaiDang = 0;
                long idpost = DAO.CreatePost(model);

                //return RedirectToAction("PostWaiting", "PostManager");
                if (bd.IsDangBai)
                    NewRSS.LoadRSS();
            }
            SetAlert("Soạn bài đăng thành công", "thanhcong");
            //return RedirectToAction("PostWaiting", "PostManager");
            return Json(Url.Action("MyPost", "PostManager"));
        }


        public JsonResult CheckCk(NoiDungBaiDang model)
        {
            var dao = new TuCamDao();
            var listTu = dao.ListTuCam();
            NoiDungBaiDang returnObject = new NoiDungBaiDang();
            List<string> TuCams = new List<string>();

            string noiDungBanDau = model.NoiDung;
            string tieuDeBanDau = model.TieuDe;
            string phuDeBanDau = model.PhuDe;
            if (model.IsNoiDung & model.IsTieuDe)
            {
                foreach (var item in listTu)
                {
                    model.NoiDung = model.NoiDung.Replace(item.Tu, "<span style = 'color:red'>" + item.Tu + "</span>");
                    model.TieuDe = model.TieuDe.Replace(item.Tu, "<span style = 'color:red'>" + item.Tu + "</span>");
                    model.PhuDe = model.PhuDe.Replace(item.Tu, "<span style = 'color:red'>" + item.Tu + "</span>");
                    if (model.NoiDung != noiDungBanDau || model.TieuDe != tieuDeBanDau || model.PhuDe != phuDeBanDau)
                    {
                        TuCams.Add(item.Tu);
                        noiDungBanDau = model.NoiDung;
                        tieuDeBanDau = model.TieuDe;
                        phuDeBanDau = model.PhuDe;
                    }
                }
            }
            else if (model.IsTieuDe)
            {
                foreach (var item in listTu)
                {
                    model.TieuDe = model.TieuDe.Replace(item.Tu, "<span style = 'color:red'>" + item.Tu + "</span>");
                    model.PhuDe = model.PhuDe.Replace(item.Tu, "<span style = 'color:red'>" + item.Tu + "</span>");
                    if (model.TieuDe != tieuDeBanDau || model.PhuDe != phuDeBanDau)
                    {
                        TuCams.Add(item.Tu);
                        noiDungBanDau = model.NoiDung;
                        tieuDeBanDau = model.TieuDe;
                        phuDeBanDau = model.PhuDe;
                    }
                }
            }
            else if (model.IsNoiDung)
            {
                foreach (var item in listTu)
                {
                    model.NoiDung = model.NoiDung.Replace(item.Tu, "<span style = 'color:red'>" + item.Tu + "</span>");
                    if (model.NoiDung != noiDungBanDau)
                    {
                        TuCams.Add(item.Tu);
                        noiDungBanDau = model.NoiDung;
                        tieuDeBanDau = model.TieuDe;
                        phuDeBanDau = model.PhuDe;
                    }
                }
            }
            model.TuCams = TuCams;
            return Json(model);
        }

        //public JsonResult ThayThe(string content)
        //{
        //    var dao = new TuCamDao();
        //    var listTu = dao.ListTuCam();
        //    ReturnObject returnObject = new ReturnObject();
        //    foreach (var item in listTu)
        //    {
        //        content = content.Replace(item.Tu, item.ThayThe);
        //    }
        //    returnObject.Message = content;          

        //    return Json(returnObject);
        //}

        public JsonResult ThayThe(NoiDungBaiDang model)
        {
            var dao = new TuCamDao();
            var listTu = dao.ListTuCam();
            ReturnObject returnObject = new ReturnObject();
            foreach (var item in listTu)
            {
                model.NoiDung = model.NoiDung.Replace(item.Tu, item.ThayThe);
            }

            if (model.IsNoiDung & model.IsTieuDe)
            {
                foreach (var item in listTu)
                {
                    model.NoiDung = model.NoiDung.Replace(item.Tu, item.ThayThe);
                    model.TieuDe = model.TieuDe.Replace(item.Tu, item.ThayThe);
                    model.PhuDe = model.PhuDe.Replace(item.Tu, item.ThayThe);
                }
            }
            else if (model.IsTieuDe)
            {
                foreach (var item in listTu)
                {
                    model.TieuDe = model.TieuDe.Replace(item.Tu, item.ThayThe);
                    model.PhuDe = model.PhuDe.Replace(item.Tu, item.ThayThe);
                }
            }
            else if (model.IsNoiDung)
            {
                foreach (var item in listTu)
                {
                    model.NoiDung = model.NoiDung.Replace(item.Tu, item.ThayThe);
                }
            }

            //returnObject.Message = model.NoiDung;
            return Json(model);
        }

        public JsonResult DinhDangIA(NoiDungBaiDang model)
        {
            //Xóa quảng cáo
            model.NoiDung = model.NoiDung;
            string chuoiDau = "";
            string chuoiGiua = "";
            string chuoiCuoi = "";
            string KetQua = "";
            //foreach (var item in collection)
            //{

            //}
            model.NoiDung = model.NoiDung.Replace("<figure>", "");
            model.NoiDung = model.NoiDung.Replace("</figure>", "");
            int indexImg = model.NoiDung.IndexOf("<img"); ;
            int indexCuoiImg = 0;
            int indexSrc = 0;
            int lengthXau = model.NoiDung.Length;
            while (indexImg > 0)
            {
                chuoiDau = model.NoiDung.Substring(0, indexImg);
                indexCuoiImg = model.NoiDung.IndexOf("/>", indexImg + 1);
                chuoiGiua = model.NoiDung.Substring(indexImg, indexCuoiImg + 1 - indexImg);
                //Xử lý thẻ img
                //Lấy src
                indexSrc = chuoiGiua.IndexOf("src");
                chuoiGiua = "<figure><img style=\"width:100%; height:auto !important\" " + chuoiGiua.Substring(indexSrc, chuoiGiua.IndexOf('"', indexSrc + 5) - indexSrc + 1) + " /></figure>";
                chuoiCuoi = model.NoiDung.Substring(indexCuoiImg + 2);
                model.NoiDung = chuoiCuoi;
                KetQua += chuoiDau + chuoiGiua;
                indexImg = model.NoiDung.IndexOf("<img");
            }
            model.NoiDung = KetQua + model.NoiDung;
            //Xử lý xóa quảng cáo
            indexImg = model.NoiDung.IndexOf("<iframe");

            while (indexImg > 0)
            {
                chuoiDau = model.NoiDung.Substring(0, indexImg);
                indexCuoiImg = model.NoiDung.IndexOf("/iframe>", indexImg + 1);
                //
                chuoiCuoi = model.NoiDung.Substring(indexCuoiImg + 8);
                model.NoiDung = chuoiDau + chuoiCuoi;
                indexImg = model.NoiDung.IndexOf("<iframe");
            }
            model.NoiDung = model.NoiDung.Replace("<ins>", "");
            model.NoiDung = model.NoiDung.Replace("</ins>", "");
            model.NoiDung = model.NoiDung.Replace("<p></p>", "");
            model.NoiDung = model.NoiDung.Replace("\n", "");
            model.NoiDung = model.NoiDung.Replace("<p>&nbsp;</p>", "");
            //model.NoiDung = model.NoiDung.Replace("<p><figure>", "");
            //model.NoiDung = model.NoiDung.Replace("</figure></p>", "");
            //model.NoiDung = model.NoiDung.Replace("<p><img>", "<img>");
            //model.NoiDung = model.NoiDung.Replace("</figure></p>", "</figure>");
            model.NoiDung = model.NoiDung.Replace("<h1>", "<p>").Replace("</h1>", "</p>").Replace("<h2>", "<p>").Replace("</h2>", "</p>").Replace("<h3>", "<p>").Replace("</h3>", "</p>");
            return Json(model);
        }

        //CẬP NHẬT
        [HttpGet]
        public ActionResult UpdatePost(int id)
        {
            var dao = new PostDao();

            var user = dao.ViewDetail(id);
            GetTypeForPost(user.IDTheLoai);
            var model = new BAIDANGVM();
            model.AnhDaiDien = user.AnhDaiDien;
            var DAO = new PostDao();
            model.IDBaiDang = user.IDBaiDang;
            model.TenBaiDang = user.TenBaiDang;
            model.TieuDe = user.TieuDe;
            model.AnhDaiDien = user.AnhDaiDien;
            model.IDTheLoai = user.IDTheLoai;
            model.TrangThaiBaiDang = "chờ duyệt";
            int idtk = int.Parse(Session["USER_ID"].ToString());
            model.IDTaiKhoan = idtk;
            model.NgayDang = DateTime.Now;
            model.NoiDung = user.NoiDung;
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(BAIDANGVM bd)
        {
            if (ModelState.IsValid)
            {
                var model = new BAIDANG();
                model.AnhDaiDien = bd.AnhDaiDien;
                var DAO = new PostDao();
                model.IDBaiDang = bd.IDBaiDang;
                model.TenBaiDang = bd.TenBaiDang;
                model.TieuDe = bd.TieuDe;
                model.AnhDaiDien = bd.AnhDaiDien;
                model.IDTheLoai = bd.IDTheLoai;
                model.IdThe = bd.IDThe;
                model.UrlRequire = RewriteURL.RewriteUrl(bd.TenBaiDang);
                if (bd.IsDangBai)
                    model.TrangThaiBaiDang = "đã duyệt";
                else
                    model.TrangThaiBaiDang = "chờ duyệt";

                int idtk = int.Parse(Session["USER_ID"].ToString());
                model.IDTaiKhoan = idtk;
                model.NgayDang = DateTime.Now;
                model.NoiDung = bd.NoiDung.Replace("<p>&nbsp;</p>", "").Replace("\n", "").Replace("<p><figure>", "").Replace("</figure></p>", "").Replace("<p><img", "<img").Replace("/></p>", "/>");
                bd.IDBaiDang = 0;
                var result = DAO.updatePost(model);
                if (result)
                {

                    //var modelview = DAO.ListAll("", 1, 10);
                    //ViewBag.searchstring = "";

                    //return RedirectToAction("Index", "PostManager", modelview);
                    if (bd.IsDangBai)
                        NewRSS.LoadRSS();
                    return Json(Url.Action("MyPost", "PostManager"));
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
                if (bd.IsDangBai)
                    NewRSS.LoadRSS();
            }
            var dao = new PostDao();
            var modelview2 = dao.ListAll("", 1, 10);
            ViewBag.searchstring = "";
            return Json(Url.Action("MyPost", "PostManager"));
        }

        public ActionResult Preview(long id)
        {
            var dao = new PostDao();
            ViewBag._k_news = dao.NewsFindByID(id);
            //var dao1 = new ViewsDao();
            //dao1.AddViews(id);
            return View();
        }

        [HttpPost]
        public ActionResult XemTruoc(BAIDANGVM bd)
        {
            if (ModelState.IsValid)
            {
                List<BAIDANG> ListBaiDang = new List<BAIDANG>();
                var model = new BAIDANG();
                model.AnhDaiDien = bd.AnhDaiDien;
                var DAO = new PostDao();
                model.IDBaiDang = bd.IDBaiDang;
                model.TenBaiDang = bd.TenBaiDang;
                model.TieuDe = bd.TieuDe;
                model.AnhDaiDien = bd.AnhDaiDien;
                model.IDTheLoai = bd.IDTheLoai;
                model.IdThe = bd.IDThe;
                model.UrlRequire = RewriteURL.RewriteUrl(bd.TenBaiDang);
                if (bd.IsDangBai)
                    model.TrangThaiBaiDang = "đã duyệt";
                else
                    model.TrangThaiBaiDang = "chờ duyệt";

                int idtk = int.Parse(Session["USER_ID"].ToString());
                model.IDTaiKhoan = idtk;
                model.NgayDang = DateTime.Now;
                model.NoiDung = bd.NoiDung;
                bd.IDBaiDang = 0;
                ListBaiDang.Add(model);
                ViewBag._k_news = ListBaiDang;
                //return RedirectToAction("PostWaiting", "PostManager");
            }

            //var dao1 = new ViewsDao();
            //dao1.AddViews(id);
            return PartialView("PreviewPatial");
        }

        public ActionResult UnAcept(long id)
        {
            var dao = new PostDao();
            ViewBag._k_news = dao.NewsFindByID(id);
            return View();
        }
        //DUyệt bài
        public ActionResult DuyetBai(long id)
        {
            var dao = new PostDao();
            dao.DuyetBai(id);
            SetAlert("Duyệt bài đăng thành công", "thanhcong");
            NewRSS.LoadRSS();
            return RedirectToAction("Index", "PostManager");
        }
        //Bỏ duyệt bài
        public ActionResult BoDuyetBai(long id)
        {
            var dao = new PostDao();
            dao.BoDuyetBai(id);
            SetAlert("Bỏ duyệt bài đăng thành công", "thanhcong");
            return RedirectToAction("PostWaiting", "PostManager");
        }
        //Lấy ra danh sách tất cả bài đăng của tôi
        public ActionResult MyPost(string searchString, int page = 1, int pageSize = 5)
        {
            var dbWithDomain = new List<BAIDANGVM>();
            var postdao = new PostDao();
            var userSession = new UserInfo();
            int idtk = int.Parse(Session["USER_ID"].ToString());
            // get all The Loai
            var types = new TypeDao().GetAll();
            var model = postdao.MyPost(searchString, page, pageSize, idtk);
            foreach (var bd in model)
            {
                var domain = types.Where(x => x.IDTheLoai == bd.IDTheLoai).Select(x => x.Domain).FirstOrDefault();
                dbWithDomain.Add(new BAIDANGVM
                {
                    IDBaiDang = bd.IDBaiDang,
                    TenBaiDang = bd.TenBaiDang,
                    NgayDang = bd.NgayDang,
                    TrangThaiBaiDang = bd.TrangThaiBaiDang,
                    AnhDaiDien = bd.AnhDaiDien,
                    Doamain = domain + "doc-bao" + bd.TenBaiDang + "-" + bd.IDBaiDang,
                });
            }
            if (model.Count() == 0)
            {
                //Response.Write("<script>alert('Tài khoản của bạn hiện chưa có bài đăng nào!');window.location.href='Index'; </script>");
                SetAlert("Tài khoản này hiện chưa có bài đăng nào", "canhbao");
            }
            else
                ViewBag.searchstring = searchString;
            var modelRes = dbWithDomain.AsEnumerable();
            return View(modelRes.ToPagedList(page, pageSize));
        }
        //lấy ra danh sách thể loại cho bài đăng
        public void GetTypeForPost(long? selectedid = null)
        {
            var dao = new TypeDao();
            ViewBag.IDTheLoai = new SelectList(dao.ListTypeForCreatePost(), "IDTheLoai", "TenTheLoai", selectedid);
        }
        //Xóa bài đăng

        [HttpGet]
        public ActionResult Delete(int id)
        {
            new PostDao().Delete(id);
            NewRSS.LoadRSS();
            return RedirectToAction("MyPost");
        }
    }
}
