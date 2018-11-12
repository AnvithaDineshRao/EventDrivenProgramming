using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{
    class Program
    {
       
        private const int K = 3; // Number of Publishers
        private const int N = 5; // Number of Bookstores

        private static Thread[] p_threads = new Thread[K];
        private static Thread[] b_threads = new Thread[N];
        private static Publisher[] publishers = new Publisher[K];

        public static MultiCellBuffer buffer = new MultiCellBuffer();

      
        //The main entry point for the application.
       
        static void Main(string[] args)
        {
            Service1 service1 = new Service1();
            service1.GetData();
            Bookstore b1 = new Bookstore();
            b1.GenerateCC();

            for (int i = 0; i < K; ++i)
            {
                Publisher publisher = new Publisher();
                publishers[i] = publisher;
                p_threads[i] = new Thread(publisher.Run);
                p_threads[i].Name = "Publisher: " + i;
                p_threads[i].Start();
                while (!p_threads[i].IsAlive);
            }

            

            // Initialize the Bookstores
            for (int i = 0; i < N; ++i)
            {
                Bookstore bookstore = new Bookstore();

               // Bookstores Subscribe to the Price Cut event
                for (int j = 0; j < K; ++j)
                {
                    
                    bookstore.Subscribe(publishers[j]);
                }

                b_threads[i] = new Thread(bookstore.Run);
                b_threads[i].Name =  i.ToString();
                b_threads[i].Start();
                while (!b_threads[i].IsAlive) ;
            }

            // Wait for the publishers to max price cuts (20 in our case)
            for (int i = 0; i < K; ++i)
            {
                while (p_threads[i].IsAlive) ;
            }

            // Bookstores are informed that publishers are not active
            for (int i = 0; i < N; ++i)
            {
                Bookstore.PublishersAlive = false;
               
            }

            //Wait for all the bookstore threads to terminate
            Console.WriteLine("Waiting for all the bookstore threads to terminate");
            for (int i = 0; i < N; ++i)
            {
                
                while (b_threads[i].IsAlive) ;
            }

            Console.WriteLine("\n\nEnd of Program");

            
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
