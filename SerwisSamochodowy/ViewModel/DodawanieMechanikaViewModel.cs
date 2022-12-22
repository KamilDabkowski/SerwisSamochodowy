using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerwisSamochodowy.ViewModel
{
    internal class DodawanieMechanikaViewModel : ViewModelBase
    {
        #region properties

        public Mechanik WybranyMechanik { get; set; }

        #endregion

        #region ctor
        public DodawanieMechanikaViewModel()
        {
            WybranyMechanik = new Mechanik();
        }

        #endregion

        #region methods

        private void ZapiszDane()
        {
            throw new NotImplementedException();
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
