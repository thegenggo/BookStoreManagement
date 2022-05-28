using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement
{
    internal class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                CreateBooksTable(db);
                CreateCustomersTable(db);
                CreateTransactionsTable(db);
            }
        }

        private static void CreateBooksTable(SqliteConnection db)
        {
            String tableCommand = "CREATE TABLE IF NOT EXISTS Books ( " +
                "ISBN NVARCHAR(20) PRIMARY KEY, " +
                "Title NVARCHAR(100) NOT NULL, " +
                "Description NVARCHAR(255) NULL, " +
                "Price MONEY NOT NULL, " +
                "Quantity INTEGER NOT NULL)";

            SqliteCommand createTable = new SqliteCommand(tableCommand, db);

            createTable.ExecuteReader();
        }

        private static void CreateCustomersTable(SqliteConnection db)
        {
            String tableCommand = "CREATE TABLE IF NOT EXISTS Customers ( " +
                "Customer_Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Customer_Name NVARCHAR(100) NOT NULL, " +
                "Address NVARCHAR(255) NULL, " +
                "PhoneNumber NVARCHAR(20) NULL, " +
                "Email NVARCHAR(100) NULL)";

            SqliteCommand createTable = new SqliteCommand(tableCommand, db);

            createTable.ExecuteReader();
        }

        private static void CreateTransactionsTable(SqliteConnection db)
        {
            String tableCommand = "CREATE TABLE IF NOT EXISTS Transactions ( " +
                "Transactions_Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "ISBN NVARCHAR(20) NOT NULL, " +
                "Customer_Id NVARCHAR(20) NOT NULL, " +
                "Quantity INTEGER NOT NULL, " +
                "Total_Price MONEY NOT NULL, " +
                "FOREIGN KEY(ISBN) REFERENCES Books(ISBN), " +
                "FOREIGN KEY(Customer_Id) REFERENCES Customers(Customer_Id))";

            SqliteCommand createTable = new SqliteCommand(tableCommand, db);

            createTable.ExecuteReader();
        }

        public static void UpdateCustomer(Customer customer)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                String query = "UPDATE Customers SET Customer_Name = @name, Address = @address, PhoneNumber = @phoneNumber, Email = @email WHERE Customer_Id = @id";

                SqliteCommand updateCommand = new SqliteCommand(query, db);

                updateCommand.Parameters.AddWithValue("@name", customer.Name);
                updateCommand.Parameters.AddWithValue("@address", customer.Address);
                updateCommand.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                updateCommand.Parameters.AddWithValue("@email", customer.Email);
                updateCommand.Parameters.AddWithValue("@id", customer.Id);

                updateCommand.ExecuteReader();
            }
        }

        public static void UpdateBook(Book book)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                String query = "UPDATE Books SET Title = @title, Description = @description, Price = @price, Quantity = @quantity WHERE ISBN = @isbn";

                SqliteCommand updateCommand = new SqliteCommand(query, db);

                updateCommand.Parameters.AddWithValue("@title", book.Title);
                updateCommand.Parameters.AddWithValue("@description", book.Description);
                updateCommand.Parameters.AddWithValue("@price", book.Price);
                updateCommand.Parameters.AddWithValue("@quantity", book.Quantity);
                updateCommand.Parameters.AddWithValue("@isbn", book.Isbn);

                updateCommand.ExecuteReader();
            }
        }

        public static void DeleteCustomer(string id)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM Customers WHERE Customer_Id = @id", db);
                deleteCommand.Parameters.AddWithValue("@id", id);

                deleteCommand.ExecuteReader();
            }
        }

        public static void DeleteBook(string isbn)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM Books WHERE ISBN = @isbn", db);
                deleteCommand.Parameters.AddWithValue("@isbn", isbn);

                deleteCommand.ExecuteReader();
            }
        }

        public static void AddCustomer(Customer customer)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO Customers VALUES (NULL, @customerName, @address, @phoneNumber, @email)";
                insertCommand.Parameters.AddWithValue("@customerName", customer.Name);
                insertCommand.Parameters.AddWithValue("@address", customer.Address);
                insertCommand.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                insertCommand.Parameters.AddWithValue("@email", customer.Email);

                insertCommand.ExecuteReader();

            }
        }

        public static void AddBook(Book book)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO Books VALUES (@isbn, @title, @description, @price, @quantity)";
                insertCommand.Parameters.AddWithValue("@isbn", book.Isbn);
                insertCommand.Parameters.AddWithValue("@title", book.Title);
                insertCommand.Parameters.AddWithValue("@description", book.Description);
                insertCommand.Parameters.AddWithValue("@price", book.Price);
                insertCommand.Parameters.AddWithValue("@quantity", book.Quantity);

                insertCommand.ExecuteReader();

            }
        }

        public static List<Customer> GetCustomers(string id, string name, string address, string phoneNumber, string email)
        {

            List<Customer> customers = new List<Customer>();

            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                string command = "SELECT * FROM Customers WHERE 1=1";
                
                if (id != string.Empty)
                {
                    command += " AND Customer_Id = @id";
                    selectCommand.Parameters.AddWithValue("@id", id);
                }

                if (name != string.Empty)
                {
                    command += " AND Customer_Name LIKE @name";
                    selectCommand.Parameters.AddWithValue("@name", "%" + name + "%");
                }

                if (address != string.Empty)
                {
                    command += " AND Address LIKE @address";
                    selectCommand.Parameters.AddWithValue("@address", "%" + address + "%");
                }

                if (phoneNumber != string.Empty)
                {
                    command += " AND PhoneNumber = @phoneNumber";
                    selectCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                }

                if (email != string.Empty)
                {
                    command += " AND Email LIKE @email";
                    selectCommand.Parameters.AddWithValue("@email", "%" + email + "%");
                }

                selectCommand.CommandText = command;

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Customer customer = new Customer();
                    customer.Id = query.GetString(0);
                    customer.Name = query.GetString(1);
                    customer.Address = query.IsDBNull(2) ? "" : query.GetString(2);
                    customer.PhoneNumber = query.IsDBNull(3) ? "" : query.GetString(3);
                    customer.Email = query.IsDBNull(4) ? "" : query.GetString(4);

                    customers.Add(customer);
                }

                db.Close();
            }

            return customers;
        }

        public static List<Book> GetBooks(string isbn, string title, string description, string price, string quantity)
        {
            List<Book> books = new List<Book>();

            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                string command = "SELECT * FROM Books WHERE 1=1";

                if (isbn != string.Empty)
                {
                    command += " AND ISBN = @isbn";
                    selectCommand.Parameters.AddWithValue("@isbn", isbn);
                }

                if (title != string.Empty)
                {
                    command += " AND Title LIKE @title";
                    selectCommand.Parameters.AddWithValue("@title", "%" + title + "%");
                }

                if (description != string.Empty)
                {
                    command += " AND Description LIKE @description";
                    selectCommand.Parameters.AddWithValue("@description", "%" + description + "%");
                }

                if (price != string.Empty)
                {
                    command += " AND Price = @price";
                    selectCommand.Parameters.AddWithValue("@price", price);
                }

                if (quantity != string.Empty)
                {
                    command += " AND Quantity = @quantity";
                    selectCommand.Parameters.AddWithValue("@quantity", quantity);
                }

                selectCommand.CommandText = command;

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Book book = new Book();
                    book.Isbn = query.GetString(0);
                    book.Title = query.GetString(1);
                    book.Description = query.IsDBNull(2) ? "" : query.GetString(2);
                    book.Price = query.GetInt32(3);
                    book.Quantity = query.GetInt32(4);

                    books.Add(book);
                }

                db.Close();
            }

            return books;
        }
        
        public static Customer GetCustomer(int id)
        {
            Customer customer = new Customer();

            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "SELECT * FROM Customers WHERE Customer_Id = @id";
                selectCommand.Parameters.AddWithValue("@id", id);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    customer.Id = query.GetString(0);
                    customer.Name = query.GetString(1);
                    customer.Address = query.IsDBNull(2) ? "" : query.GetString(2);
                    customer.PhoneNumber = query.IsDBNull(3) ? "" : query.GetString(3);
                    customer.Email = query.IsDBNull(4) ? "" : query.GetString(4);
                }

                db.Close();
            }

            return customer;
        }
        
        public static Book GetBook(string isbn)
        {
            Book book;
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "SELECT * FROM Books WHERE ISBN = @isbn";
                selectCommand.Parameters.AddWithValue("@isbn", isbn);

                SqliteDataReader query = selectCommand.ExecuteReader();

                query.Read();

                book = new Book();
                book.Isbn = query.GetString(0);
                book.Title = query.GetString(1);
                book.Description = query.IsDBNull(2) ? "" : query.GetString(2);
                book.Price = query.GetInt32(3);
                book.Quantity = query.GetInt32(4);

                db.Close();
            }

            return book;
        }

        public static void BuyBook(string isbn, string customerId, int quantity, float totalPrice)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=BookStoreManagement.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price) VALUES (@isbn, @customerId, @quantity, @totalPrice)";
                insertCommand.Parameters.AddWithValue("@isbn", isbn);
                insertCommand.Parameters.AddWithValue("@customerId", customerId);
                insertCommand.Parameters.AddWithValue("@quantity", quantity);
                insertCommand.Parameters.AddWithValue("@totalPrice", totalPrice);

                insertCommand.ExecuteReader();
            }
        }
    }
}
