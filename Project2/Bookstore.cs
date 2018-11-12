using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{
    class Bookstore
    {
        private static Random random = new Random();
        private  int normal_books_order = random.Next(1, 10);     //at normal price, buy lesser books.
        private  int bulk_books_order = random.Next(11, 25);      //if there's a price cut, we order more books.
        public static bool PublishersAlive = true;
        private double unitPrice;
        private string PublisherId;
        private string BookstoreId;
        private bool bulk_order = false;
        private bool booksneeded = true;

        Service1 service1 = new Service1();     
        public static long[] cardnos = new long[5];  

        //Generating Credit Cards using Bank Service
        public void GenerateCC()
        {
            for(int k=0; k < 5; k++)
            {
                cardnos[k] = Convert.ToInt64(service1.CreateCreditCard(k));
            }
        }

        public static void setPublishersAlive(bool value)
        {
            PublishersAlive = value;
        }

        public void Run()
        {
            while (PublishersAlive)
            {
                //PricingModel pc = new PricingModel();
                OrderClass o = new OrderClass();
                if (booksneeded)
                {  
                    if (!bulk_order)

                        create_normal_order(PublisherId);
                    else
                        create_bulk_order(PublisherId);  //Publisher Id will be set when the event is triggered.
                }
                else
                {
                    //Console.WriteLine("BookStore : " + Thread.CurrentThread.Name + " is waiting...");
                    Thread.Sleep(1000);
                    booksneeded = true;
                }
            }
        }

        //Create Normal order when final price of books is greater than 100
        public void create_normal_order(String PublisherId)
        {
            
            OrderClass order = new OrderClass();   
            Random randm = new Random();
            normal_books_order= random.Next(1, 10);
            order.No_of_books = normal_books_order;
            order.BookstoreID = Thread.CurrentThread.Name;
            order.CardNo = cardnos[Convert.ToInt32(order.BookstoreID)];
            order.BookstoreID = Thread.CurrentThread.Name;
            order.PublisherId = PublisherId;
            booksneeded = false;
            string order_string = Encoder.Encode(order);
            Program.buffer.setOneCell(order_string);
        }
        //Create bulk order when final price of books is less than 100
        public void create_bulk_order(string PublisherId)
        {
           
            OrderClass order = new OrderClass();    //create an order object.
            Random randm = new Random();
            bulk_books_order= random.Next(11, 25);
            order.No_of_books = bulk_books_order;
            order.BookstoreID = Thread.CurrentThread.Name;
            order.CardNo = cardnos[Convert.ToInt32(order.BookstoreID)];
            order.PublisherId = PublisherId;
            booksneeded = false;                     //after you're done creating bulk order, set it to false to wait for next price cut.
            string order_string = Encoder.Encode(order);
            Program.buffer.setOneCell(order_string);
        }

        //subscribe to Publisher for price cut events.
        public void Subscribe(Publisher publisher)  
        {
            Console.WriteLine("Bookstore {0} is subscribing to price cut event",Thread.CurrentThread.Name);
            publisher.pricecutevent += issue_order;
        }

        public void issue_order(PriceCutEventHandler p)
        {
            bulk_order = true;
            PublisherId = p.Id;
            unitPrice = p.Price;
        }

    }
}
