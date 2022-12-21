using Newtonsoft.Json;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
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
        [JsonIgnore]
        private Usterka usterka { get; set; }

        #endregion

        #region methods

        void ZamówCzęści()
        {
            throw new NotImplementedException();
        }

        void PrzyjmijDostarczonaCzesc()
        {
            throw new NotImplementedException();
        }

        void WymieńCzęść()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
