using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{
    class OrderProcessing
    {
        static double tax = 0.1;
        static object order_num = 0;
        int local_order_num = 0;


        

        OrderClass order;
        public OrderProcessing(OrderClass order)
        {
            this.order = order;
            lock (order_num)
            {
                order_num = (int)order_num + 1;
                local_order_num = (int)order_num;
            }
         
        }

        public void processOrder()
        {
            /*Calculate price based on the no of books and order timestamp.*/
            double book_price = order.Current_Price_order;

            book_price = book_price * (1.0 + tax);  //adding 10% tax to the price of book.
            Console.WriteLine("Verify credit card no...");
            /*Implement credit card validation function here*/
            
            
            Service1 myservice = new Service1();
            String encrypted_card_no = myservice.encryptCreditCard(order.CardNo.ToString());
            if (myservice.CheckValidTransaction(encrypted_card_no,order.No_of_books*book_price))
            {
                Console.WriteLine("Valid credit card for {0}", Thread.CurrentThread.Name);
                Console.WriteLine("The order details are : \n");
                Console.WriteLine("BookStore ID : " + order.BookstoreID);
                Console.WriteLine("Card no : " + order.CardNo);
                Console.WriteLine("No of books : " + order.No_of_books);
                Console.WriteLine("Timestamp : " + order.Timestamp);


                String confirmation_message = "Your order has been confirmed! Please store the receipt for your records:" +
                    "\n -------------------------------------------\n" + "Order Number : " + local_order_num +
                    " \nBookstore ID : " + order.BookstoreID + "\nNo of books : " +
                    order.No_of_books + "\nPrice per book : " + book_price + "\nTotal Price : " + order.No_of_books * book_price +
                    "\n------------------------------\n";
                Console.WriteLine(confirmation_message); /*Write confirmation message out to screen*/
            }

            else
            {
                Console.WriteLine("Invalid credit card for {0}", Thread.CurrentThread.Name);

            }
            
        }

        

    }
}
