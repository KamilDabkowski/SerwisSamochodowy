using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
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
        public Usterka WybranaUsterka { get; set; }
        public Czesc WybranaCzesc { get; set; }
        public ObservableCollection<Czesc> Czesci { get; set; }

        #endregion

        #region ctor

        public SzczegolyUsterkiViewModel()
        {
            WybranaUsterka = new Usterka();
            WybranaUsterka.Mechanik = new Mechanik();
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
            OnPropertyChanged(nameof(Mechanicy));
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

        #endregion
    }
}
