using Newtonsoft.Json;
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
    internal class Czesc
    {
        #region properties

        public int IdCzesc { get; set; }
        public int IdUsterka { get; set; }
        public string Nazwa { get; set; }
        public float Cena { get; set; }
        public string Numer { get; set; }
        public bool Zamowione { get; set; }
        public bool Dostarczone { get; set; }
        public bool Wymienione { get; set; }

        #endregion

        #region methods

        public void ZapiszCzesc()
        {
            if (IdCzesc < 1)
            {
                int id = 0;
                var ostatnie = BazaDanych.Czesci.LastOrDefault();
                if (ostatnie != null)
                    id = ostatnie.IdCzesc;

                IdCzesc = ++id;
                BazaDanych.Czesci.Add(this);
            }
            else
            {
                var zastepowane = BazaDanych.Czesci.FirstOrDefault(c => c.IdCzesc == IdCzesc);
                var index = BazaDanych.Czesci.IndexOf(zastepowane);
                BazaDanych.Czesci[index] = this;
            }
            ObslugaJSON<Czesc>.ZapiszDoJSON(BazaDanych.Czesci, Staticks.PlikCzesci);
        }
        public static ObservableCollection<Czesc> WczytajCzesci(int idUsterki)
        {
            var result = new ObservableCollection<Czesc>(BazaDanych.Czesci.Where(c => c.IdUsterka == idUsterki));
            return result;
        }

        public static ObservableCollection<Czesc> WczytajCzesciZlecenia(int idZlecenie)
        {
            var czesci = new ObservableCollection<Czesc>();
            var usterki = Usterka.WczytajUsterki(idZlecenie);
            foreach (var usterka in usterki)
            {
                var czesciUsterki = new ObservableCollection<Czesc>(Czesc.WczytajCzesci(usterka.IdUsterka));
                foreach (var czesc in czesciUsterki)
                    czesci.Add(czesc);
            }

            return czesci;
        }

        #endregion
    }
}
