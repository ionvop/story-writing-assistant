﻿using System;
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
            Session.Load();
            mainFrame.Navigate(new Uri("Menu.xaml", UriKind.Relative));
        }

        public void Navigate(Uri uri) {
            mainFrame.Navigate(uri);
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            Session.Save();
        }
    }
}