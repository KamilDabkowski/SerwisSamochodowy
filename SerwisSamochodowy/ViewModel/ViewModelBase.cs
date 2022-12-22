using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {

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
