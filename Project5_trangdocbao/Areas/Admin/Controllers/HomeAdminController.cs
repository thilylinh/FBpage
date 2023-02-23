using System.Web.Mvc;

namespace Project5_trangdocbao.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
