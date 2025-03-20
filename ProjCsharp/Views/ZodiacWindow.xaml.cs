using AppZodiac.ViewModels;
using System;
using System.Collections.Generic;
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

namespace AppZodiac.Views
{
    /// <summary>
    /// Interaction logic for ZodiacWindow.xaml
    /// </summary>
    public partial class ZodiacWindow : Window
    {

        public ZodiacWindow(DateTime birthDate)
        {
            InitializeComponent();
            DataContext = new ZodiacViewModel(birthDate);
        }
    }
}
