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

        private Czesc _wybranaCzesc;

        public Czesc WybranaCzesc
        {
            get { return _wybranaCzesc; }
            set
            {
                _wybranaCzesc = value;
                OnPropertyChanged(nameof(WybranaCzesc));
            }
        }

        public ObservableCollection<Czesc> Czesci { get; set; }

        #endregion

        #region ctor

        public SzczegolyUsterkiViewModel()
        {
            WybranaCzesc= new Czesc();
            WybranaUsterka = new Usterka();
            WybranyMechanik = new Mechanik();
            Czesci = new ObservableCollection<Czesc>();
        }

        public SzczegolyUsterkiViewModel(int idZlecenie, Usterka usterka = null) :this()
        {
            _idZlecenie = idZlecenie;

            if (usterka == null)
                this.WybranaUsterka = new Usterka();
            else
                this.WybranaUsterka = usterka;
        }

        private void WczytajMechanikow()
        {
            Mechanicy = WybranyMechanik.WczytajMechanikow();
            WybranyMechanik = WybranyMechanik.WyszukajMechanika(WybranaUsterka.IdMechanik);
            OnPropertyChanged(nameof(Mechanicy), nameof(WybranyMechanik));
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
                         Czesci = WybranaCzesc.WczytajCzesci(WybranaUsterka.IdUsterka);
                         OnPropertyChanged(nameof(Czesci));
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
                         WybranaUsterka.IdZlecenieNaprawy = _idZlecenie;
                         WybranaUsterka.ZapiszUsterke();
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _zapisz;
            }
        }

        private ICommand _zapiszCzesci;
        public ICommand ZapiszCzesci
        {
            get
            {
                if (_zapiszCzesci == null)
                    _zapiszCzesci = new RelayCommand(
                     (object argument) =>
                     {
                         WybranaCzesc.IdUsterka = WybranaUsterka.IdUsterka;
                         WybranaCzesc.ZapiszCzesc();
                         Czesci = WybranaCzesc.WczytajCzesci(WybranaUsterka.IdUsterka);
                         OnPropertyChanged(nameof(Czesci), nameof(WybranaCzesc));
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _zapiszCzesci;
            }
        }

        private ICommand _dodajCzesci;
        public ICommand DodajCzesci
        {
            get
            {
                if (_dodajCzesci == null)
                    _dodajCzesci = new RelayCommand(
                     (object argument) =>
                     {
                         WybranaCzesc.IdUsterka = WybranaUsterka.IdUsterka;
                         Czesci.Add(WybranaCzesc);
                         WybranaCzesc.ZapiszCzesc();
                         Czesci = WybranaCzesc.WczytajCzesci(WybranaUsterka.IdUsterka);
                         OnPropertyChanged(nameof(Czesci), nameof(WybranaCzesc));
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _dodajCzesci;
            }
        }

        #endregion
    }
}
