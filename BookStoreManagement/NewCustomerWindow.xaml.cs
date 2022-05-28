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
    /// Interaction logic for NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        Customer customer;

        public NewCustomerWindow()
        {
            InitializeComponent();
            customer = new Customer();
            DataContext = customer;
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            if (customer.Name == "")
            {
                MessageBox.Show("Please enter name");
                return;
            }
            DataAccess.AddCustomer(customer);
            Close();
        }
    }
}
