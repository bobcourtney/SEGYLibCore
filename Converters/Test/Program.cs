using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savian.CoreConverters;
using Savian.CoreConverters.Converters;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Converter c =new Converter();
            byte[] bytes = new byte[] { 0x00, 0x00, 0x00, 0x80, 0x31, 0x93, 0x60, 0x48 };
            double d = c.ConvertBytesToDouble(Platform.IbmFloat, bytes);

            Console.WriteLine("Correct return value should be: " + 1620259200);
            Console.WriteLine("Return Value:                   " + d);
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
