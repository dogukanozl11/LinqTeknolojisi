using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTeknolojisi
{
    internal class Program
    {
        #region Uzun yollar için Metotlar
        static bool funcDelefateKullanimi1(Musteri m)
        {
            if (m.isim[0] == 'A')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool predicateDelegateMetot(Musteri m)
        {
            if (m.dogumTarih.Year > 1990)
            {
                return true;
            }
            else
                return false;
        }

        static void musteriListele(Musteri m)
        {
            Console.WriteLine(m.isim + " " + m.soyisim);
        }
        #endregion
        static void Main(string[] args)
        {
            dataSource ds = new dataSource();
            List<Musteri> musteriListe = ds.musteriListesi();

            #region Linq İnceleme ve Ara Ödevler

            // Müşteri listesi içerisinde bulunan  kayıtlardan ismi a ile başlayan
            // soyismi değerinin içinde e olan ve doğum yılın 1985 den büyük olan kayıtları getirin.

            var odevAlistirma1 = from I in musteriListe
                                 where
                                 I.isim.StartsWith("A") &&
                                 I.soyisim.Contains("e") &&
                                 I.dogumTarih.Year > 1985
                                 select I;
            Console.WriteLine(odevAlistirma1.Count());

            var odevAlistirma2 = musteriListe.Where
                (I => I.isim.StartsWith("A") && 
                I.soyisim.Contains("e") && 
                I.dogumTarih.Year > 1985).Select(I => I);
            Console.WriteLine(odevAlistirma2.Count());

            #endregion

            #region Action Delegate
            //En uzun yol
            Action<Musteri> actionMusteri = new Action<Musteri>(musteriListele);
            musteriListe.ForEach(actionMusteri);

            musteriListe.ForEach(new Action<Musteri>(musteriListele));

            musteriListe.ForEach(delegate (Musteri m) { Console.WriteLine(m.isim + " " + m.soyisim); });

            musteriListe.ForEach((Musteri m) => { Console.WriteLine(m.isim + " " + m.soyisim); });

            //en kısa
            musteriListe.ForEach((m) => { Console.WriteLine(m.isim + " " + m.soyisim); });


            #endregion

            #region Linq sorgularında Delegate kullanımı Predicate Delegate
            //En uzun yol.
            Predicate<Musteri> predicate = new Predicate<Musteri>(predicateDelegateMetot);
            var DelegateKullanimiPredicate1 = musteriListe.FindAll(predicate);
            // 2.
            var DelegateKullanimiPredicate2 = musteriListe.FindAll(new Predicate<Musteri>(predicateDelegateMetot));

            var DelegateKullanimiPredicate3 = musteriListe.FindAll(delegate (Musteri m) { return m.dogumTarih.Year > 1990; });

            var DelegateKullanimiPredicate4 = musteriListe.FindAll((Musteri m) => { return m.dogumTarih.Year > 1990; });

            var DelegateKullanimiPredicate5 = musteriListe.FindAll((m) => { return m.dogumTarih.Year > 1990; });

            var DelegateKullanimiPredicate6 = musteriListe.FindAll(m => m.dogumTarih.Year > 1990);


            #endregion

            #region LinQ Sorgularında Delegate kullanımı =>

            var DelegateKullanimi1 = musteriListe.Where(I => I.isim.StartsWith("A"));

            Func<Musteri, bool> funcDelegate1 = new Func<Musteri, bool>(funcDelefateKullanimi1);
            var DelegateKullanimi2 = musteriListe.Where(funcDelegate1);
            DelegateKullanimi2 = musteriListe.Where(funcDelefateKullanimi1);

            var DelegateKullanimi3 = musteriListe.Where(new Func<Musteri, bool>(funcDelefateKullanimi1));

            var DelegateKullanimi4 = musteriListe.Where(delegate (Musteri m) { return m.isim[0] == 'A' ? true : false; });

            var DelegateKullanimi5 = musteriListe.Where((Musteri m) => { return m.isim[0] == 'A' ? true : false; });

            var DelegateKullanimi6 = musteriListe.Where((m) => { return m.isim[0] == 'A' ? true : false; });

            var DelegateKullanimi7 = musteriListe.Where(m => m.isim[0] == 'A'); // =>


            #endregion

            #region Alistirmalar

            // 1 : musteriler içerisinde ülke değeri A ile başlayan musterileri Linq to metot kullanarak bulalım
            // StartsWith : ilk değer için
            // EndsWith :  son değer için
            // Contains : içinde var olna için 
            IEnumerable<Musteri> musteriListesiAlıstırma1 = musteriListe.Where(I => I.ulke.StartsWith("A"));
            Console.WriteLine(musteriListesiAlıstırma1.Count());

            // 2 :musteriler listesi içerisindeki kayıtlardan isminin içerisinde b harfi geçen ve ülke değeri içinde A harfi geçen müşterilerin listesini getirin.

            var musteriListesiAlistirma2 = musteriListe.Where(I => I.isim.ToLower().Contains("b") && I.ulke.ToLower().Contains("a")).ToList();
            Console.WriteLine(musteriListesiAlistirma2.Count());

            // 3: musteriler listesi içerisindeki kayıtlardan doğum yılı 1990 dan büyük olan ve isminin içerisinde a harfi geçen müşterileri 2. yol olan linq to query ile bulalım.
            var musteriListesiArlisitirma3 = from I in musteriListe
                                             where I.dogumTarih.Year > 1990 && I.isim.ToLower().Contains("a")
                                             select I;
            Console.WriteLine(musteriListesiArlisitirma3.Count());


            // 4 : Musteriler listesi içerisindeki kayıtlardan doğum yılı 1990 dan büyük olan veya isminin içerisinde a harfi geçen müşterileri 2.yol olan linq to query ile bulalım.

            var musteriListesiArlisitirma4 = from I in musteriListe
                                             where I.dogumTarih.Year > 1990 || I.isim.ToLower().Contains("a")
                                             select I;
            Console.WriteLine(musteriListesiArlisitirma4.Count());

            #endregion

            #region Linq Sorgulama Çeşitleri

            // I.Yol

            int toplamMusteriAdet = musteriListe.Where(I => I.isim.StartsWith("A")).Count();

            var toplamMusteriAdet2 = (from I in musteriListe
                                      where I.isim.StartsWith("A")
                                      select I).Count();

            #endregion

            #region Baslarken
            // A ile başlayan müşterilerin adedini verin.

            int bulunanToplam = 0;
            for (int i = 0; i < musteriListe.Count; i++)
            {
                if (musteriListe[i].isim[0] == 'A')
                {
                    bulunanToplam++;
                }
            }
            Console.WriteLine("Liste içerisinde isim değeri A ile başlayan kayıt sayısı : {0}", bulunanToplam);
            Console.WriteLine(musteriListe.Count);

            bulunanToplam = 0;

            bulunanToplam = musteriListe.Where(i => i.isim.StartsWith("A")).Count();
            Console.ReadLine();
            #endregion
        }
    }
}
