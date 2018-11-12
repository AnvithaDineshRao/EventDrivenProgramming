using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class OrderClass

    {

        private string bookstoreID; // The identity of the bookstore
        private string publisherId; // The identity of the publisher
        private long cardNo; // An integer that represents a credit card number
        private int no_of_books; // Represents the number of books to order
        private DateTime timestamp = DateTime.Now; // Time the order is placed
        private double current_Price_order;
        public override string ToString()
        {
            return "ORDER\n\t{ID: " + BookstoreID
                + "}\n\t{RECEIVER_ID: " + PublisherId
                + "}\n\t{CARD_NO: " + CardNo
                + "}\n\t{NUMBER: " + No_of_books
                + "}\n\t{CREATED: " + Timestamp.ToString("d", CultureInfo.InvariantCulture) + "}";
        }

        public string BookstoreID { get => bookstoreID; set => bookstoreID = value; }
        public string PublisherId { get => publisherId; set => publisherId = value; }
        public long CardNo { get => cardNo; set => cardNo = value; }
        public int No_of_books { get => no_of_books; set => no_of_books = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public double Current_Price_order { get => current_Price_order; set => current_Price_order = value; }

        
    }
}
