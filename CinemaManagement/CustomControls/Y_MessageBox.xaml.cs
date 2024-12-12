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

namespace CinemaManagement.CustomControls
{
    /// <summary>
    /// Interaction logic for Y_MessageBox.xaml
    /// </summary>
    public partial class Y_MessageBox : Window
    {
        public bool Result {  get; private set; }
        public Y_MessageBox(string Text)
        {
            InitializeComponent();
            User_noti.Text = Text;
        }
        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
