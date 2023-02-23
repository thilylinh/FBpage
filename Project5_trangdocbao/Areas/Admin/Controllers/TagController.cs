using Model.DAO;
using Model.EntityFramework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project5_trangdocbao.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        public JsonResult GetAllTag()
        {
            TheDao theDAO = new TheDao();
            var tags = theDAO.ListThe();
            List<The> tagName = new List<The>();
            foreach(var tag in tags)
            {
                The t = new The();
                t.IdThe = tag.IdThe;
                t.Name = tag.Name;
                tagName.Add(t);
            }
            return Json(tagName);
        }
        [HttpPost]
        public JsonResult AddTag(The the)
        {
            if(the.Name != null)
            {
                var tag = new The();
                tag.Name = the.Name;
                TheDao theDAO = new TheDao();
                theDAO.addThe(tag);
                the.IdThe = tag.IdThe;
            }
            else
            {
                //do something
              // return Json();
            }
           
            return Json(the);
        }

    }
}
