using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model
{
    internal class Klient
    {
        public int IdKlient { get; set; }
        public string NumerTelefonu { get; set; }

        public Klient(int idKlient, string numerTelefonu)
        {
            this.IdKlient = idKlient;
            this.NumerTelefonu = numerTelefonu;
        }



    }
}
