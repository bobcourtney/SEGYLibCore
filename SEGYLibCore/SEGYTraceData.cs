using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savian.CoreConverters.Converters; // IBM floating point converter

namespace SEGYlib
{
    /// <summary>
    /// SEGYTraceData allows access to the contents of the binary trace data
    /// </summary>
    public class SEGYTraceData
    {
        private byte[] iTraceDataBuffer;
        private int iformat;
        private bool isbigendian;

        /// <summary>
        /// SEGYTraceData allows access to the contents of the binary trace data
        /// </summary>
        public SEGYTraceData()
        {

        }
        /// <summary>
        /// Initialize the class
        /// </summary>
        /// <param name="format">segy rev 1 data format</param>
        /// <param name="bigendian">true if data is big endian</param>
        public void Initialize(int format, bool bigendian)
        {
            iformat = format;
            isbigendian = bigendian;
        }
        /// <summary>
        /// access to byte[] trace data block
        /// </summary>
        public byte[] TraceDataBuffer
        {
            get
            {
                return iTraceDataBuffer;
            }
            set
            {
                iTraceDataBuffer = value;
            }
        }

        /// <summary>
        /// a double precision view of the trace data
        /// use this  to read and change the contents of the trace data buffer
        /// </summary>
        public double[] Data
        {

            get
            {
                byte[] buffer;
                double[] data = new double[0];
                if (iTraceDataBuffer != null)
                {
                    switch (iformat)
                    {
                        case 1:
                            // data is stored in IBM floating point format
                            Converter c = new Converter();
                            if (isbigendian)
                            {
                                Common.Endian = Endian.BigEndian;
                            }
                            else
                            {
                                Common.Endian = Endian.LittleEndian;
                            }

                            int wordLength = 4;
                             buffer = new byte[4];
                            data = new double[iTraceDataBuffer.Length / wordLength];
                            for (int i = 0; i < iTraceDataBuffer.Length / wordLength; i++)
                            {
                                Array.Copy(iTraceDataBuffer, i * wordLength, buffer, 0, wordLength);
                                data[i] = c.ConvertBytesToSingle(Platform.IbmFloat, buffer);
                            }
                            break;
                        case 2:
                            // data is stored int4 byte integer
                            wordLength = 4;
                            buffer = new byte[4];
                            data = new double[iTraceDataBuffer.Length / wordLength];
                            for (int i = 0; i < iTraceDataBuffer.Length / wordLength; i++)
                            {
                                Array.Copy(iTraceDataBuffer, i * wordLength, buffer, 0, wordLength);
                                if (isbigendian) Array.Reverse(buffer);
                                data[i] = BitConverter.ToInt32(buffer, 0);
                            }
                            break;
                        case 3:
                            // data is stored int2 byte integer
                            wordLength = 2;
                            buffer = new byte[wordLength];
                            data = new double[iTraceDataBuffer.Length / wordLength];
                            for (int i = 0; i < iTraceDataBuffer.Length / wordLength; i++)
                            {
                                Array.Copy(iTraceDataBuffer, i * wordLength, buffer, 0, wordLength);
                                if (isbigendian) Array.Reverse(buffer);
                                data[i] = BitConverter.ToInt16(buffer, 0);
                            }
                            break;
                        case 5:
                            // data is stored in IEEE float
                            wordLength = 4;
                            buffer = new byte[wordLength];
                            data = new double[iTraceDataBuffer.Length / wordLength];
                            for (int i = 0; i < iTraceDataBuffer.Length / wordLength; i++)
                            {
                                Array.Copy(iTraceDataBuffer, i * wordLength, buffer, 0, wordLength);
                                if (isbigendian) Array.Reverse(buffer);
                                data[i] = BitConverter.ToSingle(buffer, 0);
                            }
                            break;
                        case 8:
                            // data is stored in signed byte format
                            wordLength = 1;
                            data = new double[iTraceDataBuffer.Length / wordLength];
                            for (int i = 0; i < iTraceDataBuffer.Length / wordLength; i++)
                            {
                                data[i] = (sbyte)iTraceDataBuffer[i];
                            }
                            break;

                    }
                }

                return data;
                
            }
            set
            {
                // stop here
                double[] data = value ;
                if (iTraceDataBuffer != null)
                {
                    switch (iformat)
                    {
                        case 1:
                            // data is stored in IBM floating point format
                            Converter c = new Converter();
                            if (isbigendian)
                            {
                                Common.Endian = Endian.BigEndian;
                            }
                            else
                            {
                                Common.Endian = Endian.LittleEndian;
                            }

                            int wordLength = 4;
                            byte[] buffer = new byte[4];
                            for (int i = 0; i < data.Length; i++)
                            {
                                byte[] b = c.ConvertSingleToBytes(Platform.IbmFloat, (float)data[i]);
                                Array.Copy(b,0, iTraceDataBuffer, i * wordLength, wordLength);
                            }
                            break;
                        case 2:
                            // data is stored int4 byte integer
                            wordLength = 4;
                            buffer = new byte[4];
                            for (int i = 0; i < data.Length ; i++)
                            {
                                int val = (int)data[i];
                                buffer = BitConverter.GetBytes(val);
                                if (isbigendian) Array.Reverse(buffer);;
                                Array.Copy(buffer,0, iTraceDataBuffer, i * wordLength, wordLength);

                            }
                            break;
                        case 3:
                            // data is stored int2 byte integer
                            wordLength = 2;
                            buffer = new byte[2];
                            for (int i = 0; i < data.Length; i++)
                            {
                                short val = (short)data[i];
                                buffer = BitConverter.GetBytes(val);
                                if (isbigendian) Array.Reverse(buffer); ;
                                Array.Copy(buffer, 0, iTraceDataBuffer, i * wordLength, wordLength);

                            }
                            break;
                        case 5:
                            // data is stored in IEEE float
                            // data is stored int2 byte integer
                            wordLength = 4;
                            buffer = new byte[4];
                            for (int i = 0; i < data.Length; i++)
                            {
                                float val = (float)data[i];
                                buffer = BitConverter.GetBytes(val);
                                if (isbigendian) Array.Reverse(buffer); ;
                                Array.Copy(buffer, 0, iTraceDataBuffer, i * wordLength, wordLength);

                            }
                            break;
                        case 8:
                            // data is stored in signed byte format
                            wordLength = 1;
                            buffer = new byte[1];
                            for (int i = 0; i < data.Length; i++)
                            {
                                buffer[0] = (byte)data[i];;
                                Array.Copy(buffer, 0, iTraceDataBuffer, i * wordLength, wordLength);

                            }
                            break;

                    }
                }
                
            }
        }

        /// <summary>
        /// Use this if you want to change the data values as 
        /// SEGYTraceData.Data always returns values in the trace data buffer
        /// </summary>
        public double[] DataCopy
        {
            get
            {
                double[]  d = new double[Data.Length];
                Array.Copy(this.Data, d, Data.Length);
                return d;
            }
            set
            {
            }
        }
    }
}
