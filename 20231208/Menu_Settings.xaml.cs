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
    /// Interaction logic for Menu_Settings.xaml
    /// </summary>
    public partial class Menu_Settings : Page {
        public Menu_Settings() {
            InitializeComponent();

            // Sets the textbox to the current API key from the Control class
            inputKey.Text = Control.ApiKey;
        }

        // Event handler for when the text in the inputKey TextBox changes
        private void InputKey_TextChanged(object sender, System.EventArgs e) {
            // Updates the API key in the Control class
            Control.ApiKey = inputKey.Text;
        }
    }
}
