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
    internal class Faktura
    {
        #region properties

        public int IdFaktura { get; private set; }
        public int IdZlecenia { get; private set; }
        public DateTime DataWystawienia { get; private set; }
        public int Numer { get; private set; }

        public Dictionary<string, float> Koszty { get; private set; }
        public float Suma { get; private set; }

        #endregion

        #region ctor

        public Faktura(int idFaktura, int idZlecenia, DateTime dataWystawienia, int numer, Dictionary<string, float> koszty, float suma)
        {
            this.IdFaktura = idFaktura;
            this.IdZlecenia = idZlecenia;
            this.DataWystawienia = dataWystawienia;
            this.Numer = numer;
            this.Koszty = koszty;
            this.Suma = suma;
        }

        #endregion

        #region methods



        private void WygenerujPlik()
        {

        }

        public Faktura WczytajFakture()
        {
            throw new NotImplementedException();
        }

        

        #endregion

    }
}
