using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model
{
    internal class Samochod
    {
        public int IdSamochod { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Silnik { get; set; }
        public int RokProdukcji { get; set; }
        public string NumerRejestracyjny { get; set; }

        public Samochod(int idSamochod, string numerRejestracyjny)
        {
            this.IdSamochod = idSamochod;
            this.NumerRejestracyjny = numerRejestracyjny;
        }

        public void ZapiszSamochod()
        {
            BazaDanych.Samochody.Add(this);
            ObslugaJSON<Samochod>.ZapiszDoJSON(BazaDanych.Samochody, Staticks.PlikSamochodow);
        }
    }
}
