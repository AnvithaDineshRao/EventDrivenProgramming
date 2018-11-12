using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Project2
{
    class MultiCellBuffer
    {
        private const int N = 3;
        private const int WRITERS = 3;   //declaring 3 semaphores for write access
        private const int READERS = 3;      //DECLARING 3 READ SEMAPHORES. At ones we can allow upto 3 threads to read.
        private string[] buffer = new string[N];                  //declaring buffer to hold serialized objects.
        private Semaphore WRITE_SEMAPHORE;
        private Semaphore READ_SEMAPHORE;
        private int front;
        public int rear;
        public  int numElements;
        public MultiCellBuffer()
        {
            WRITE_SEMAPHORE = new Semaphore(WRITERS, WRITERS);
            READ_SEMAPHORE = new Semaphore(READERS, READERS);
           front = 0;
        rear = 0;
         numElements = 0;
    }
      

        public void setOneCell(string order)
        {
            //check availability
            WRITE_SEMAPHORE.WaitOne();      
            Console.WriteLine("Bookstore : " + Thread.CurrentThread.Name + " trying to place order");
            Console.WriteLine("Bookstore : " + Thread.CurrentThread.Name + " is waiting for empty slot in buffer...");

            //if the buffer had a cell available lock it
            lock (this)               
            {
                while (numElements == N)
                {
                    Monitor.Wait(this);   //wait for one of the cells to become free.
                }

                Console.WriteLine("Bookstore : " + Thread.CurrentThread.Name + " is writing to the buffer");

                buffer[rear] = order;
                rear = (rear + 1) % N;

                numElements++;

                Console.WriteLine("Bookstore : " + Thread.CurrentThread.Name + " wrote order " + order + " to multi cell buffer\n" + "Number of slots occupied " + numElements);

                WRITE_SEMAPHORE.Release();      //release when write finished.

                Monitor.Pulse(this);            //trigger the next thread waiting on this object.
                                                //               Console.WriteLine("Thread : " + Thread.CurrentThread.Name + " : front = " + front + " rear = " + rear + " Numelements = " + numElements);
                Console.WriteLine("Bookstore : " + Thread.CurrentThread.Name + " finished placing the order\n\n");
            }

           
        }

        public string getOneCell()
        {

            READ_SEMAPHORE.WaitOne();

            Console.WriteLine("Publisher : " + Thread.CurrentThread.Name + " is reading from the buffer..");

            lock (this)              //reader locks buffer.
            {
                Console.WriteLine("Publisher : " + Thread.CurrentThread.Name + " is waiting for an element to read from buffer...");
                XmlDocument doc = new XmlDocument();

                while (numElements == 0)
                {
                    //if buffer empty must wait.
                    Monitor.Wait(this);         
                }

                Console.WriteLine("Publisher " + Thread.CurrentThread.Name + " retrieved the order successfully");
            

                string order = "";
                order = buffer[front];

                doc.LoadXml(order);
                Console.WriteLine("doc : " + doc);
                //Creating XML document to be processed.
                XmlElement node = doc.GetElementById("PublisherId");

                if (node == null || Thread.CurrentThread.Name == node.InnerText)
                {
                    front = (front + 1) % N;
                    numElements--;

                }
                else
                {
                    Console.WriteLine("Could not extract order due to ID mismatch");
                }

              READ_SEMAPHORE.Release();
              Monitor.Pulse(this);
              return order;
                
                
            }

        }
    }
}

