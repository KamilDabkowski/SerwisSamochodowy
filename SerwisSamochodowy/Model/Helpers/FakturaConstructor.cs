using SerwisSamochodowy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model.Helpers
{
    internal static class FakturaConstructor
    {
        public static Faktura WystawFakture(int idZlecenieNaprawy)
        {
            var dataWystawienia = DateTime.Now.Date;
            int idFaktura = 0;
            int numer = 0;

            var ostatni = BazaDanych.Faktury.LastOrDefault();
            if (ostatni != null)
            {
                idFaktura = ostatni.IdFaktura;
                numer = ostatni.Numer;
            }
            else
            {
                idFaktura = 0;
                numer = 0;
            }

            Dictionary<string, float> koszty = PrzypiszKoszty(idZlecenieNaprawy);

            var suma = koszty.Sum(k => k.Value);

            var result = new Faktura(++idFaktura, idZlecenieNaprawy, dataWystawienia, ++numer, koszty, suma);

            BazaDanych.Faktury.Add(result);
            ObslugaJSON<Faktura>.ZapiszDoJSON(BazaDanych.Faktury, Staticks.PlikFaktur);

            return result;
        }

        private static Dictionary<string, float> PrzypiszKoszty(int idZlecenieNaprawy)
        {
            var koszty = new Dictionary<string, float>();

            var usterki = Usterka.WczytajUsterki(idZlecenieNaprawy);
            foreach (var usterka in usterki)
            {
                koszty.Add(usterka.Opis, usterka.Koszt);
            }

            var czesci = Czesc.WczytajCzesciZlecenia(idZlecenieNaprawy);
            foreach (var czesc in czesci)
            {
                koszty.Add(czesc.Nazwa + " " + czesc.Numer, czesc.Cena);
            }

            return koszty;
        }
    }
}
