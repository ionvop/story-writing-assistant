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
            inputKey.Text = Session.ApiKey;
        }

        private void InputKey_TextChanged(object sender, System.EventArgs e) {
            Session.ApiKey = inputKey.Text;
        }
    }
}
