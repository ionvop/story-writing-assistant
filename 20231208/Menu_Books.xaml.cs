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
            listBooks.ItemsSource = Control.Books;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e) {
            Control.Books.Add(new("New book"));
            Update();
        }

        private void ListBooks_Select(object sender, SelectionChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) {
                divPanel.Visibility = Visibility.Visible;
                inputTitle.Text = book.Title;
                inputAuthor.Text = book.Author;
                inputDescription.Text = book.Description;
                return;
            }

            divPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e) {
            if (listBooks.SelectedItem is Book book) Control.CurrentBook = book; else return;
            Control.Navigate(Window.GetWindow(this), "ChapterSelect.xaml");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (listBooks.SelectedItem is Book book) Control.Books.Remove(book);
            Update();
        }

        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) book.Title = inputTitle.Text;
        }

        private void InputAuthor_TextChanged(object sender, TextChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) book.Author = inputAuthor.Text;
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (listBooks.SelectedItem is Book book) book.Description = inputDescription.Text;
        }

        public void Update() {
            listBooks.ItemsSource = null;
            listBooks.ItemsSource = Control.Books;
        }
    }
}
