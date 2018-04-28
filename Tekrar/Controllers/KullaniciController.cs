using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tekrar.App_Classes;

namespace Tekrar.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        //
        // GET: /Kullanici/
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();  //GetAllUsers metodu tüm kullanıcıları çekiyor, bunları users değişkenine atıyoruz
            return View(users);
        }

        [AllowAnonymous]
        public ActionResult Ekle()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Ekle(Kullanici k)  //Kullanici bizim oluşturduğumuz class tan geliyor
        {
            MembershipCreateStatus durum;  //inputlarda ki geçersiz olan şeyleri tutar(geçersiz email, tekrarlanan email parola, kısa parola vs gibi)
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);

            string hataMesajı = "";
            switch (durum)
            {
                case MembershipCreateStatus.DuplicateEmail:
                    hataMesajı += "Kullanımış mail adresi girildi.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    hataMesajı += "Kullanımış kullanıcı key hatası.";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    hataMesajı += "Kullanımış kullanıcı adı.";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    hataMesajı += "Geçersiz gizli cevap.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    hataMesajı += "Geçersiz mail adresi.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    hataMesajı += "Geçersiz parola.";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    hataMesajı += "Geçersiz kullanıcı key hatası.";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    hataMesajı += "Geçersiz gizli soru.";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    hataMesajı += "Geçersiz kullanıcı adı.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    hataMesajı += "Üye yönetimi sağlayıcısı hatası.";
                    break;
                case MembershipCreateStatus.Success:

                    break;
                case MembershipCreateStatus.UserRejected:
                    hataMesajı += "Kullanıcı engel hatası.";
                    break;
                default:
                    break;
            }

            ViewBag.Mesaj = hataMesajı;
            if (durum == MembershipCreateStatus.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]      //yetkilendirme yapıyoruz, sadece adminler bu işlemi yapabilsin.Giriş yapmış ve rolü adminse be sayfayı çalıştırabilir
        public ActionResult RolAta()
        {
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.Kullanicilar = Membership.GetAllUsers();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolAta(string KullaniciAdi, string RolAdi)
        {
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string UyeRolleri(string kullaniciAdi)
        {
            List<string> roller = Roles.GetRolesForUser(kullaniciAdi).ToList();
            string rol = "";
            foreach (string r in roller)
            {
                rol += r + "-";   //kullanıcının tüm rollerini aralarına - koyarak yan yana yazar
            }
            rol = rol.Remove(rol.Length - 1, 1);    //en sona konulacak - işaretini siler
            return rol;
        }
    }
}