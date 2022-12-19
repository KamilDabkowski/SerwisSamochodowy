using SerwisSamochodowy.Model;
using SerwisSamochodowy.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.ViewModel
{
    internal class SzczegolyUsterkiViewModel : INotifyPropertyChanged
    {

        #region properties
        public Usterka WybranaUsterka { get; set; }
        public Czesc WybranaCzesc { get; set; }
        public ObservableCollection<Czesc> Czesci { get; set; }

        #endregion

        #region ctor

        public SzczegolyUsterkiViewModel()
        {
            WybranaUsterka = new Usterka();
            Czesci = new ObservableCollection<Czesc>();
        }
        public SzczegolyUsterkiViewModel(ObservableCollection<Czesc> czesci = null, Usterka usterka = null)
        {
            if (czesci == null)
                this.Czesci = new ObservableCollection<Czesc>();
            else
                this.Czesci = czesci;

            if (usterka == null)
                this.WybranaUsterka = new Usterka();
            else
                this.WybranaUsterka = usterka;
        }

        #endregion

        #region events

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(params string[] nazwyWlasnosci)
        {
            if (PropertyChanged != null)
            {
                foreach (string nazwaWlasnosci in nazwyWlasnosci)
                    PropertyChanged(this, new PropertyChangedEventArgs(nazwaWlasnosci));
            }
        }

        #endregion

    }
}
