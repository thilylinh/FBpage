using System.Web.Mvc;

namespace Project5_trangdocbao.Areas.Admin.Controllers
{
    public class HomeUserController : BaseUserController
    {
        //
        // GET: /Admin/HomeUser/

        public ActionResult Index()
        {
            return View();
        }

    }
}
