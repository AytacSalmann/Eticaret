using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tekrar.Models;

namespace Tekrar.Controllers
{
    using App_Classes;
    [Authorize]
    public class UrunController : Controller
    {
        //
        // GET: /Urun/

        KuzeyYeliContext ctx = new KuzeyYeliContext();
        public ActionResult Index()
        {
            List<Urunler> urunler = ctx.Urunlers.ToList();
            return View(urunler);
        }

        //bu metod urun ekleme sayfasının view ını gösterir
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = ctx.Kategorilers.ToList();
            ViewBag.Tedarikciler = ctx.Tedarikcilers.ToList();
            return View();
        }

        //Urun ekleme sayfasında ki form dan gelecek değerleri post edecek metod.
        [HttpPost]
        public ActionResult UrunEkle(Urunler u)   //bu action a Urunler sınıfından parametreler geliyor(UrunEkle view ındaki input ve selectlerden)
        {
            ctx.Urunlers.Add(u);
            ctx.SaveChanges();
            return RedirectToAction("Index");   //burda sadece action ismini verdiğimiz için Urun controller içindeki Indexe gider
            //("Index","Home") dersek Home içindeki Index e gider.(yönlendirmeyi bu şekilde de ayarlayabiliriz.)
        }

        public ActionResult Sil(int id)
        {
            Urunler u = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Sil(Urunler u)    //bu Sil metodu (int id) parametresi alamaz, üstteki metod ile çakışır(C# kuralı), o yüzden (Urunler u) parametresi veriyoruz
        {
            Urunler urn = ctx.Urunlers.FirstOrDefault(x => x.UrunID == u.UrunID);
            ctx.Urunlers.Remove(urn);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //urun detay sayfasını query string yöntemi ile yapıyoruz
        public ActionResult UrunDetay()
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            string adi = Request.QueryString["adi"].ToString();   //bu şekilde de bir sayfaya birden fazla parametre gönderebiliriz
            Urunler u = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            return View(u);
        }

        [HttpPost]
        public void SepeteAt(int id)
        {
            Sepet s;
            if (Session["AktifSepet"] == null)
            {
                // sepet henüz yoksa
                s = new Sepet();
            }
            else
            {
                //sepet daha önce oluşturulmuşsa
                s = (Sepet)Session["AktifSepet"];
            }
            Urunler u = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            s.Urunler.Add(u);
            Session["AktifSepet"] = s;
        }
    }
}