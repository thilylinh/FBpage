
using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Model.DAO
{
    public class TuCamDao
    {
        DocBaoDataContext db = null;


        public TuCamDao()
        {
            db = new DocBaoDataContext();
        }

        public List<TuCam> ListTuCam()
        {
            return db.TuCams.ToList();
        }


        public IEnumerable<TuCam> ListAll(int page, int pageSize)
        {
            return db.TuCams.OrderByDescending(i => i.Id).ToPagedList(page, pageSize);
        }

        public long addTuCam(TuCam tk)
        {
            db.TuCams.Add(tk);
            db.SaveChanges();
            return tk.Id;
        }


        public bool Delete(int ID)
        {
            try
            {
                var acc = db.TuCams.SingleOrDefault(x => x.Id == ID);
                db.TuCams.Remove(acc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
