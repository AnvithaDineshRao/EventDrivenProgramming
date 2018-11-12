using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{
     class Publisher
    {

        private const Int32 Max_Price_Cuts = 20;        //after these many price cuts have been made, a publisher thread will terminate

        private int price_cut_count = 1; // Counter for the price cuts
        private double currentPrice = 0.0; // Current Unit Price of the books
        private double previousPrice = 0.0; // Previous Unit Price of the books
        private static Random random = new Random(); // Random number generator

        private ArrayList processingThreads = new ArrayList();

        public delegate void PriceCutHandler(PriceCutEventHandler p);
        public event PriceCutHandler pricecutevent;

        private void PriceCutEvent()
        {
            if (pricecutevent != null)
            {
                Console.WriteLine("\n Thread {1} performing price cut event {0}\n",Thread.CurrentThread.Name,price_cut_count);
                price_cut_count++;
                pricecutevent(new PriceCutEventHandler(Thread.CurrentThread.Name,currentPrice));
            }

            else
            {
                Console.WriteLine("\n No subscibers");
            }

           

        }

        public void Run()
        {
            while(price_cut_count <= Max_Price_Cuts)
            {
                SetPrice();

                if (currentPrice < previousPrice)
                {
                    PriceCutEvent();
                }

                ProcessOrder(GetOrder(),currentPrice);

            }


            foreach (Thread item in processingThreads)
            {
                while (item.IsAlive) ;
            }

           
        }

        //Calculate Original and Discounted price for a book
        public void SetPrice()
        {
            DateTime book_ordered_time = DateTime.Now.AddDays(random.Next(-10, -1));
         
            previousPrice = currentPrice;
            Random rand = new Random();
            currentPrice = PricingModel.calculatePrice(rand.Next(1,3), book_ordered_time);
            previousPrice = PricingModel.original_price;

        }

        private OrderClass GetOrder()
        {
            return Decoder.Decode(Program.buffer.getOneCell());
        }
        
        //Printing Order Confirmation
        public void ProcessOrder(OrderClass o,double currentPrice)
        {
            if(o.PublisherId == Thread.CurrentThread.Name)
            { 
                //need to add logic to this for processing order
                Console.WriteLine("Order for bookstore {0} received", Thread.CurrentThread.Name);
                OrderProcessing orderProcessing = new OrderProcessing(o);
                o.Current_Price_order = currentPrice;
                Thread p_thread = new Thread(new ThreadStart(orderProcessing.processOrder));
                processingThreads.Add(p_thread);
                p_thread.Name = "p_thread " + Thread.CurrentThread.Name;
                p_thread.Start();

            }

            else
            {
                Console.WriteLine("This order is not for Publisher {0} "+ Thread.CurrentThread.Name);
            }
        }
        


    }
}
