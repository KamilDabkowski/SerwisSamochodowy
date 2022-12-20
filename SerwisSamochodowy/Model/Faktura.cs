using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model
{
    internal class Faktura
    {
        #region fields

        private ZlecenieNaprawy _zlecenieNaprawy;

        #endregion

        #region properties

        public int IdFaktura { get; private set; }
        public int IdZlecenieNaprawy { get; private set; }
        public DateTime DataWystawienia { get; private set; }
        public string Numer { get; private set; }

        #endregion

        #region ctor

        public Faktura()
        {

        }

        #endregion

        #region methods

        void WystawFakture(ZlecenieNaprawy zlecenieNaprawy)
        {
            throw new NotImplementedException();
        }

        void Zaplac()
        {
            throw new NotImplementedException();
        }

        

        #endregion

    }
}
