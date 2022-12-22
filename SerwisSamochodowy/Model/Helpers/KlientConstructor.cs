using SerwisSamochodowy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model.Helpers
{
    internal class KlientConstructor
    {
        private string _numerTelefonu;
        public KlientConstructor(string numerTelefonu)
        {
            this._numerTelefonu = numerTelefonu;
        }
        private Klient WyszukajKlienta()
        {
            var wyszukany = BazaDanych.Klienci.FirstOrDefault(k => k.NumerTelefonu == _numerTelefonu);
            return wyszukany;
        }

        public int ZnajdzIdKlienta()
        {
            var klient = WyszukajKlienta();

            if (klient is null)
                klient = ZdefiniujNowegoKlienta();

            return klient.IdKlient;
        }

        private Klient ZdefiniujNowegoKlienta()
        {
            int id = 0;

            var ostatni = BazaDanych.Klienci.LastOrDefault();
            if (ostatni != null)
                id = ostatni.IdKlient;

            var klient = new Klient(++id, _numerTelefonu);

            BazaDanych.Klienci.Add(klient);
            ObslugaJSON<Klient>.ZapiszDoJSON(BazaDanych.Klienci, Staticks.PlikKlientow);
            return klient;
        }
    }
}
