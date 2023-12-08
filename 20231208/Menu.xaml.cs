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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20231208 {
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page {
        public Menu() {
            InitializeComponent();
            mainFrame.Navigate(new Uri("Menu_Books.xaml", UriKind.Relative));
        }

        private void BtnBooks_Click(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(new Uri("Menu_Books.xaml", UriKind.Relative));
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(new Uri("Menu_Settings.xaml", UriKind.Relative));
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
