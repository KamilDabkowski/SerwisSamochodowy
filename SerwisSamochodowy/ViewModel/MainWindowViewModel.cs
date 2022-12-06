using SerwisSamochodowy.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {



        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                if (_loadedCommand == null)
                    _loadedCommand = new RelayCommand(
                     (object argument) =>
                     {
                         Debug.WriteLine("not implemented");
                     },
                     (object argument) =>
                     {
                         return true;
                     }
                    );
                return _loadedCommand;
            }
        }

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
