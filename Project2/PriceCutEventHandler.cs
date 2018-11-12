using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class PriceCutEventHandler
    {

        private double price;
        private string id;

        public PriceCutEventHandler(string id, double price)
        {
            this.Id = id;
            this.Price = price;
        }

        public double Price
        { get => price;
        set => price = value; }


        public string Id
        { get => id;
        set => id = value; }
    }
}
