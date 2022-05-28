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
    /// Interaction logic for NewBookWindow.xaml
    /// </summary>
    public partial class NewBookWindow : Window
    {
        Book book;

        public NewBookWindow()
        {
            InitializeComponent();
            book = new Book();
            DataContext = book;
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            if (book.Title == "")
            {
                MessageBox.Show("Title is required");
                return;
            }
            if (book.Price <= 0)
            {
                MessageBox.Show("Price must be greater than 0");
                return;
            }
            if (book.Quantity < 0)
            {
                MessageBox.Show("Quantity must be greater than 0");
                return;
            }
            try
            {
                DataAccess.AddBook(book);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    MessageBox.Show("Book already exists");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                return;
            }
            Close();
        }
    }
}
