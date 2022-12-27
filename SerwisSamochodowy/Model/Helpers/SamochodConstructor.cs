using SerwisSamochodowy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model.Helpers
{
    internal class SamochodConstructor
    {
        private readonly string _numerRejestracyjny;
        public SamochodConstructor(string numerRejestracyjny)
        {
            this._numerRejestracyjny = numerRejestracyjny;
        }
        private Samochod WyszukajSamochod()
        {
            var wyszukany = BazaDanych.Samochody.FirstOrDefault(s => s.NumerRejestracyjny == _numerRejestracyjny);
            return wyszukany;
        }

        public Samochod ZnajdzSamochod()
        {
            var samochod = WyszukajSamochod();

            if (samochod is null)
                samochod = ZdefiniujNowySamochod();

            return samochod;
        }

        private Samochod ZdefiniujNowySamochod()
        {
            int id = 0;

            var ostatni = BazaDanych.Samochody.LastOrDefault();
            if (ostatni != null)
                id = ostatni.IdSamochod;

            var samochod = new Samochod(++id, _numerRejestracyjny);

            return samochod;
        }
    }
}
