using SerwisSamochodowy.Model;
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
        public Usterka WybranaUsterka { get; set; }
        public Czesc WybranaCzesc { get; set; }
        public ObservableCollection<Usterka> Usterki { get; set; }
        public ObservableCollection<Czesc> Czesci { get; set; }



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
