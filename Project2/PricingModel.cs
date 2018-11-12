using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace Project2
{
    /* Idea : 
     if timespan between order placed and today is less and number of orders to process is less,you get less discount.
     if timespan between order placed and today is more and number of orders to process is more,you get more discount*/

    public class PricingModel
    {
        //Threshold for number of books ordered.
        private const double BOOKS_ORDERED_NUMBER_LOW = 5;
        private const double BOOKS_ORDERED_NUMBER_MID = 10;
        private const double BOOKS_ORDERED_NUMBER_HIGH = 50;

        //Time span i.e Difference between current day and timestamp of creating the order request
        private const double BOOKS_TIMESPAN_LOW = 4;
        private const double BOOKS_TIMESPAN_MID = 6;
        private const double BOOKS_TIMESPAN_HIGH = 10;

        //Discount based on number of orders to be processed.
        private const double DISCOUNT_ORDERED_LOW = 1.25;
        private const double DISCOUNT_ORDERED_MID = 1.00;
        private const double DISCOUNT_ORDERED_HIGH = 0.25;  

        //Based on the timespan ,we offer discounts.
        private const double DISCOUNT_AVAILABLE_LOW = 1.50;
        private const double DISCOUNT_AVAILABLE_MID = 1.00;
        private const double DISCOUNT_AVAILABLE_HIGH = 0.70;
        private const double DISCOUNT_AVAILABLE_MAX = 0.5;
        public static double original_price=0.0;
        public static double final_price = 0.0;
        private static Random random = new Random();


        /*Calculates the Original price of the book,that is between 50 to 200 and returns the final price of the book after discount*/
        public static double calculatePrice(int orders_number, DateTime book_ordered_time)
        {
            //price of each book between 50 and 200.
            original_price = random.Next(50, 200);
            Console.WriteLine("Book Ordered time {0} and number of orders {1}", book_ordered_time, orders_number);
            Console.WriteLine("Original Price of the Book is {0}", original_price);
            final_price = original_price * calculateDiscountOrderedBooks(orders_number) * calculateDiscountAvailableBooks(book_ordered_time);
            Console.WriteLine("Price of the book After Discount is {0}\n", final_price);
            return final_price;
        }
        //If the timespan is large,then they get higher discount.As they had subscribed for it first
        public static double calculateDiscountAvailableBooks(DateTime book_ordered_time)
        {
            TimeSpan span = DateTime.Now - book_ordered_time;
            String number_day = span.Days.ToString();

            int av = Int32.Parse(number_day);
         //   Console.WriteLine(av);

            if (av <= BOOKS_TIMESPAN_LOW)
                return DISCOUNT_AVAILABLE_LOW;
            else if (av <= BOOKS_TIMESPAN_MID)
                return DISCOUNT_AVAILABLE_MID;
            else if (av <= BOOKS_TIMESPAN_HIGH)
                return DISCOUNT_AVAILABLE_HIGH;
            else
                return DISCOUNT_AVAILABLE_MAX;
        }
       
        //Calculating discount based on number of orders processed.
        //Based on the number of elements in the buffer,which we assume needs immediate processing 
        public static double calculateDiscountOrderedBooks(int orders_number)
        {
            if (orders_number == 1)
                return DISCOUNT_ORDERED_LOW;
            else if (orders_number == 2)
                return DISCOUNT_ORDERED_MID;
            else
                return DISCOUNT_ORDERED_HIGH;
        }
    }
            
 }
    



