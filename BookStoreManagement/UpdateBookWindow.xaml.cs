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
    /// Interaction logic for UpdateBookWindow.xaml
    /// </summary>
    public partial class UpdateBookWindow : Window
    {
        Book book;
        
        public UpdateBookWindow(Book book)
        {
            InitializeComponent();
            this.book = book;
            DataContext = book;
        }

        private void UpdateBook(object sender, RoutedEventArgs e)
        {
            DataAccess.UpdateBook(book);
            Close();
        }
    }
}
