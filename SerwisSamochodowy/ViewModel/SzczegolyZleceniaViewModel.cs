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
        public Faktura WystawionaFaktura { get; set; }

        #endregion

        #region ctor

        public SzczegolyZleceniaViewModel()
        {
            WybranaUsterka= new Usterka();
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

        private void OtworzOknoSzczegolow(Usterka wybranaUsterka = null)
        {
            var szczegolyViewModel = new SzczegolyUsterkiViewModel(WybraneZlecenie.IdZlecenie, wybranaUsterka);

            var szczegolyWindow = new SzczegolyUsterkiWindow(szczegolyViewModel);
            szczegolyWindow.ShowDialog();

            Usterki = Usterka.WczytajUsterki(WybraneZlecenie.IdZlecenie);
            Czesci = Czesc.WczytajCzesciZlecenia(WybraneZlecenie.IdZlecenie);

            OnPropertyChanged(nameof(WybraneZlecenie), nameof(WybranaUsterka), nameof(Usterki), nameof(Czesci));
        }

        private void ZapiszDane()
        {
            if (WybranySamochod != null)
                WybranySamochod.ZapiszSamochod();

            if (WybraneZlecenie != null)
                WybraneZlecenie.ZapiszZlecenie();
        }

        private void WczytajUsterki()
        {
            Usterki = Usterka.WczytajUsterki(WybraneZlecenie.IdZlecenie);
            OnPropertyChanged(nameof(Usterki));
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
                         if (WybraneZlecenie != null)
                         {
                             Czesci = Czesc.WczytajCzesciZlecenia(WybraneZlecenie.IdZlecenie);
                             WystawionaFaktura = Faktura.WczytajFakture(WybraneZlecenie.IdZlecenie);
                             OnPropertyChanged(nameof(WybraneZlecenie), nameof(Czesci), nameof(WystawionaFaktura));
                         }
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
                         OtworzOknoSzczegolow();
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
                         OtworzOknoSzczegolow(WybranaUsterka);
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _szczegolyUsterki;
            }
        }

        private ICommand _wystawFakture;
        public ICommand WystawFakture
        {
            get
            {
                if (_wystawFakture == null)
                    _wystawFakture = new RelayCommand(
                     (object argument) =>
                     {
                         WybraneZlecenie.DataOdbioru = DateTime.Now.Date;
                         WybraneZlecenie.ZapiszZlecenie();
                         WystawionaFaktura = FakturaConstructor.WystawFakture(WybraneZlecenie.IdZlecenie);
                         OnPropertyChanged(nameof(WystawionaFaktura), nameof(WybraneZlecenie));
                     },
                     (object argument) =>
                     {
                         if (WystawionaFaktura == null)
                             return true;
                         else
                             return false;
                     }
                    );
                return _wystawFakture;
            }
        }

        private ICommand _otworzFakture;
        public ICommand OtworzFakture
        {
            get
            {
                if (_otworzFakture == null)
                    _otworzFakture = new RelayCommand(
                     (object argument) =>
                     {
                         WystawionaFaktura.OtworzFakture();

                     },
                     (object argument) =>
                     {
                         if (WystawionaFaktura != null)
                             return true;
                         else
                             return true;
                     }
                    );
                return _otworzFakture;
            }
        }

        #endregion

    }
}
