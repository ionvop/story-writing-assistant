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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20231208 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // Load previously saved data
            Control.Load();

            // Navigate to the Menu.xaml page
            mainFrame.Navigate(new("Menu.xaml", UriKind.Relative));
        }

        // Method to navigate to a specified page
        public void Navigate(Uri uri) {
            // Use the mainFrame's Navigate method to navigate to the specified page
            mainFrame.Navigate(uri);
        }

        // Override the OnClosing method to perform actions before the application closes
        // Polymorphism achieved
        protected override void OnClosing(CancelEventArgs e) {
            // Call the base class implementation of OnClosing
            base.OnClosing(e);

            // Save the current state (books and API key) when the application is closing
            Control.Save();
        }
    }
}
