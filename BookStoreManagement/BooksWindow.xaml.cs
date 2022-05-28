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
using System.Windows.Shapes;

namespace BookStoreManagement
{
    /// <summary>
    /// Interaction logic for BooksWindow.xaml
    /// </summary>
    public partial class BooksWindow : Window
    {
        List<Book> books;

        public BooksWindow()
        {
            InitializeComponent();
            books = DataAccess.GetBooks(isbnTxt.Text, titleTxt.Text, descriptionTxt.Text, priceTxt.Text, quantityTxt.Text);
            booksGrid.ItemsSource = books;
        }

        private void NewBook(object sender, RoutedEventArgs e)
        {
            NewBookWindow newBookWindow = new NewBookWindow();
            newBookWindow.ShowDialog();
            LoadBook(sender, e);
        }

        private void LoadBook(object sender, RoutedEventArgs e)
        {
            books = DataAccess.GetBooks(isbnTxt.Text, titleTxt.Text, descriptionTxt.Text, priceTxt.Text, quantityTxt.Text);
            booksGrid.ItemsSource = books;
        }

        private void UpdateBook(object sender, RoutedEventArgs e)
        {
            Book? book = booksGrid.SelectedItem as Book;
            if (book != null)
            {
                UpdateBookWindow updateBookWindow = new UpdateBookWindow(book);
                updateBookWindow.ShowDialog();
                LoadBook(sender, e);
            }
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            Book? book = booksGrid.SelectedItem as Book;
            if (book == null) return;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this book?", "Delete Book", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DataAccess.DeleteBook(book.Isbn);
                LoadBook(sender, e);
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            isbnTxt.Text = "";
            titleTxt.Text = "";
            descriptionTxt.Text = "";
            priceTxt.Text = "";
            quantityTxt.Text = "";
        }
    }
}
