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

            // Check if there is a current book; if not, show an error message and navigate to the Menu page
            if (Control.CurrentBook == null) {
                MessageBox.Show("Error loading book.");
                Control.Navigate(Window.GetWindow(this), "Menu.xaml");
                return;
            }

            // Set the title label to the current book's title
            textTitle.Text = "Chapters for " + Control.CurrentBook.Title;

            // Set the items source to the chapters of the current book
            listChapters.ItemsSource = Control.CurrentBook.Chapters;
        }

        // Event handler for the "New" button click
        private void BtnNew_Click(object sender, RoutedEventArgs e) {
            // Add a new chapter to the current book and update the UI
            Control.CurrentBook?.Chapters.Add(new("New chapter"));
            Update();
        }

        // Event handler for when a chapter is selected
        private void ListChapters_Select(object sender, SelectionChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) {
                // If a chapter is selected, show its details in the UI
                divPanel.Visibility = Visibility.Visible;
                inputTitle.Text = chapter.Title;
                inputDescription.Text = chapter.Description;
                return;
            }

            // If no chapter is selected, hide the details panel
            divPanel.Visibility = Visibility.Collapsed;
        }

        // Event handler for the "Back" button click
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            // Navigate back to the Menu page
            Control.Navigate(Window.GetWindow(this), "Menu.xaml");
        }

        // For the following TextChanged events, they basically update the properties of the selected chapter with the user's new input
        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) chapter.Title = inputTitle.Text;
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (listChapters.SelectedItem is Chapter chapter) chapter.Description = inputDescription.Text;
        }

        // Event handler for the "Edit" button click
        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            // Set the current chapter and navigate to the Editor page
            if (listChapters.SelectedItem is Chapter chapter) Control.CurrentChapter = chapter; else return;
            Control.Navigate(Window.GetWindow(this), "Editor.xaml");
        }

        // Event handler for the "Delete" button click
        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            // Remove the selected chapter from the current book and update the UI
            if (listChapters.SelectedItem is Chapter chapter) Control.CurrentBook?.Chapters.Remove(chapter);
            Update();
        }

        // Update the UI with the current list of chapters
        public void Update() {
            listChapters.ItemsSource = null;
            listChapters.ItemsSource = Control.CurrentBook?.Chapters;
        }
    }
}
