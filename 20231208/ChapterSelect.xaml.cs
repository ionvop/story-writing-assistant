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
    /// Interaction logic for ChapterSelect.xaml
    /// </summary>
    public partial class ChapterSelect : Page {
        public ChapterSelect() {
            InitializeComponent();

            if (Control.CurrentBook == null) {
                MessageBox.Show("Error loading book.");
                Control.Navigate(Window.GetWindow(this), "Menu.xaml");
                return;
            }

            textTitle.Text = "Chapters for " + Control.CurrentBook.Title;
            listChapters.ItemsSource = Control.CurrentBook.Chapters;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e) {
            Control.CurrentBook?.Chapters.Add(new("New chapter"));
            Update();
        }

        private void ListChapters_Select(object sender, SelectionChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) {
                divPanel.Visibility = Visibility.Visible;
                inputTitle.Text = chapter.Title;
                inputDescription.Text = chapter.Description;
                return;
            }

            divPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            Control.Navigate(Window.GetWindow(this), "Menu.xaml");
        }

        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) chapter.Title = inputTitle.Text;
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) chapter.Description = inputDescription.Text;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) Control.CurrentChapter = chapter; else return;
            Control.Navigate(Window.GetWindow(this), "Editor.xaml");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) Control.CurrentBook?.Chapters.Remove(chapter);
            Update();
        }

        public void Update() {
            listChapters.ItemsSource = null;
            listChapters.ItemsSource = Control.CurrentBook?.Chapters;
        }
    }
}
