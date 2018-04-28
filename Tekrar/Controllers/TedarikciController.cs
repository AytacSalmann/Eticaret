using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tekrar.Models;

namespace Tekrar.Controllers
{
    [Authorize]
    public class TedarikciController : Controller
    {
        //
        // GET: /Tedarikci/
        KuzeyYeliContext ctx = new KuzeyYeliContext();
        public ActionResult Index()
        {
            List<Tedarikciler> data = ctx.Tedarikcilers.ToList();
            return View(data);
        }

        [HttpPost]
        public string Sil(int id)
        //silme işleminin başarılı olup olmadığını ajax a göndereceğimiz için string tanımladık
        {
            Tedarikciler tedarikci = ctx.Tedarikcilers.FirstOrDefault(x => x.TedarikciID == id);
            ctx.Tedarikcilers.Remove(tedarikci);
            try
            {
                //silme işlemi başarılı ise burası geçerli
                ctx.SaveChanges();
                return "başarılı";
            }
            catch (Exception)
            {
                //silme işlemi başarısız ise burası geçerli
                return "hatalı";
            }
        }
    }
}