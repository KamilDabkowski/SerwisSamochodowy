using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region properties
        public List<ZlecenieNaprawy> ZleceniaNaprawy { get; set; }

        public ZlecenieNaprawy WybraneZlecenie { get; set; }

        #endregion

        #region ctor

        public MainWindowViewModel()
        {

        }

        #endregion

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
                         ZleceniaNaprawy = new List<ZlecenieNaprawy>();
                         var zlecenieTestowe = new ZlecenieNaprawy(new Klient(), new Samochod { Marka = "Suzuki", Model = "Swift" }, new List<Usterka>());
                         zlecenieTestowe.Zaplacone = true;
                         zlecenieTestowe.DataPrzyjecia = DateTime.Today;

                         ZleceniaNaprawy.Add(zlecenieTestowe);

                         OnPropertyChanged(nameof(ZleceniaNaprawy));
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _loaded;
            }
        }

        private ICommand _dodajNoweZlecenie;
        public ICommand DodajNoweZlecenie
        {
            get
            {
                if (_dodajNoweZlecenie == null)
                    _dodajNoweZlecenie = new RelayCommand(
                     (object argument) =>
                     {
                         throw new NotImplementedException();
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _dodajNoweZlecenie;
            }
        }

        private ICommand _szczegolyZlecenia;
        public ICommand SzczegolyZlecenia
        {
            get
            {
                if (_szczegolyZlecenia == null)
                    _szczegolyZlecenia = new RelayCommand(
                     (object argument) =>
                     {
                         throw new NotImplementedException();
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _szczegolyZlecenia;
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
