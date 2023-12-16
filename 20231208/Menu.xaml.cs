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

            // Navigate to the Menu_Books.xaml page when the Menu page is loaded
            mainFrame.Navigate(new("Menu_Books.xaml", UriKind.Relative));
        }

        // The following are Click event handlers for the sidebar buttons
        // They navigate to the specified page when the sidebar button is clicked
        private void BtnBooks_Click(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(new("Menu_Books.xaml", UriKind.Relative));
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e) {
            mainFrame.Navigate(new("Menu_Settings.xaml", UriKind.Relative));
        }

        // Event handler for the Exit sidebar button click
        private void BtnExit_Click(object sender, RoutedEventArgs e) {
            // Shut down the application when the 'Exit' button is clicked
            Application.Current.Shutdown();
        }
    }
}
