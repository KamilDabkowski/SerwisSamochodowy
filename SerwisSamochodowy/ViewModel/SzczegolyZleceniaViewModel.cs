using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
using SerwisSamochodowy.Model.Helpers;
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
    internal class SzczegolyZleceniaViewModel : ViewModelBase
    {

        #region properties

        public ZlecenieNaprawy WybraneZlecenie { get; set; }
        public Usterka WybranaUsterka { get; set; }

        ObservableCollection<Czesc> Czesci { get; set; }

        #endregion

        #region ctor

        public SzczegolyZleceniaViewModel()
        {
            Czesci = new ObservableCollection<Czesc>();
            WybraneZlecenie = new ZlecenieNaprawy(new Klient(), new Samochod(), new List<Usterka>());
        }
        public SzczegolyZleceniaViewModel(ZlecenieNaprawy zlecenieNaprawy) : this()
        {
            if(zlecenieNaprawy != null)
                this.WybraneZlecenie = zlecenieNaprawy;
        }

        #endregion

        #region methods

        private void OtworzOknoSzczegolow(ObservableCollection<Czesc> czesci = null, Usterka wybranaUsterka = null)
        {
            var szczegolyViewModel = new SzczegolyUsterkiViewModel(czesci, wybranaUsterka);

            var szczegolyWindow = new SzczegolyUsterkiWindow(szczegolyViewModel);
            szczegolyWindow.ShowDialog();

            OnPropertyChanged(nameof(WybraneZlecenie), nameof(WybranaUsterka), nameof(Czesci));
        }

        private void ZapiszDane()
        {
            if (WybraneZlecenie.IdZlecenie < 1)
            {
                WybraneZlecenie.IdZlecenie = BazaDanych.ZleceniaNaprawy.LastOrDefault<ZlecenieNaprawy>().IdZlecenie + 1;
                BazaDanych.ZleceniaNaprawy.Add(WybraneZlecenie);
            }
            else
            {
                var zastepowaneZlecenie = BazaDanych.ZleceniaNaprawy.FirstOrDefault(w => w.IdZlecenie == WybraneZlecenie.IdZlecenie);
                var index = BazaDanych.ZleceniaNaprawy.IndexOf(zastepowaneZlecenie);
                BazaDanych.ZleceniaNaprawy[index] = WybraneZlecenie;
            }
            ObslugaJSON<ZlecenieNaprawy>.ZapiszDoJSON(BazaDanych.ZleceniaNaprawy, Staticks.PlikZlecenNaprawy);
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
                         OnPropertyChanged(nameof(WybraneZlecenie));
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _loaded;
            }
        }

        private ICommand _zapisz;
        public ICommand Zapisz
        {
            get
            {
                if (_zapisz == null)
                    _zapisz = new RelayCommand(
                     (object argument) =>
                     {
                         ZapiszDane();
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _zapisz;
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

    }
}
