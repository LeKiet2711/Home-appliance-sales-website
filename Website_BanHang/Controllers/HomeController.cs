using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Models;
using System.Data.SqlClient;

using PagedList;
using PagedList.Mvc;

namespace Website_BanHang.Controllers
{
    public class HomeController : Controller
    {
        dbQLBanHangDataContext data=new dbQLBanHangDataContext();

        public List<SanPham> LaySP(int n)
        {
            return data.SanPhams.OrderByDescending(c=>c.NgayCapNhat).Take(n).ToList();
        }

        public ActionResult Index(int ? page,string searching="")
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);

            if (!string.IsNullOrEmpty(searching))
            {
                var search = data.SanPhams.Where(s => s.TenSP.Contains(searching) || s.MoTa.Contains(searching));
                MyViewModel viewModel1 = new MyViewModel
                {
                    SanPhamData = search.ToList(),
                    LoaiSanPhamData = from c in data.LoaiSanPhams select c,
                    SanPhamPagedList = (PagedList<SanPham>)search.ToPagedList(1, pageSize),  // Không cần phân trang cho kết quả tìm kiếm => tạo rỗng hoặc để rỗng trang
                    IsPaging = false
                };
                return View(viewModel1);
            }
            else
            {
                var sanPhams = LaySP(48);
                var sp = sanPhams.ToPagedList(pageNum, pageSize);

                //var tien=from c in data.SanPhams.FirstOrDefault(s => s.MaSP == maSP);

                MyViewModel viewModel = new MyViewModel
                {
                    SanPhamData = sp,
                    LoaiSanPhamData = from c in data.LoaiSanPhams select c,
                    SanPhamPagedList = (PagedList<SanPham>)sp,
                    IsPaging = true
                };
                return View(viewModel);
            }
        }

        //public ActionResult IndexWithPaging(int? page)
        //{
        //    int pageSize = 5;
        //    int pageNum = (page ?? 1);

        //    var sanPhams = LaySP(20);
        //    var sp = sanPhams.ToPagedList(pageNum, pageSize);

        //    MyViewModel viewModel = new MyViewModel
        //    {
        //        LoaiSanPhamData = from c in data.LoaiSanPhams select c,
        //        SanPhamData = sp,
        //        IsPaging = true
        //    };

        //    return View(viewModel);
        //}


        public ActionResult Details(int id)
        {
            var sanpham = from c in data.SanPhams where c.MaSP == id select c;
            return View(sanpham.Single());
        }

        public ActionResult SpTungLoai(int id)
        {
            MyViewModel viewModel = new MyViewModel
            {
                SanPhamData = from c in data.SanPhams where c.MaLoaiSP == id select c,
                LoaiSanPhamData = from c in data.LoaiSanPhams select c
            };
            return View(viewModel);
        }
        [HttpGet]

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Wishlist()
        {
            return View();
        }

        


    }
}