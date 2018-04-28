using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tekrar.Controllers
{
    using Models;
    using App_Classes;
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.AktifKullanici = HttpContext.Application["AktifKullanici"]; //kodları global.asax ta
            ViewBag.ToplamZiyaretci = HttpContext.Application["ToplamZiyaretci"]; //kodları global.asax ta
            return View();
        }

        //cookie yöntemi
        public ActionResult CookieAta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CookieAta(string CookieAdi, string CookieDegeri)
        {
            HttpCookie ck = new HttpCookie(CookieAdi);
            ck.Value = CookieDegeri;
            ck.Expires = DateTime.Now.AddDays(1);   //oluşturulan cookienin süresidir.(oluştuğu tarihten sonra bir gün cookie browserda kalsın diyoruz.)
            Response.Cookies.Add(ck);
            return View();
        }

        //oluşturulan cookinin okunması
        public ActionResult CookieOku()
        {
            string deger = Request.Cookies["Grup Kodu"].Value;
            ViewBag.Cerez = deger;
            return View();
        }

        public ActionResult Sepetim()
        {
            List<Urunler> urunler = new List<Urunler>();
            if (Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)Session["AktifSepet"];
                urunler = s.Urunler;
            }
            return View(urunler);
        }
    }
}