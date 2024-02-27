using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Models;
using System.Data.SqlClient;
using System.IO;

using PagedList;
using PagedList.Mvc;


namespace Website_BanHang.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        dbQLBanHangDataContext data = new dbQLBanHangDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendangnhap = collection["TaikhoanAdmin"];
            var matkhau = collection["MatkhauAdmin"];
            if (String.IsNullOrEmpty(tendangnhap))
            {
                ViewData["Loi1"] = "Vui lòng nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                Admin ad = data.Admins.SingleOrDefault(c => c.TaikhoanAdmin == tendangnhap && c.MatkhauAdmin == matkhau);
                if (ad != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TaiKhoanAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Thông tin đăng nhập không chính xác";
                }
            }

            return View();
        }
        
        public ActionResult Sanpham(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pagesize = 7;
            return View(data.SanPhams.ToList().OrderBy(c=>c.MaSP).ToPagedList(pageNumber,pagesize));
        }

        [HttpGet]
        public ActionResult Themmoisanpham()
        {
            ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(c => c.TenLoai), "MaLoaiSP", "TenLoai");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisanpham(SanPham sanpham, HttpPostedFileBase fileupload)
        {
            ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(c => c.TenLoai), "MaLoaiSP", "TenLoai");

            //if (fileupload == null)
            //{
            //    ViewBag.Thongbao = "Vui lòng chọn ảnh";
            //    return View();
            //}
            //else
            //{
            //if (ModelState.IsValid)
            //{
                        //var fileName = Path.GetFileName(fileupload.FileName);
                        //var path = Path.Combine(Server.MapPath("~/image"), fileName);
                        //if (System.IO.File.Exists(path))
                        //{
                        //    ViewBag.Thongbao = "Ảnh đã tồn tại";
                        //}
                        //else
                        //{
                        //    fileupload.SaveAs(path);
                        //}
                        //sanpham.AnhBia = fileName;
                    data.SanPhams.InsertOnSubmit(sanpham);
                    data.SubmitChanges();
                    
                //}
                return RedirectToAction("Sanpham");
            //}
        }

        public ActionResult Chitietsanpham(int id)
        {
            SanPham sanpham = data.SanPhams.SingleOrDefault(c => c.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {

                return null;
            }
            return View(sanpham);
        }

        [HttpGet]
        public ActionResult Xoasanpham(int id)
        {
            SanPham sanpham = data.SanPhams.SingleOrDefault(c => c.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {

                return null;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("Xoasanpham")]
        public ActionResult Xoa(int id)
        {
            SanPham sanpham = data.SanPhams.SingleOrDefault(c => c.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {

                return null;
            }
            data.SanPhams.DeleteOnSubmit(sanpham);
            data.SubmitChanges();
            return RedirectToAction("Sanpham");
        }

        [HttpGet]
        public ActionResult Suasanpham(int id)
        {
            SanPham sanpham = data.SanPhams.SingleOrDefault(c => c.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {

                return null;
            }
            ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(c => c.TenLoai), "MaLoaiSP", "TenLoai");
            return View(sanpham);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasanpham(SanPham sanpham, HttpPostedFileBase fileupload)
        {
            ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(c => c.TenLoai), "MaLoaiSP", "TenLoai");


            UpdateModel(sanpham);
            data.SubmitChanges();

            
            return RedirectToAction("Sanpham");
            
        }


    }
}