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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CustomersWindow? customersWindow;
        BooksWindow? booksWindow;
        TransactionsWindow? transactionsWindow;

        public MainWindow()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();
        }

        private void ShowCustomersWindow(object sender, RoutedEventArgs e)
        {
            if (customersWindow == null)
            {
                customersWindow = new CustomersWindow();
                customersWindow.Closed += (s, args) => customersWindow = null;
                customersWindow.Show();
            }
            else
            {
                customersWindow.Activate();
            }
        }

        private void ShowBooksWindow(object sender, RoutedEventArgs e)
        {
            if (booksWindow == null)
            {
                booksWindow = new BooksWindow();
                booksWindow.Closed += (s, args) => booksWindow = null;
                booksWindow.Show();
            }
            else
            {
                booksWindow.Activate();
            }
        }

        private void ShowTransactionsWindow(object sender, RoutedEventArgs e)
        {
            if (transactionsWindow == null)
            {
                transactionsWindow = new TransactionsWindow();
                transactionsWindow.Closed += (s, args) => transactionsWindow = null;
                transactionsWindow.Show();
            }
            else
            {
                transactionsWindow.Activate();
            }
        }

        private void CloseApplication(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
