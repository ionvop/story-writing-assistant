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
    /// Interaction logic for Menu_Books.xaml
    /// </summary>
    public partial class Menu_Books : Page {
        public Menu_Books() {
            InitializeComponent();

            // Set the items source to the list of books
            listBooks.ItemsSource = Control.Books;
        }

        // Event handler for the "New" button click
        private void BtnNew_Click(object sender, RoutedEventArgs e) {
            // Add a new book and update the UI
            Control.Books.Add(new("New book"));
            Update();
        }

        // Event handler for when a book is selected
        private void ListBooks_Select(object sender, SelectionChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) {
                // If a book is selected, show its details in the UI
                divPanel.Visibility = Visibility.Visible;
                inputTitle.Text = book.Title;
                inputAuthor.Text = book.Author;
                inputDescription.Text = book.Description;
                return;
            }

            // If no book is selected, hide the details panel
            divPanel.Visibility = Visibility.Collapsed;
        }

        // Event handler for the "Open" button click
        private void BtnOpen_Click(object sender, RoutedEventArgs e) {
            // Set the current book and navigate to the Chapter Select page
            if (listBooks.SelectedItem is Book book) Control.CurrentBook = book; else return;
            Control.Navigate(Window.GetWindow(this), "ChapterSelect.xaml");
        }

        // Event handler for the "Delete" button click
        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            // Remove the selected book and update the UI
            if (listBooks.SelectedItem is Book book) Control.Books.Remove(book);
            Update();
        }

        // For the following TextChanged events, they basically update the properties of the selected book with the user's new input
        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) book.Title = inputTitle.Text;
        }

        private void InputAuthor_TextChanged(object sender, TextChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) book.Author = inputAuthor.Text;
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) book.Description = inputDescription.Text;
        }

        // Update the UI with the current list of books
        public void Update() {
            listBooks.ItemsSource = null;
            listBooks.ItemsSource = Control.Books;
        }
    }
}
