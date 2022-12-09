using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savian.CoreConverters.Converters
{
    public enum Endian
    {
        BigEndian = 1,
        LittleEndian = 2,
    }

    public enum FloatType
    {
        IeeeSingleFloat,
        IeeeDoubleFloat,
        IbmSingleFloat,
        IbmDoubleFloat,
        VaxSingleFloat
    }

    public enum MethodNumber
    {
        ByMultiple,
        ByLog
    }

    public enum Platform
    {
        IbmFloat,
        IeeeFloat,
        VaxFloat,
    }
}
