using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model
{
    internal class Kierownik
    {
        public int IdKierownik { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }

        public void ZapiszDane()
        {
            if (IdKierownik == 0)
            {
                var ostatni = BazaDanych.Kierownicy.LastOrDefault();
                int id = 0;
                if (ostatni != null)
                    id = ostatni.IdKierownik;
                IdKierownik = ++id;
                BazaDanych.Kierownicy.Add(this);
            }

            ObslugaJSON<Kierownik>.ZapiszDoJSON(BazaDanych.Kierownicy, Staticks.PlikKierownikow);
        }
    }
}
