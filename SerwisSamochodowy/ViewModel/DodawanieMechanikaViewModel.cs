using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class DodawanieMechanikaViewModel : ViewModelBase
    {
        #region properties

        private Mechanik _wybranyMechanik;

        public Mechanik WybranyMechanik
        {
            get { return _wybranyMechanik; }
            set
            {
                _wybranyMechanik = value;
                OnPropertyChanged(nameof(WybranyMechanik));
            }
        }


        public ObservableCollection<Mechanik> Mechanicy { get; set; }

        #endregion

        #region ctor
        public DodawanieMechanikaViewModel()
        {
            WybranyMechanik = new Mechanik();

            Mechanicy = BazaDanych.Mechanicy;
        }

        #endregion

        #region methods

        private void ZapiszDane()
        {
            if (WybranyMechanik.IdMechanik == 0)
            {
                var ostatni = Mechanicy.LastOrDefault();
                int id = 0;
                if (ostatni != null)
                    id = ostatni.IdMechanik;
                WybranyMechanik.IdMechanik = ++id;
                Mechanicy.Add(WybranyMechanik);
            }

            ObslugaJSON<Mechanik>.ZapiszDoJSON(Mechanicy, Staticks.PlikMechanikow);
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
                         if (!string.IsNullOrEmpty(WybranyMechanik.Imie) && !string.IsNullOrEmpty(WybranyMechanik.Nazwisko))
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
