﻿using SerwisSamochodowy.Common;
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
        public ObservableCollection<ZlecenieNaprawyWrapper> Zlecenia { get; set; }

        public ZlecenieNaprawyWrapper WybraneZlecenie { get; set; }

        #endregion

        #region ctor

        public MainWindowViewModel()
        {
            BazaDanych.WczytajDaneZPlikow();
            Zlecenia = ZlecenieNaprawyWrapper.WczytajZlecenia();
        }

        #endregion

        #region methods
        private void OtworzOknoSzczegolow(ZlecenieNaprawy wybraneZlecenie = null)
        {
            var szczegolyViewModel = new SzczegolyZleceniaViewModel(wybraneZlecenie);

            var szczegolyWindow = new SzczegolyZleceniaWindow(szczegolyViewModel);
            szczegolyWindow.ShowDialog();

            Zlecenia = ZlecenieNaprawyWrapper.WczytajZlecenia();

            OnPropertyChanged(nameof(Zlecenia), nameof(WybraneZlecenie));
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
                         BazaDanych.WczytajDaneZPlikow();

                         OnPropertyChanged(nameof(Zlecenia));
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

                         if (WybraneZlecenie != null && WybraneZlecenie.ZlecenieNaprawy.IdZlecenie > 0)
                             OtworzOknoSzczegolow(WybraneZlecenie.ZlecenieNaprawy);
                         else
                             MessageBox.Show("Nie wybrano zlecenia");

                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _szczegolyZlecenia;
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
