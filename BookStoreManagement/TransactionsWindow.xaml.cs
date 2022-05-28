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
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        Book? book;
        Customer? customer;

        public TransactionsWindow()
        {
            InitializeComponent();
        }

        private void SearchBook(object sender, TextChangedEventArgs e)
        {
            try
            {
                book = DataAccess.GetBook(isbnTxt.Text);
                titleTxt.Text = book.Title;
                descriptionTxt.Text = book.Description;
                priceTxt.Text = book.Price.ToString() + " THB";
                if (quantityTxt.Text != "")
                {
                    UpdateTotalPrice(sender, e);
                }
            }
            catch (Exception)
            {
                book = null;
                titleTxt.Text = "";
                descriptionTxt.Text = "";
                priceTxt.Text = "";
            }
        }

        private void SearchCustomer(object sender, TextChangedEventArgs e)
        {
            try
            {
                customer = DataAccess.GetCustomer(int.Parse(customerIdTxt.Text));
                nameTxt.Text = customer.Name;
            }
            catch (Exception)
            {
                customer = null;
                nameTxt.Text = "";
            }
        }

        private void Buy(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to buy this book?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (book != null && customer != null)
                {
                    try
                    {
                        DataAccess.BuyBook(book.Isbn, customer.Id, int.Parse(quantityTxt.Text), CalculateTotalPrice());
                        MessageBox.Show("Book bought successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        //Result
                        string resultStr = "";
                        resultStr += "Book: " + book.Title + "\n";
                        resultStr += "Customer: " + customer.Name + "\n";
                        resultStr += "Quantity: " + quantityTxt.Text + "\n";
                        resultStr += "Total Price: " + CalculateTotalPrice() + " THB";
                        MessageBox.Show(resultStr, "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                        Clear();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please enter quantity in integer format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a book and a customer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateTotalPrice(object sender, TextChangedEventArgs e)
        {
            totalPriceTxt.Text = CalculateTotalPrice().ToString() + " THB";
        }

        private float CalculateTotalPrice()
        {
            if (book != null && quantityTxt.Text != "")
            {
                return book.Price * int.Parse(quantityTxt.Text);
            }
            else
            {
                return 0;
            }
        }

        private void Clear()
        {
            book = null;
            customer = null;
            isbnTxt.Text = "";
            customerIdTxt.Text = "";
            quantityTxt.Text = "";
            titleTxt.Text = "";
            descriptionTxt.Text = "";
            priceTxt.Text = "";
            nameTxt.Text = "";
        }
    }
}
