using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTeknolojisi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dataSource ds = new dataSource();
            List<Musteri> musteriListe = ds.musteriListesi();

            #region LinQ Sorgularında Delegate kullanımı =>

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
