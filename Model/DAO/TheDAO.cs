using Model.EntityFramework;
using System.Collections.Generic;
using System.Linq;


namespace Model.DAO
{
    public class TheDao
    {
        DocBaoDataContext db = null;


        public TheDao()
        {
            db = new DocBaoDataContext();
        }

        public List<The> ListThe()
        {
            return db.Thes.ToList();
        }


        //public IEnumerable<TuCam> ListAll(int page, int pageSize)
        //{
        //    return db.TuCams.OrderByDescending(i => i.Id).ToPagedList(page, pageSize);
        //}

        public long addThe(The the)
        {
            db.Thes.Add(the);
            db.SaveChanges();
            return the.IdThe;
        }

        //public bool Delete(int ID)
        //{
        //    try
        //    {
        //        var acc = db.TuCams.SingleOrDefault(x => x.Id == ID);
        //        db.TuCams.Remove(acc);
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}

    }
}
