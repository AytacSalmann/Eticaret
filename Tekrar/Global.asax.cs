using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tekrar
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //Application icin bu metodları biz yazıyoruz. Session_Start ve Session_End bizim ürettiğimiz metoddlar değil zaten mvc de var ama artık 
        //mvc de gelmiyor biz o yüzden biz burda bu metodları çağırdık.
        //Global.asax proje ilk açıldığında çalışan bir dosyadır.
        //Session start metodunun çalışması demek siteye birisinin girdiği anlamına gelir, SessionEnd de ise siteden birisi çıktığı zaman çalışır
        //ortak bir yerde bir sayac tutacaz ve sessionstart ile sessionend çalışma durumuna göre sitedeki kişileri bulacaz 
        //siteye giriş anında(session oluşursa) çalışır
        protected void Session_Start()
        {
            //application daha önce oluşmamış ise
            if (Application["AktifKullanici"] == null)  //Application herkes buraya bağlıdır(session un tam tersi)
            {
                int sayac = 1;
                Application["AktifKullanici"] = sayac;
            }
            else
            {
                int sayac = (int)Application["AktifKullanici"];
                sayac++;
                Application["AktifKullanici"] = sayac;
            }

            //toplam kaç kisi siteye girmiş
            if (Application["ToplamZiyaretci"] == null)
            {
                int sayac = 1;
                Application["ToplamZiyaretci"] = sayac;
            }
            else
            {
                int sayac = (int)Application["ToplamZiyaretci"];
                sayac++;
                Application["ToplamZiyaretci"] = sayac;
            }
        }

        //siteden çıkınca(logout olunca) çalışır
        protected void Session_End()
        {
            //burası çalışıyorsa demekki öncesinde session_Start çalışmış demektir
            int sayac = (int)Application["AktifKullanici"];
            sayac--;
            Application["AktifKullanici"] = sayac;
        }
    }
}
