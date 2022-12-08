using SerwisSamochodowy.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SerwisSamochodowy.View
{
    /// <summary>
    /// Logika interakcji dla klasy SzczegolyZleceniaWindow.xaml
    /// </summary>
    public partial class SzczegolyZleceniaWindow : Window
    {
        public SzczegolyZleceniaWindow()
        {
            InitializeComponent();
        }

        public SzczegolyZleceniaWindow(INotifyPropertyChanged viewModel) : this()
        {
            this.DataContext = viewModel;
        }
    }
}
