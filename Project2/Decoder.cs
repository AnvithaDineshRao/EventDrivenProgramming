using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project2
{
    class Decoder
    {

        public static OrderClass Decode(string order)
        {
            XmlSerializer xml_deserializer = new XmlSerializer(typeof(OrderClass));
            using (TextReader treader = new StringReader(order))
            {
                object deserialized_order = xml_deserializer.Deserialize(treader);
                return (OrderClass)deserialized_order;
            }
        }

    }
}
