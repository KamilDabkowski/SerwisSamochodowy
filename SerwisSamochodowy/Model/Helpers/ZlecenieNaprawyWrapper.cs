using SerwisSamochodowy.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model.Helpers
{
    internal class ZlecenieNaprawyWrapper
    {
        public ZlecenieNaprawy ZlecenieNaprawy { get; set; }
        public Samochod Samochod { get; set; }

        public static ObservableCollection<ZlecenieNaprawyWrapper> WczytajZlecenia()
        {
            var result = new ObservableCollection<ZlecenieNaprawyWrapper>();

            var zleceniaNaprawy = BazaDanych.ZleceniaNaprawy;
            foreach (var zlecenie in zleceniaNaprawy)
            {
                var samochod = BazaDanych.Samochody.FirstOrDefault(s => s.IdSamochod == zlecenie.IdSamochod);
                result.Add(new ZlecenieNaprawyWrapper { ZlecenieNaprawy = zlecenie, Samochod = samochod });
            }
            return result;
        }
    }
}
