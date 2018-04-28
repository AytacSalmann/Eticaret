using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tekrar.Controllers
{
    using App_Classes;   //Kullanici adi ve parola bilgileri bu classtan(Kullanici.cs) gelecek 
    using System.Web.Security;
    [Authorize]
    public class UyeController : Controller
    {
        //
        // GET: /Uye/
        [AllowAnonymous]        //Giriş sayfasına da Authorize yaptık ama bu actionlara herkes girebilsin diye yazıyoruz
        public ActionResult GirisYap()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k, string Hatirla)      //Kullanici k -> kullaniciAdi ve parola burdan, string Hatirla ->beni hatırla parametresi burdan gelir
        {
            bool sonuc = Membership.ValidateUser(k.KullaniciAdi, k.Parola);     //ValidateUser metodu kullanıcı adı ve parola doğru mu diye bakıyor, sonucu bool olarak değişkene atıyoruz.
            if (sonuc)
            {
                //böyle bir kullanıcımız varsa
                if (Hatirla == "on")   //beni hatırla seçmişse 
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);    //beni hatırla işaretlemişse kullanıcıyı cooki de tut(true)
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);   //yine giriş yaptır ancak cookide tutma(false) 
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı adı veya Parola Hatalı!";
            }
            return View();
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }

        [AllowAnonymous]
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ParolamiUnuttum(Kullanici k)   //ParolamiUnuttum viewındaki değerler(gizlisoru, gizlicevap ve parola değerleri Kullanici classından gelir, çünkü orda tanımlamıştık)
        {
            MembershipUser mu = Membership.GetUser(k.KullaniciAdi); //kul. adına göre kullanıcıyı seçiyoruz
            if (mu.PasswordQuestion == k.GizliSoru)         //veritabanında tutulan gizli soru ile inputa yazdığı aynımı
            {
                string pwd = mu.ResetPassword(k.GizliCevap);
                mu.ChangePassword(pwd, k.Parola);
                return RedirectToAction("GirisYap");
            }
            else
            {
                ViewBag.Mesaj = "Girilen bilgiler yanlıştır";
            }
            return View();
        }
    }
}