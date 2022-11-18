using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bài_test.Models;
using Bài_test.Models.ViewModel;
using System.IO;
using PagedList;


namespace Bài_test.Controllers
{
    public class PhimController : Controller
    {
        private BaiTestEntities1 db = new BaiTestEntities1();
        // GET: Phim
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            List<Phim> dsp = db.Phims.ToList();
            return View(dsp);
        }
        public ActionResult Create()
        {
            //Lấy ra ds loại sp
            PhimViewModels pModel = new PhimViewModels();
            return View(pModel);
        }
        [HttpPost]
        public ActionResult Create(PhimViewModels p, HttpPostedFileBase fileUpload)
        {
            //lấy tên file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Tạo đường dẫn lưu file
            var filePath = Path.Combine(Server.MapPath("~/Content/img"), fileName);
            //Lưu ảnh xuống thư mục img
            fileUpload.SaveAs(filePath);
            //kt có ảnh chưa
            if (!System.IO.File.Exists(filePath))
                fileUpload.SaveAs(filePath);


            Phim SanPhamM = new Phim();
            SanPhamM.TenPhim = p.TenPhim;
            //lưu đường dẫn vào database
            SanPhamM.HinhPhim = "Content/img/" + fileName;
            //sanPhamMoi.HinhSP = sp.HinhSP;
            db.Phims.Add(SanPhamM);
            SanPhamM.NumberLike = 0;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Like (int id)
        {
            User kh = (User)Session["Taikhoan"];
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Login");
            }
            Phim p = db.Phims.ToList().Find(s => s.MaPhim == id);
            p.NumberLike += 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Unlike(int id)
        {
            User kh = (User)Session["Taikhoan"];
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Login");
            }
            Phim p = db.Phims.ToList().Find(s => s.MaPhim == id);
            p.NumberLike -= 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}