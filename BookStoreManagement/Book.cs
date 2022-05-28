using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStoreManagement
{
    public class Book
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public Book()
        {
            Isbn = "";
            Title = "";
            Description = "";
            Price = 0;
            Quantity = 0;
        }
    }
}