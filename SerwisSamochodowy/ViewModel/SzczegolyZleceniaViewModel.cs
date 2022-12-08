using SerwisSamochodowy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.ViewModel
{
    internal class SzczegolyZleceniaViewModel : INotifyPropertyChanged
    {
        public ZlecenieNaprawy _zlecenieNaprawy { get; set; }

        public SzczegolyZleceniaViewModel()
        {
            _zlecenieNaprawy = new ZlecenieNaprawy(new Klient(), new Samochod { Marka = "asdfasdf", Model = "Swiasdfasdfft" }, new List<Usterka>());
        }
        public SzczegolyZleceniaViewModel(ZlecenieNaprawy zlecenieNaprawy)
        {
            this._zlecenieNaprawy = zlecenieNaprawy;
        }

        #region events

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(params string[] nazwyWlasnosci)
        {
            if (PropertyChanged != null)
            {
                foreach (string nazwaWlasnosci in nazwyWlasnosci)
                    PropertyChanged(this, new PropertyChangedEventArgs(nazwaWlasnosci));
            }
        }

        #endregion
    }
}
