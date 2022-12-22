using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model.Helpers;
using SerwisSamochodowy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class DodawanieKierownikaViewModel : ViewModelBase
    {
        #region properties

        private Kierownik _wybranyKierownik;

        public Kierownik WybranyKierownik
        {
            get { return _wybranyKierownik; }
            set
            {
                _wybranyKierownik = value;
                OnPropertyChanged(nameof(WybranyKierownik));
            }
        }


        public ObservableCollection<Kierownik> Kierownicy { get; set; }

        #endregion

        #region ctor
        public DodawanieKierownikaViewModel()
        {
            WybranyKierownik = new Kierownik();

            Kierownicy = BazaDanych.Kierownicy;
        }

        #endregion

        #region methods

        private void ZapiszDane()
        {
            WybranyKierownik.ZapiszDane();
            WybranyKierownik = new Kierownik();
            OnPropertyChanged(nameof(WybranyKierownik));
        }

        #endregion

        #region commands

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
                         if (!string.IsNullOrEmpty(WybranyKierownik.Imie) && !string.IsNullOrEmpty(WybranyKierownik.Nazwisko)
                         && !string.IsNullOrEmpty(WybranyKierownik.Login) && !string.IsNullOrEmpty(WybranyKierownik.Haslo))
                             return true;
                         else
                             return false;
                     }
                    );
                return _zapisz;
            }
        }

        #endregion

    }
}
