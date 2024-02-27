using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Website_BanHang.Models;

namespace Website_BanHang.Controllers
{
    public class YeuthichController : Controller
    {
        // GET: Yeuthich
        dbQLBanHangDataContext data = new dbQLBanHangDataContext();

        public List<Giohang> Laydsyeuthich()
        {
            List<Giohang> lstyeuthich = Session["Giohang"] as List<Giohang>;
            if (lstyeuthich == null)
            {
                lstyeuthich = new List<Giohang>();
                Session["Giohang"] = lstyeuthich;
            }
            return lstyeuthich;
        }

        public ActionResult Themdsyeuthich(int iMaSP, string strURL)
        {
            List<Giohang> lstyeuthich = Laydsyeuthich();
            Giohang sanpham = lstyeuthich.Find(c => c.iMaSP == iMaSP);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMaSP);
                lstyeuthich.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        public ActionResult Xoadsyeuthich(int iMaSP)
        {
            List<Giohang> lstyeuthich = Laydsyeuthich();
            Giohang sanpham = lstyeuthich.SingleOrDefault(c => c.iMaSP == iMaSP);
            if (sanpham != null)
            {
                lstyeuthich.RemoveAll(c => c.iMaSP == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstyeuthich.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult DsYeuthich()
        {
            List<Giohang> lstyeuthich = Laydsyeuthich();
            if (lstyeuthich.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(lstyeuthich);
        }

    }
}