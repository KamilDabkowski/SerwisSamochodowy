using Newtonsoft.Json;
using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model
{
    internal class Usterka
    {
        #region properties

        public int IdUsterka { get; set; }
        public int IdZlecenieNaprawy { get; set; }
        public int IdMechanik { get; set; }
        public string Opis { get; set; }
        public bool Naprawa { get; set; }
        public bool Naprawione { get; set; }
        public float Koszt { get; set; }

        #endregion

        #region methods

        public void ZapiszUsterke()
        {
            if (IdUsterka < 1)
            {
                int id = 0;
                var ostatnie = BazaDanych.Usterki.LastOrDefault();
                if (ostatnie != null)
                    id = ostatnie.IdUsterka;

                IdUsterka = ++id;
                BazaDanych.Usterki.Add(this);
            }
            else
            {
                var zastepowane = BazaDanych.Usterki.FirstOrDefault(w => w.IdUsterka == IdUsterka);
                var index = BazaDanych.Usterki.IndexOf(zastepowane);
                BazaDanych.Usterki[index] = this;
            }
            ObslugaJSON<Usterka>.ZapiszDoJSON(BazaDanych.Usterki, Staticks.PlikUsterek);
        }

        void PrzydzielMechanika(Mechanik mechanik)
        {
            throw new NotImplementedException();
        }

        void NaprawUsterke()
        {
            throw new NotImplementedException();
        }

        bool PrzypiszCzesci()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
