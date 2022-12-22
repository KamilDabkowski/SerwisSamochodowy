using SerwisSamochodowy.Model;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Common
{
    internal static class BazaDanych
    {
        public static ObservableCollection<ZlecenieNaprawy> ZleceniaNaprawy;
        public static ObservableCollection<Mechanik> Mechanicy;
        public static ObservableCollection<Kierownik> Kierownicy;
        public static ObservableCollection<Klient> Klienci;
        public static ObservableCollection<Samochod> Samochody;
        public static ObservableCollection<Usterka> Usterki;
        public static ObservableCollection<Faktura> Faktury;
        public static ObservableCollection<Czesc> Czesci;

        public static void WczytajDaneZPlikow()
        {
            Czesci = ObslugaJSON<Czesc>.PobierzDaneZJSON(Staticks.PlikCzesci);
            Faktury = ObslugaJSON<Faktura>.PobierzDaneZJSON(Staticks.PlikFaktur);
            Kierownicy = ObslugaJSON<Kierownik>.PobierzDaneZJSON(Staticks.PlikKierownikow);
            Klienci = ObslugaJSON<Klient>.PobierzDaneZJSON(Staticks.PlikKlientow);
            Mechanicy = ObslugaJSON<Mechanik>.PobierzDaneZJSON(Staticks.PlikMechanikow);
            Samochody = ObslugaJSON<Samochod>.PobierzDaneZJSON(Staticks.PlikSamochodow);
            Usterki = ObslugaJSON<Usterka>.PobierzDaneZJSON(Staticks.PlikUsterek);
            ZleceniaNaprawy = ObslugaJSON<ZlecenieNaprawy>.PobierzDaneZJSON(Staticks.PlikZlecenNaprawy);
        }
    }
}
