using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Models;
using System.Data;

namespace Website_BanHang.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        dbQLBanHangDataContext data = new dbQLBanHangDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection,KhachHang kh)
        {
            var hoten = collection["HoTen"];
            var tendangnhap = collection["TenDangNhap"];
            var matkhau = collection["MatKhau"];
            var xacnhanmatkhau = collection["XacNhanMatKhau"];
            var dienthoai = collection["DienThoai"];
            var diachi = collection["DiaChi"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }else if (String.IsNullOrEmpty(tendangnhap))
            {
                ViewData["Loi2"] = "Tên đăng nhập không được để trống";
            }else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mật khẩu không được để trống";
            }else if (String.IsNullOrEmpty(xacnhanmatkhau))
            {
                ViewData["Loi4"] = "Vui lòng nhập lại mật khẩu";
            }else
            {
                kh.HoTen = hoten;
                kh.MatKhau = matkhau;
                kh.TaiKhoan = tendangnhap;
                kh.SDT = dienthoai;
                kh.DiaChi = diachi;
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Login");
            }
            return this.Register();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendangnhap = collection["TenDangNhap"];
            var matkhau = collection["MatKhau"];
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
                KhachHang kh = data.KhachHangs.SingleOrDefault(c => c.TaiKhoan == tendangnhap && c.MatKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TenTaiKhoan"] = kh.TaiKhoan;
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.ThongBao = "Thông tin đăng nhập không chính xác";
                }
            }

                return View();
        }


    }
}