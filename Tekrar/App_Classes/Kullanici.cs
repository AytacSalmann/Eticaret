using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tekrar.App_Classes
{
    public class Kullanici
    {
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }
        public string GizliSoru { get; set; }
        public string GizliCevap { get; set; }
        //eklediğimiz 5 property in isimleri kullanici ekle deki name lerle eşleşmesi lazım
    }
}