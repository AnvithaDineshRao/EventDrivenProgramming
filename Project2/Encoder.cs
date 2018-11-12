using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project2
{
    class Encoder
    {
        public static string Encode(OrderClass o)
        {
            XmlSerializer serialize = new XmlSerializer(o.GetType());
            using (StringWriter swriter = new StringWriter())
            {
                serialize.Serialize(swriter, o);
                string encoded = swriter.ToString();
                return encoded;
            }
        }

    }
}
