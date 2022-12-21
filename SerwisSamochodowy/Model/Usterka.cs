using Newtonsoft.Json;
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
        [JsonIgnore]
        public Mechanik Mechanik { get; set; }
        [JsonIgnore]
        private ZlecenieNaprawy _zlecenieNaprawy { get; set; }
        [JsonIgnore]
        public List<Czesc> Czesci { get; set; }

        #endregion

        #region methods

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
