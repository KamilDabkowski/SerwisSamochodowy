using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
using SerwisSamochodowy.View;
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

        #region properties

        public ZlecenieNaprawy ZlecenieNaprawy { get; set; }
        public Usterka WybranaUsterka { get; set; }

        ObservableCollection<Czesc> Czesci { get; set; }

        #endregion

        #region ctor

        public SzczegolyZleceniaViewModel()
        {
            Czesci = new ObservableCollection<Czesc>();
            ZlecenieNaprawy = new ZlecenieNaprawy(new Klient(), new Samochod { Marka = "asdfasdf", Model = "Swiasdfasdfft" }, new List<Usterka>());
        }
        public SzczegolyZleceniaViewModel(ZlecenieNaprawy zlecenieNaprawy) : this()
        {
            this.ZlecenieNaprawy = zlecenieNaprawy;
        }

        #endregion

        #region methods

        private void OtworzOknoSzczegolow(ObservableCollection<Czesc> czesci = null, Usterka wybranaUsterka = null)
        {
            var szczegolyViewModel = new SzczegolyUsterkiViewModel(czesci, wybranaUsterka);

            var szczegolyWindow = new SzczegolyUsterkiWindow(szczegolyViewModel);
            szczegolyWindow.ShowDialog();

            OnPropertyChanged(nameof(ZlecenieNaprawy), nameof(WybranaUsterka), nameof(Czesci));
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

        private ICommand _dodajNowaUsterke;
        public ICommand DodajNowaUsterke
        {
            get
            {
                if (_dodajNowaUsterke == null)
                    _dodajNowaUsterke = new RelayCommand(
                     (object argument) =>
                     {
                         OtworzOknoSzczegolow(Czesci);
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _dodajNowaUsterke;
            }
        }

        private ICommand _szczegolyUsterki;
        public ICommand SzczegolyUsterki
        {
            get
            {
                if (_szczegolyUsterki == null)
                    _szczegolyUsterki = new RelayCommand(
                     (object argument) =>
                     {
                         OtworzOknoSzczegolow(Czesci, WybranaUsterka);
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _szczegolyUsterki;
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
