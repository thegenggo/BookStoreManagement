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
    /// Interaction logic for UpdateCustomerWindow.xaml
    /// </summary>
    public partial class UpdateCustomerWindow : Window
    {
        Customer customer;

        public UpdateCustomerWindow(Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            DataContext = customer;
        }

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            DataAccess.UpdateCustomer(customer);
            Close();
        }
    }
}
