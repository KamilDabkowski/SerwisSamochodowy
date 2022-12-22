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
using System.Windows;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region properties
        public ObservableCollection<ZlecenieNaprawy> ZleceniaNaprawy { get; set; }

        public ZlecenieNaprawy WybraneZlecenie { get; set; }

        #endregion

        #region ctor

        public MainWindowViewModel()
        {

        }

        #endregion

        #region methods
        private void OtworzOknoSzczegolow(ZlecenieNaprawy wybraneZlecenie = null)
        {
            var szczegolyViewModel = new SzczegolyZleceniaViewModel(wybraneZlecenie);

            var szczegolyWindow = new SzczegolyZleceniaWindow(szczegolyViewModel);
            szczegolyWindow.ShowDialog();
            WybraneZlecenie = ZleceniaNaprawy.Last();

            OnPropertyChanged(nameof(ZleceniaNaprawy), nameof(WybraneZlecenie));
        }

        private void WczytajDaneZPlikow()
        {
            BazaDanych.ZleceniaNaprawy = ObslugaJSON<ZlecenieNaprawy>.PobierzDaneZJSON(Staticks.PlikZlecenNaprawy);
            BazaDanych.Mechanicy = ObslugaJSON<Mechanik>.PobierzDaneZJSON(Staticks.PlikMechanikow);
            BazaDanych.Kierownicy = ObslugaJSON<Kierownik>.PobierzDaneZJSON(Staticks.PlikKierownikow);
            ZleceniaNaprawy = BazaDanych.ZleceniaNaprawy;
            //ObslugaJSON<ZlecenieNaprawy>.ZapiszDoJSON(BazaDanych.ZleceniaNaprawy, Staticks.PlikZlecenNaprawy);
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
                         WczytajDaneZPlikow();

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
                         OtworzOknoSzczegolow();
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
                         OtworzOknoSzczegolow(WybraneZlecenie);
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _szczegolyZlecenia;
            }
        }

        private ICommand _dodajKierownika;
        public ICommand DodajKierownika
        {
            get
            {
                if (_dodajKierownika == null)
                    _dodajKierownika = new RelayCommand(
                     (object argument) =>
                     {
                         var kierownikWindow = new DodawanieKierownikaWindow();
                         kierownikWindow.ShowDialog();
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _dodajKierownika;
            }
        }

        private ICommand _dodajMechanika;
        public ICommand DodajMechanika
        {
            get
            {
                if (_dodajMechanika == null)
                    _dodajMechanika = new RelayCommand(
                     (object argument) =>
                     {
                         var mechanikWindow = new DodawanieMechanikaWindow();
                         mechanikWindow.ShowDialog();
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _dodajMechanika;
            }
        }

        #endregion

    }
}
