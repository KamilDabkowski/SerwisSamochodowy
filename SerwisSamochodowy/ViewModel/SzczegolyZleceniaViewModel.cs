using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class SzczegolyZleceniaViewModel : INotifyPropertyChanged
    {
        public ZlecenieNaprawy ZlecenieNaprawy { get; set; }
        public Usterka WybranaUsterka { get; set; }

        public SzczegolyZleceniaViewModel()
        {
            ZlecenieNaprawy = new ZlecenieNaprawy(new Klient(), new Samochod { Marka = "asdfasdf", Model = "Swiasdfasdfft" }, new List<Usterka>());
        }
        public SzczegolyZleceniaViewModel(ZlecenieNaprawy zlecenieNaprawy)
        {
            this.ZlecenieNaprawy = zlecenieNaprawy;
        }

        #region commands

        private ICommand _loaded;
        public ICommand Loaded
        {
            get
            {
                if (_loaded == null)
                    _loaded = new RelayCommand(
                     (object argument) =>
                     {
                         OnPropertyChanged(nameof(ZlecenieNaprawy));
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _loaded;
            }
        }

        #endregion

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
