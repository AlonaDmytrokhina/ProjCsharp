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
    /// Interaction logic for DateWindow.xaml
    /// </summary>
    public partial class DateWindow : Window
    {
        private DateViewModel _viewModel;

        public DateWindow()
        {
            InitializeComponent();
            _viewModel = new DateViewModel();

            _viewModel.OpenZodiacWindowAction = OpenZodiacWindow;
            DataContext = _viewModel;
        }

        private void OpenZodiacWindow(DateTime birthDate)
        {
            ZodiacWindow zodiacWindow = new ZodiacWindow(birthDate);
            zodiacWindow.Show();

            this.Close();
        }

    }
}
