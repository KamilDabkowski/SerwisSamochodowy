using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
using SerwisSamochodowy.Model.Helpers;
using SerwisSamochodowy.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class SzczegolyUsterkiViewModel : ViewModelBase
    {

        #region fields

        private int _idZlecenie;

        #endregion

        #region properties
        public ObservableCollection<Mechanik> Mechanicy { get; set; }
        private Mechanik _wybranyMechanik;

        public Mechanik WybranyMechanik
        {
            get { return _wybranyMechanik; }
            set
            {
                _wybranyMechanik = value;
                if(WybranaUsterka != null && WybranyMechanik != null && WybranaUsterka.IdMechanik != WybranyMechanik.IdMechanik)
                    WybranaUsterka.IdMechanik = WybranyMechanik.IdMechanik;
            }
        }

        public Usterka WybranaUsterka { get; set; }
        public Czesc WybranaCzesc { get; set; }
        public ObservableCollection<Czesc> Czesci { get; set; }

        #endregion

        #region ctor

        public SzczegolyUsterkiViewModel()
        {
            WybranaUsterka = new Usterka();
            WybranyMechanik = new Mechanik();
            Czesci = new ObservableCollection<Czesc>();
        }

        public SzczegolyUsterkiViewModel(int idZlecenie, ObservableCollection<Czesc> czesci = null, Usterka usterka = null)
        {
            _idZlecenie = idZlecenie;
            if (czesci == null)
                this.Czesci = new ObservableCollection<Czesc>();
            else
                this.Czesci = czesci;

            if (usterka == null)
                this.WybranaUsterka = new Usterka();
            else
                this.WybranaUsterka = usterka;
        }

        private void WczytajMechanikow()
        {
            Mechanicy = BazaDanych.Mechanicy;
            WybranyMechanik = Mechanicy.FirstOrDefault(m => m.IdMechanik == WybranaUsterka.IdMechanik);
            OnPropertyChanged(nameof(Mechanicy), nameof(WybranyMechanik));
        }

        private void ZapiszUsterke()
        {
            if (WybranaUsterka.IdUsterka < 1)
            {
                int id = 0;
                var ostatnie = BazaDanych.Usterki.LastOrDefault();
                if (ostatnie != null)
                    id = ostatnie.IdUsterka;

                WybranaUsterka.IdUsterka = ++id;
                BazaDanych.Usterki.Add(WybranaUsterka);
            }
            else
            {
                var zastepowaneZlecenie = BazaDanych.Usterki.FirstOrDefault(w => w.IdUsterka == WybranaUsterka.IdUsterka);
                var index = BazaDanych.Usterki.IndexOf(zastepowaneZlecenie);
                BazaDanych.Usterki[index] = WybranaUsterka;
            }
            ObslugaJSON<Usterka>.ZapiszDoJSON(BazaDanych.Usterki, Staticks.PlikUsterek);
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
                         WczytajMechanikow();
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
                         ZapiszUsterke();
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _zapisz;
            }
        }
        #endregion
    }
}
