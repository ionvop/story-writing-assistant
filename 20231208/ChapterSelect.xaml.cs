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

            if (Session.CurrentBook == null) {
                MessageBox.Show("Error loading book.");

                if (Window.GetWindow(this) is MainWindow mainWindow) {
                    mainWindow.Navigate(new Uri("Menu.xaml", UriKind.Relative));
                }

                return;
            }

            textTitle.Text = "Chapters for " + Session.CurrentBook.Title;
            listChapters.ItemsSource = Session.CurrentBook.Chapters;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e) {
            Session.CurrentBook?.Chapters.Add(new Chapter("New chapter"));
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
            Session.Save();

            if (Window.GetWindow(this) is MainWindow mainWindow) {
                mainWindow.Navigate(new Uri("Menu.xaml", UriKind.Relative));
            }
        }

        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) chapter.Title = inputTitle.Text;
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) chapter.Description = inputDescription.Text;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            if (Window.GetWindow(this) is MainWindow mainWindow) {
                if (listChapters.SelectedItem is Chapter chapter) Session.CurrentChapter = chapter; else return;
                mainWindow.Navigate(new Uri("Editor.xaml", UriKind.Relative));
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) Session.CurrentBook?.Chapters.Remove(chapter);
            Update();
        }

        public void Update() {
            listChapters.ItemsSource = null;
            listChapters.ItemsSource = Session.CurrentBook?.Chapters;
        }
    }
}
