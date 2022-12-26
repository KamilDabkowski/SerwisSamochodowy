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

        private int _idKlienta;

        private string _numerTelefonuKlienta;

        public string NumerTelefonuKlienta
        {
            get { return _numerTelefonuKlienta; }
            set
            {
                _numerTelefonuKlienta = value;
                _idKlienta = new KlientConstructor(NumerTelefonuKlienta).ZnajdzIdKlienta();
            }
        }

        private string _numerRejestracyjny;

        public string NumerRejestracyjny
        {
            get { return _numerRejestracyjny; }
            set
            {
                _numerRejestracyjny = value;

                if (WybranySamochod == null || WybranySamochod.NumerRejestracyjny == null)
                {
                    WybranySamochod = new SamochodConstructor(NumerRejestracyjny).ZnajdzSamochod();
                    WybraneZlecenie = new ZlecenieNaprawy(_idKlienta, WybranySamochod.IdSamochod);
                }
                OnPropertyChanged(nameof(WybranySamochod), nameof(NumerRejestracyjny), nameof(WybraneZlecenie));
            }
        }

        public Samochod WybranySamochod { get; set; }

        private ZlecenieNaprawy _wybraneZlecenie;

        public ZlecenieNaprawy WybraneZlecenie
        {
            get { return _wybraneZlecenie; }
            set
            {
                _wybraneZlecenie = value;
                WczytajUsterki();
            }
        }

        public ObservableCollection<Usterka> Usterki { get; set; }
        public Usterka WybranaUsterka { get; set; }

        public ObservableCollection<Czesc> Czesci { get; set; }

        #endregion

        #region ctor

        public SzczegolyZleceniaViewModel()
        {
            Usterki = new ObservableCollection<Usterka>();
            Czesci = new ObservableCollection<Czesc>();
        }
        public SzczegolyZleceniaViewModel(ZlecenieNaprawy zlecenieNaprawy) : this()
        {
            if (zlecenieNaprawy != null)
            {
                this.WybraneZlecenie = zlecenieNaprawy;
                NumerTelefonuKlienta = BazaDanych.Klienci.FirstOrDefault(k => k.IdKlient == zlecenieNaprawy.IdKlient).NumerTelefonu;
                WybranySamochod = BazaDanych.Samochody.FirstOrDefault(s=>s.IdSamochod == WybraneZlecenie.IdSamochod);
                NumerRejestracyjny = WybranySamochod.NumerRejestracyjny;

            }
        }

        #endregion

        #region methods

        private void OtworzOknoSzczegolow(ObservableCollection<Czesc> czesci = null, Usterka wybranaUsterka = null)
        {
            var szczegolyViewModel = new SzczegolyUsterkiViewModel(WybraneZlecenie.IdZlecenie, wybranaUsterka);

            var szczegolyWindow = new SzczegolyUsterkiWindow(szczegolyViewModel);
            szczegolyWindow.ShowDialog();

            WczytajCzesci();

            OnPropertyChanged(nameof(WybraneZlecenie), nameof(WybranaUsterka));
        }

        private void ZapiszDane()
        {
            ZapiszSamochod();
            ZapiszZlecenie();
        }

        private void ZapiszSamochod()
        {
            if (WybranySamochod != null)
            {
                BazaDanych.Samochody.Add(WybranySamochod);
                ObslugaJSON<Samochod>.ZapiszDoJSON(BazaDanych.Samochody, Staticks.PlikSamochodow);
            }
        }

        private void ZapiszZlecenie()
        {
            if (WybraneZlecenie.IdZlecenie < 1)
            {
                int id = 0;
                var ostatnie = BazaDanych.ZleceniaNaprawy.LastOrDefault();
                if (ostatnie != null)
                    id = ostatnie.IdZlecenie;

                WybraneZlecenie.IdZlecenie = ++id;
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

        private void WczytajUsterki()
        {
            Usterki = new ObservableCollection<Usterka>(BazaDanych.Usterki.Where(u => u.IdZlecenieNaprawy == WybraneZlecenie.IdZlecenie));
            OnPropertyChanged(nameof(Usterki));
        }

        private void WczytajCzesci()
        {
            Czesci = new ObservableCollection<Czesc>();
            foreach (var usterka in Usterki)
            {
                var czesciUsterki = new ObservableCollection<Czesc>(new Czesc().WczytajCzesci(usterka.IdUsterka));
                foreach(var czesc in czesciUsterki)
                    Czesci.Add(czesc);
            }
            OnPropertyChanged(nameof(Czesci));
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
                         WczytajCzesci();
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
