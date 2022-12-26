using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model
{
    internal class Mechanik
    {
        public int IdMechanik { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public void ZapiszDane()
        {
            if (IdMechanik == 0)
            {
                var ostatni = BazaDanych.Mechanicy.LastOrDefault();
                int id = 0;
                if (ostatni != null)
                    id = ostatni.IdMechanik;
                IdMechanik = ++id;
                BazaDanych.Mechanicy.Add(this);
            }

            ObslugaJSON<Mechanik>.ZapiszDoJSON(BazaDanych.Mechanicy, Staticks.PlikMechanikow);
        }

        public ObservableCollection<Mechanik> WczytajMechanikow()
        {
            return BazaDanych.Mechanicy;
        }

        public Mechanik WyszukajMechanika(int idMechanik)
        {
            return BazaDanych.Mechanicy.FirstOrDefault(m => m.IdMechanik == idMechanik);
        }
    }
}
