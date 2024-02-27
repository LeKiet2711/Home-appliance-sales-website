using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website_BanHang.Models
{
    public class Giohang
    {
        dbQLBanHangDataContext data = new dbQLBanHangDataContext();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public Double dDonGia { get; set; }
        public int iSoluong { get; set; }
        public Double dThanhTien()
        {
             return iSoluong * dDonGia;
        }

        public Giohang(int MaSP)
        {
            iMaSP = MaSP;
            SanPham sanpham = data.SanPhams.Single(c => c.MaSP == iMaSP);
            sTenSP = sanpham.TenSP;
            sAnhBia = sanpham.AnhBia;
            dDonGia = double.Parse(sanpham.GiaBan.ToString());
            iSoluong = 1;
        }

    }
}