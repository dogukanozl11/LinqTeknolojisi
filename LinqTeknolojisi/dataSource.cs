using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTeknolojisi
{
    public class dataSource
    {
        List<Musteri> musteriler;
        public dataSource()
        {
            musteriler = new List<Musteri>();
        }
        public List<Musteri> musteriListesi()
        {
            for (int i = 1; i <= 1000; i++)
            {
                Musteri m = new Musteri();
                m.musteriNumara = i;
                m.isim = FakeData.NameData.GetFirstName();
                m.soyisim=FakeData.NameData.GetSurname();
                m.dogumTarih=FakeData.DateTimeData.GetDatetime(new DateTime(1984,01,01),new DateTime(1999,01,01));

                m.ulke = FakeData.PlaceData.GetCountry();
                m.il = FakeData.PlaceData.GetCity();
                m.ilce = FakeData.PlaceData.GetCounty();

                m.eMailAdres = $"{m.isim.ToLower()}.{m.soyisim.ToLower()}@{FakeData.NetworkData.GetDomain()}";
                m.telefonNumara=FakeData.PhoneNumberData.GetPhoneNumber();

                musteriler.Add(m);
            }
            return musteriler;
        }
    }
}
