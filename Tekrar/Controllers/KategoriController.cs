using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tekrar.Models;

namespace Tekrar.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        //
        // GET: /Kategori/
        KuzeyYeliContext ctx = new KuzeyYeliContext();
        public ActionResult Index()
        {
            List<Kategoriler> ktg = ctx.Kategorilers.ToList();
            return View(ktg);
        }

        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Kategoriler k)
        {
            ctx.Kategorilers.Add(k);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //silme işlemini ajax ile yapacağımız için iki tane Sil metodu yazmıyacaz, bir tane [HttpPOst] action u yeterli
        [HttpPost]
        public void Sil(int id)         //bu metod geriye hiç birşey döndürmez 
        {
            Kategoriler k = ctx.Kategorilers.FirstOrDefault(x => x.KategoriID == id);
            ctx.Kategorilers.Remove(k);
            ctx.SaveChanges();
        }
    }
}