using CinemaManagement.View;
using CinemaManagement.View.AdminView.AccountView;
using CinemaManagement.ViewModel.AdminVM;
using CinemaManagement.ViewModel.NavigationVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

namespace CinemaManagement
{
    /// <summary>
    /// Interaction logic for MainNavigation.xaml
    /// </summary>
    public partial class MainNavigation : Window
    {
        public MainNavigation()
        {
            InitializeComponent();
            mainFrame.Navigate(new AccountView());
        }
        private void Closebutton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
