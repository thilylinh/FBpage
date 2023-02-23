using System.Collections.Generic;

namespace Project5_trangdocbao.Areas.Admin.ViewModels
{
    public class NoiDungBaiDang
    {
        public string NoiDung { get; set; }
        public string TieuDe { get; set; }
        public string PhuDe { get; set; }
        public bool IsNoiDung { get; set; }
        public bool IsTieuDe { get; set; }
        public List<string> TuCams { get; set; }
    }
}