using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEGYlib
{
    /// <summary>
    /// SEGYUtilities for use in reading and writing SEGY files
    /// </summary>
    public class SEGYUtilities
    {

        /// <summary>
        /// convert an ASCII byte array to an EBCDIC byte array
        /// </summary>
        /// <param name="asciiData">input ASCII-formatted byte array</param>
        /// <returns>byte array containing EBCDIC formatted text data</returns>
        public static byte[] ConvertAsciiToEbcdic(byte[] asciiData)
        {
            // Create two different encodings.         
            Encoding ascii = Encoding.ASCII;
            Encoding ebcdic = Encoding.GetEncoding("IBM037");

            //Retutn Ebcdic Data
            return Encoding.Convert(ascii, ebcdic, asciiData);
        }

        /// <summary>
        /// convert an EBCDIC  byte array to an ASCII byte array
        /// </summary>
        /// <param name="ebcdicData">input EBCDIC array</param>
        /// <returns>byte array containing ASCII formatted text data</returns>
        public static byte[] ConvertEbcdicToAscii(byte[] ebcdicData)
        {
            // Create two different encodings.      
            Encoding ascii = Encoding.ASCII;
            Encoding ebcdic = Encoding.GetEncoding("IBM037");

            //Return Ascii Data 
            return Encoding.Convert(ebcdic, ascii, ebcdicData);
        }

        /// <summary>
        /// convert bytes to long int
        /// </summary>
        /// <param name="byteArray">input byte array</param>
        /// <param name="startPosition">starting position</param>
        /// <param name="length">length of byte array</param>
        /// <param name="signed">is the value signed</param>
        /// <param name="swap">swap the bytes first</param>
        /// <returns>long integer converted from byte array</returns>
        public static long Bytes2Int(byte[] byteArray, int startPosition, int length, bool signed, bool swap)
        {
            long integer = 0;
            byte[] subArray = new byte[length];

            Array.Copy(byteArray, startPosition, subArray, 0, length);

            if (swap) Array.Reverse(subArray);

            if(length == 1)
            {
                if(signed)
                {
                    integer = Convert.ToSByte(subArray[0]);
                }
                else
                {
                    integer = Convert.ToByte(subArray[0]);
                }
            }
            else if( length == 2 )
            {
                if(signed)
                {
                    integer = BitConverter.ToInt16(subArray,0);
                }
                else
                {
                    integer = BitConverter.ToUInt16(subArray,0);
                }

            }
            else if( length == 4 )
            {
                if(signed)
                {
                    integer = BitConverter.ToInt32(subArray,0);
                }
                else
                {
                    integer = BitConverter.ToUInt32(subArray,0);
                }

            }
            return integer;
            
        }

        /// <summary>
        /// convert a long int to bytes
        /// </summary>
        /// <param name="integer">input long int</param>
        /// <param name="signed">is the value signed</param>
        /// <param name="byteArray">output byte array</param>
        /// <param name="start">starting position in output array</param>
        /// <param name="length">length of output word</param>
        /// <param name="swap">swap the output byte array</param>
        /// <returns>byte array converted from long integer</returns>
        public static void Int2Bytes(long integer, bool signed, byte[] byteArray, int start, int length, bool swap)
        {
            byte[] subArray = new byte[length];

             if( length == 1)
             {
                 subArray = new byte[1];

                 if(signed)
                 {
                     sbyte s = (sbyte) integer;
                     subArray[0] = (byte)s;
                 }
                 else
                 {
                     subArray[0] = (byte)integer;
                 }
             }
            else if ( length == 2)
             {

                 if (signed)
                 {
                     Int16 s = (Int16)integer;
                     subArray = BitConverter.GetBytes(s);
                 }
                 else
                 {
                     UInt16 s = (UInt16)integer;
                     subArray = BitConverter.GetBytes(s);
                 }
             }
             else if (length == 4)
             {

                 if (signed)
                 {
                     Int32 s = (Int32)integer;
                     subArray = BitConverter.GetBytes(s);
                 }
                 else
                 {
                     UInt32 s = (UInt32)integer;
                     subArray = BitConverter.GetBytes(s);
                 }
             }


             if (swap) Array.Reverse(subArray);
             Array.Copy(subArray, 0, byteArray, start, length);
        }

        /// <summary>
        /// convert a SEGY trace header positional value to position
        /// </summary>
        /// <param name="x">segy rev 1 trace header position</param>
        /// <param name="coordinateSystem">segy rev 1 trace header coordinate system</param>
        /// <param name="scalarToBeAppliedToAllCoordinates">segy rev 1 trace header scalarToBeAppliedToAllCoordinates</param>
        /// <returns>a decimal position calculated using coordinateSystem and scalarToBeAppliedToAllCoordinates </returns>
        public static double convertToPosition( int x, ushort coordinateSystem, double scalarToBeAppliedToAllCoordinates)
        {

            double d = (double)x;

            // apply scalar
            if (  scalarToBeAppliedToAllCoordinates < 0 )
            {
                d /= -scalarToBeAppliedToAllCoordinates;
            } else if (  scalarToBeAppliedToAllCoordinates > 0 )
            {
                d *= scalarToBeAppliedToAllCoordinates;
            }

            // if in seconds of arc or DMS format then transform;
            if ( coordinateSystem == 2 )
            {
                d = secondsOfArctoDegrees(d);

            } else if ( coordinateSystem == 4 )
            {
                d = dmsToDecimalDegrees(d);
            }

            return d;
        }
        /// <summary>
        /// convert a position to a SEGY trace header integer
        /// </summary>
        /// <param name="d">input position</param>
        /// <param name="coordinateSystem">segy rev 1 trace header coordinate system</param>
        /// <param name="scalarToBeAppliedToAllCoordinates">segy rev 1 trace header scalarToBeAppliedToAllCoordinates</param>
        /// <returns>an integer value calculated using coordinateSystem and scalarToBeAppliedToAllCoordinates </returns>
        public static int convertPositionToint(double d, ushort coordinateSystem, double scalarToBeAppliedToAllCoordinates)
        {

            // if in seconds of arc or DMS format then transform;
            if (coordinateSystem == 2)
            {
                d = degreesToSecondsOfArc(d);
            }
            else if (coordinateSystem == 4)
            {
                d = decimalDegreesToDMS(d);
            }

            // apply scalar
            if (scalarToBeAppliedToAllCoordinates < 0)
            {
                d *= -scalarToBeAppliedToAllCoordinates;
            }
            else if (scalarToBeAppliedToAllCoordinates > 0)
            {
                d /= scalarToBeAppliedToAllCoordinates;
            }



            return (int) d;
        }
        /// <summary>
        /// convert seconds of arc to decimal degrees
        /// </summary>
        /// <param name="secOfArc">input seconds of arc</param>
        /// <returns>a decimal position calculated using seconds of arc </returns>
        public static double secondsOfArctoDegrees(double secOfArc)
        {
            return secOfArc/3600.0;
        }

        /// <summary>
        /// convert decimal degrees to  seconds of arc
        /// </summary>
        /// <param name="degrees">input decimal degrees</param>
        /// <returns>seconds of arc </returns>
        public static double degreesToSecondsOfArc(double degrees)
        {
            return degrees*3600.0;
        }

        /// <summary>
        /// convert decimal degrees to  degrees-minutes-seconds
        /// </summary>
        /// <param name="dg">input decimal degrees</param>
        /// <returns>DDDMMSS </returns>
        public static double decimalDegreesToDMS(double dg)
        {
            // DDDMMSS.ss input format
            int sign = 1;
            double degrees = dg;

            if (degrees < 0)
            {
                sign = -1;
                degrees *= sign;
            }
            int ddd = (int)degrees;
            double remainder = 60 * (degrees - ddd);

            int mm = (int)remainder;
            remainder = 60 * (remainder - mm);

            int ss = (int)remainder;


            return sign * (degrees*10000 + ss * 100 + ss );

        }
        /// <summary>
        /// convert   degrees-minutes-seconds to decimal degrees
        /// </summary>
        /// <param name="DDDMMSS">input DDDMMSS</param>
        /// <returns>a decimal position </returns>
        public static double dmsToDecimalDegrees(double DDDMMSS)
        {
            // DDDMMSS.ss input format
            int sign = 1;
            if ( DDDMMSS < 0 ) 
            {
                sign = -1;
                DDDMMSS *= sign;
            }
            double degrees = (int)(DDDMMSS / 10000);
            double remainder = DDDMMSS - degrees*1e4;

            double minutes = (int)remainder / 100;
            remainder = remainder - minutes * 100;
            double sec = remainder;
            return sign*(degrees + minutes*60 + sec*3600.0);

        }
    }
}
