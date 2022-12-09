using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savian.CoreConverters.Converters
{
    public static class Common
    {
        public static Endian Endian { get; set; }

        internal static void Initialize()
        {
            Endian = Endian.LittleEndian;
        }
    }
}
