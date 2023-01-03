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
    internal class ZlecenieNaprawy
    {
        #region properties

        public int IdZlecenie { get; set; }
        public int IdKlient { get; set; }
        public int IdSamochod { get; set; }
        public DateTime DataPrzyjecia { get; private set; }
        public bool Naprawione { get; set; }
        public bool ZgloszoneZakonczenieNapraw { get; set; }
        public DateTime DataOdbioru { get; private set; }
        public bool Zaplacone { get; set; }
        public bool Odebrane { get; set; }

        #endregion

        #region ctor

        public ZlecenieNaprawy(int idKlienta, int idSamochod)
        {
            DataPrzyjecia = DateTime.Now.Date;
            this.IdKlient = idKlienta;
            this.IdSamochod = idSamochod;
        }

        #endregion

        #region methods

        public void ZapiszZlecenie()
        {
            if (IdZlecenie < 1)
            {
                int id = 0;
                var ostatnie = BazaDanych.ZleceniaNaprawy.LastOrDefault();
                if (ostatnie != null)
                    id = ostatnie.IdZlecenie;

                IdZlecenie = ++id;
                BazaDanych.ZleceniaNaprawy.Add(this);
            }
            else
            {
                var zastepowaneZlecenie = BazaDanych.ZleceniaNaprawy.FirstOrDefault(w => w.IdZlecenie == IdZlecenie);
                var index = BazaDanych.ZleceniaNaprawy.IndexOf(zastepowaneZlecenie);
                BazaDanych.ZleceniaNaprawy[index] = this;
            }
            ObslugaJSON<ZlecenieNaprawy>.ZapiszDoJSON(BazaDanych.ZleceniaNaprawy, Staticks.PlikZlecenNaprawy);
        }

        #endregion

    }
}
