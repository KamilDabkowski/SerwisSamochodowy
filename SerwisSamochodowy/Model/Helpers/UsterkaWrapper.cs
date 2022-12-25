using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model.Helpers
{
    internal class UsterkaWrapper : Usterka
    {
        public Mechanik Mechanik { get; set; }
        public ObservableCollection<Czesc> Czesci { get; set; }

    }
}
