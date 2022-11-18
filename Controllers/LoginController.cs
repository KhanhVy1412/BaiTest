using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bài_test.Models;
namespace Bài_test.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        BaiTestEntities1  db = new BaiTestEntities1();
        //localhos:xxxx/Login/Index

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collectionn, User kh)
        {
            var hoten = collectionn["HoTen"];
            var taikhoan = collectionn["TaiKhoan"];
            var matkhau = collectionn["MatKhau"];
            var email = collectionn["Email"];
            var diachi = collectionn["DiaChi"];
            var dienthoai = collectionn["DienThoai"];
            var ngaysinh = String.Format("{0:dd/MM/yyyy}", collectionn["NgaySinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Phải nhập đầy đủ";
            }
            else if (String.IsNullOrEmpty(taikhoan))
            {
                ViewData["Loi2"] = "Phải nhập đầy đủ";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập đầy đủ";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi4"] = "Phải nhập đầy đủ";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Phải nhập đầy đủ";
            }
            else if (String.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Loi6"] = "Phải nhập đầy đủ";
            }
            else
            {
                kh.HoTen = hoten;
                kh.TaiKhoan = taikhoan;
                kh.MatKhau = matkhau;
                kh.DiaChi = diachi;
                kh.DienThoai = dienthoai;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                db.Users.Add(kh);
                db.SaveChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collectionn)
        {
            var tendn = collectionn["TenDangNhap"];
            var matkhau = collectionn["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                User kh = db.Users.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;

                    return Redirect("~/Phim/Index");

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Clear();

            return Redirect("~/Phim/Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

    