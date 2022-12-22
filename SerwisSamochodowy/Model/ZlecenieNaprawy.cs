using Newtonsoft.Json;
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
        [JsonIgnore]
        public Samochod Samochod { get; set; }
        public DateTime DataPrzyjecia { get; set; }
        public bool Naprawione { get; set; }
        public bool ZgloszoneZakonczenieNapraw { get; set; }
        public DateTime DataOdbioru { get; set; }
        public bool Zaplacone { get; set; }
        public bool Odebrane { get; set; }
        [JsonIgnore]
        public List<Usterka> Usterki { get; set; }
        [JsonIgnore]
        public Faktura Faktura { get; set; }

        #endregion

        #region ctor

        public ZlecenieNaprawy(int idKlienta, Samochod samochod, List<Usterka> usterki)
        {
            DataPrzyjecia = DateTime.Now.Date;
            this.IdKlient = idKlienta;
            this.Samochod = samochod;
            this.Usterki = usterki;
        }

        #endregion

        #region methods

        void ZglosZakonczenieNapraw()
        {
            throw new NotImplementedException();
        }

        void WydajSamochod()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
