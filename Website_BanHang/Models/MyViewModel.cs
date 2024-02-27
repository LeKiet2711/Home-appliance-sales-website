using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace Website_BanHang.Models
{
    public class MyViewModel
    {
        public IEnumerable<SanPham> SanPhamData { get; set; }
        public IEnumerable<SanPham> SanPhamData2 { get; set; }
        public IEnumerable<LoaiSanPham> LoaiSanPhamData { get; set; }


        public bool IsPaging { get; set; }
        public PagedList<SanPham> SanPhamPagedList { get; set; }
    }
}