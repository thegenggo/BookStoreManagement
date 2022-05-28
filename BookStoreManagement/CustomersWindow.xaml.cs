using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        List<Customer> customers;

        public CustomersWindow()
        {
            InitializeComponent();
            customers = DataAccess.GetCustomers(idTxt.Text, nameTxt.Text, addressTxt.Text, phoneNumberTxt.Text, emailTxt.Text);
            customersGrid.ItemsSource = customers;
        }

        private void NewCustomer(object sender, RoutedEventArgs e)
        {
            NewCustomerWindow newCustomerWindow = new NewCustomerWindow();
            newCustomerWindow.ShowDialog();
            LoadCustomer(sender, e);
        }

        private void LoadCustomer(object sender, RoutedEventArgs e)
        {
            customers = DataAccess.GetCustomers(idTxt.Text, nameTxt.Text, addressTxt.Text, phoneNumberTxt.Text, emailTxt.Text);
            customersGrid.ItemsSource = customers;
        }

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)customersGrid.SelectedItem;
            if (customer != null)
            {
                UpdateCustomerWindow updateCustomerWindow = new UpdateCustomerWindow(customer);
                updateCustomerWindow.ShowDialog();
                LoadCustomer(sender, e);
            }
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            Customer? customer = customersGrid.SelectedItem as Customer;
            if (customer == null) return;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Delete Customer", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DataAccess.DeleteCustomer(customer.Id);
                LoadCustomer(sender, e);
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            idTxt.Text = "";
            nameTxt.Text = "";
            addressTxt.Text = "";
            phoneNumberTxt.Text = "";
            emailTxt.Text = "";
        }
    }
}
