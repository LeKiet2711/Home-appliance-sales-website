using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Models;
using System.Data.SqlClient;

namespace Website_BanHang.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbQLBanHangDataContext data = new dbQLBanHangDataContext();

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang == null)
            {
                lstgiohang = new List<Giohang>();
                Session["Giohang"] = lstgiohang;
            }
            return lstgiohang;
        }

        public ActionResult ThemGiohang(int iMaSP,string strURL)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sanpham = lstgiohang.Find(c => c.iMaSP == iMaSP);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMaSP);
                lstgiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        public ActionResult XoaGiohang(int iMaSP)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sanpham = lstgiohang.SingleOrDefault(c=>c.iMaSP==iMaSP);
            if (sanpham != null)
            {
                lstgiohang.RemoveAll(c=>c.iMaSP==iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sanpham = lstgiohang.SingleOrDefault(c => c.iMaSP == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());

            }
            return RedirectToAction("Giohang");
        }


        private double Tongsoluong()
        {
            int iTongsoluong = 0;
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang != null)
            {
                iTongsoluong = lstgiohang.Sum(c => c.iSoluong);
            }
            return iTongsoluong;
        }

        private double TongTien()
        {
            double dTongTien = 0;
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang != null)
            {
                dTongTien = lstgiohang.Sum(n=>n.iSoluong*n.dDonGia);
            }
            return dTongTien;
        }


        public ActionResult GioHang()
        {
            List<Giohang> lstgiohang = Laygiohang();
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.TongTien = TongTien();
            Session["Soluong"]= Tongsoluong();
            return View(lstgiohang);
        }
        
        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["Taikhoan"]==null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login","Account");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index","Home");
            }

            List<Giohang> lstgiohang = Laygiohang();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.TongTien = TongTien();
            return View(lstgiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"]; //Lấy khách hàng đang login dựa vào AccountController
            List<Giohang> gh = Laygiohang();
            

            dh.MaKH = kh.MaKH;
            dh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yy}", collection["Ngaygiao"]);
            dh.NgayGiao = DateTime.Parse(ngaygiao);
            dh.TinhTrang = 0;
            dh.ThanhToan = "";
            data.DonHangs.InsertOnSubmit(dh);
            data.SubmitChanges();

            foreach (var item in gh)
            {
                ChiTietDonHang ct = new ChiTietDonHang();
                ct.MaDonHang = dh.MaDonHang;
                ct.MaSP = item.iMaSP;
                ct.SoLuong = item.iSoluong;
                ct.DonGia = (decimal)item.dDonGia;
                data.ChiTietDonHangs.InsertOnSubmit(ct);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhan", "Giohang");
        }

        public ActionResult Xacnhan()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}