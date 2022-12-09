using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEGYlib
{
    /// <summary>
    /// SEGYTrace is used to access and set SEGY rev 1 trace data
    /// </summary>
    public class SEGYTrace
    {
        private SEGYTraceData iSEGYTraceData;
        private SEGYTraceHeader iSEGYTraceHeader;
        private int iformat;
        private long iPositionOfTraceInFile;

        /// <summary>
        /// constructor
        /// </summary>

        public SEGYTrace()
        {

        }

        /// <summary>
        /// initilize trace structure
        /// </summary>
        /// <param name="isBigEndian">is the file big endian or little endian</param>
        /// <param name="format">format of data word length according to SEGY standard</param>
        public void Intialize(bool isBigEndian, int format)
        {
            iSEGYTraceData = new SEGYTraceData();
            iSEGYTraceData.Initialize(format, isBigEndian);
            iSEGYTraceHeader = new SEGYTraceHeader();
            iSEGYTraceHeader.Initialize(isBigEndian);
            iformat = format;
        }
        /// <summary>
        /// access to underlying Trace Header Class
        /// </summary>
        public SEGYTraceHeader TraceHeader
        {
            get
            {
                return iSEGYTraceHeader;
            }
            set
            {
                iSEGYTraceHeader = value;
            }
        }

        /// <summary>
        /// access to underlying Trace Data Class
        /// </summary>
        public SEGYTraceData TraceData
        {
            get
            {
                return iSEGYTraceData;
            }
            set
            {
                iSEGYTraceData = value;
            }
        }

        /// <summary>
        /// signal amplitude
        /// </summary>
        public double[] Data
        {
            get
            {
                return this.iSEGYTraceData.Data;
            }
            set
            {
                this.iSEGYTraceData.Data = value;
            }
        }

        /// <summary>
        /// DateTime of trace instance
        /// </summary>
        public DateTime timeTracedRecorded
        {
            get
            {
                DateTime t = new DateTime(); ;
                try
                { 
                 t = new DateTime(this.iSEGYTraceHeader.yearDataRecorded,1,1,this.iSEGYTraceHeader.hourOfDay, this.iSEGYTraceHeader.minuteOfHour, this.iSEGYTraceHeader.secondOfMinute);
                 t = t.AddDays(this.iSEGYTraceHeader.dayOfYear - 1);
                 t = t.AddMilliseconds(this.iSEGYTraceHeader.lagTimeAMsec + this.iSEGYTraceHeader.lagTimeBMsec);
                }
                catch (System.Exception e)
                {
                    int err = 1;
                }
                return t;
            }
            set
            {
                DateTime tin = value;
                this.iSEGYTraceHeader.yearDataRecorded  = (ushort) tin.Year;
                this.iSEGYTraceHeader.dayOfYear = (ushort)tin.DayOfYear;
                this.iSEGYTraceHeader.hourOfDay = (ushort)tin.Hour;
                this.iSEGYTraceHeader.minuteOfHour = (ushort)tin.Minute;
                this.iSEGYTraceHeader.secondOfMinute = (ushort)tin.Second;
                this.iSEGYTraceHeader.lagTimeBMsec = (short)tin.Millisecond;

            }
        }

        /// <summary>
        /// source position X corrected for scaling factors
        /// </summary>
        public double sourcePositionX
        {
            get
            {
                return SEGYUtilities.convertToPosition(this.iSEGYTraceHeader.sourceCoordinateX, this.iSEGYTraceHeader.coordinateUnits, this.iSEGYTraceHeader.scalarToBeAppliedToAllCoordinates);
            }
            set
            {
                this.iSEGYTraceHeader.sourceCoordinateX   = SEGYUtilities.convertPositionToint(value, this.iSEGYTraceHeader.coordinateUnits, this.iSEGYTraceHeader.scalarToBeAppliedToAllCoordinates);

            }
        }

        /// <summary>
        /// source position Y corrected for scaling factors
        /// </summary>
        public double sourcePositionY
        {
            get
            {
                return SEGYUtilities.convertToPosition(this.iSEGYTraceHeader.sourceCoordinateY,this.iSEGYTraceHeader.coordinateUnits,this.iSEGYTraceHeader.scalarToBeAppliedToAllCoordinates);
            }
            set
            {
                this.iSEGYTraceHeader.sourceCoordinateY = SEGYUtilities.convertPositionToint(value, this.iSEGYTraceHeader.coordinateUnits, this.iSEGYTraceHeader.scalarToBeAppliedToAllCoordinates);

            }
        }

        /// <summary>
        /// is it a lat/lon position or projected
        /// </summary>
        public bool isLatLon
        {
            get
            {
                if ( this.iSEGYTraceHeader.coordinateUnits == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
            }
        }

        /// <summary>
        /// position in bytes
        /// </summary>
        public long positionOfTraceInFile
        {
            get
            {
                return iPositionOfTraceInFile;
            }
            set
            {
                iPositionOfTraceInFile = value;
            }
        }

        /// <summary>
        /// true if big endian
        /// </summary>
        public bool isBigEndian
        {
            get
            {
                return iSEGYTraceHeader.bigEndian;
            }
            set
            {
            }
        }

        /// <summary>
        /// total number of bytes of trace data in including trace header
        /// </summary>
        public int totalLengthOfTraceData
        {
            get
            {
                if (this.iSEGYTraceData.TraceDataBuffer != null)
                { 
                    return this.iSEGYTraceHeader.TraceHeaderBuffer.Length + this.iSEGYTraceData.TraceDataBuffer.Length;
                } else {
                    return this.iSEGYTraceHeader.TraceHeaderBuffer.Length;
                }
            }
            set
            {
            }
        }

        /// <summary>
        /// GSCA implemententation of group position
        /// </summary>
        public double groupPositionXGSCDIG
        {
            get
            {
                return SEGYUtilities.convertToPosition(this.iSEGYTraceHeader.groupCoordinateX, 3, -1e6);
            }
            set
            {
            }
        }

        /// <summary>
        /// GSCA implemententation of group position
        /// </summary>
        public double groupPositionYGSCDIG
        {
            get
            {
                return SEGYUtilities.convertToPosition(this.iSEGYTraceHeader.groupCoordinateY, 3, -1e6);
            }
            set
            {
            }
        }

        /// <summary>
        /// trace time in DDDHHHMMSSmmm
        /// </summary>
        public long codedTime
        {
            get
            {
                // use this to generate dddhhmmssmmm formatted time code
                int day = this.iSEGYTraceHeader.dayOfYear; ;
                int hr = this.iSEGYTraceHeader.hourOfDay ;
                int min = this.iSEGYTraceHeader.minuteOfHour;
                int sec = this.iSEGYTraceHeader.secondOfMinute;
                int msec = this.iSEGYTraceHeader.lagTimeAMsec + this.iSEGYTraceHeader.lagTimeBMsec;
                return msec +  1000*( (long) (sec + 100 * (min + 100 * (hr + 100 * day))));

            }
            set
            {
            }
        }

        /// <summary>
        /// make a deep copy of a SEGY Trace
        /// </summary>
        /// <returns>pointer to a deep copy of the input trace</returns>
        public SEGYTrace Copy()
        {
            SEGYTrace newTrace = new SEGYTrace();

            newTrace.Intialize(this.iSEGYTraceHeader.bigEndian, iformat);

             byte[] newTraceDataBuffer = null;
            if (this.iSEGYTraceData.TraceDataBuffer != null  )
            { 
                newTraceDataBuffer = new byte[this.iSEGYTraceData.TraceDataBuffer.Length];
                Array.Copy(this.iSEGYTraceData.TraceDataBuffer, newTraceDataBuffer,this.iSEGYTraceData.TraceDataBuffer.Length  ); 
            }  // copies value contents

            byte[] newTraceHeaderBuffer = new byte[this.iSEGYTraceHeader.TraceHeaderBuffer.Length];
            Array.Copy(this.iSEGYTraceHeader.TraceHeaderBuffer,newTraceHeaderBuffer , this.iSEGYTraceHeader.TraceHeaderBuffer.Length);

            newTrace.iSEGYTraceData.TraceDataBuffer = newTraceDataBuffer;
            newTrace.iSEGYTraceHeader.TraceHeaderBuffer = newTraceHeaderBuffer;

            newTrace.positionOfTraceInFile = -1; // trace has not been written to a file yet;

            return newTrace;
        }

        /// <summary>
        /// write a trace to a BinaryWriter stream
        /// </summary>
        /// <param name="bw">output stream pointer</param>
        /// <returns>true if successful</returns>
        public bool Write(System.IO.BinaryWriter bw)
        {
            if (bw == null) return false;
            this.positionOfTraceInFile = bw.BaseStream.Position;

            SEGYTrace trace = this;

            byte[] bytesToWrite = trace.TraceHeader.TraceHeaderBuffer;
            bw.Write(bytesToWrite);

            bytesToWrite = trace.TraceData.TraceDataBuffer;
            bw.Write(bytesToWrite);

            return true;
        }

        /// <summary>
        /// transcribe msec field in old GSC format
        /// the old GSC formatted files used the Time Basis Field 166-167 for storing msec field
        /// should use lag b or lag A field
        /// this copies 166-167 to 106-107
        /// </summary>
        public void FixMsecField()
        {
            // the old GSC formatted files used the Time Basis Field 166-167 for storing msec field
            // should use lag b or lag A field
            // this copies 166-167 to 106-107
            this.iSEGYTraceHeader.TraceHeaderBuffer[106] = this.iSEGYTraceHeader.TraceHeaderBuffer[166];
            this.iSEGYTraceHeader.TraceHeaderBuffer[107] = this.iSEGYTraceHeader.TraceHeaderBuffer[167];
        }

        /// <summary>
        /// resize the trace by truncation or zero padding
        /// </summary>
        public void Resize(int newsize)
        {
            int oldsize = this.TraceHeader.numberOfSamplesInTrace;
            int nb = newsize;
            if ( this.iformat == 1 || iformat == 2 || iformat  == 5 )
            {
                nb *= 4;
            } 
            else if ( iformat == 3)
            {
                nb *= 2;
            }
            byte [] newbuff = new byte[nb];
            Array.Clear(newbuff,0,nb);
            if ( nb >= iSEGYTraceData.TraceDataBuffer.Length)
            {
                Array.Copy(this.iSEGYTraceData.TraceDataBuffer, newbuff, iSEGYTraceData.TraceDataBuffer.Length);
            } else {
                Array.Copy(this.iSEGYTraceData.TraceDataBuffer, newbuff, nb);
            }

            this.TraceData.TraceDataBuffer = newbuff;
            this.TraceHeader.numberOfSamplesInTrace = (ushort)newsize;

        }
        public void Resize(int newsize, int istart)
        {
            int oldsize = this.TraceHeader.numberOfSamplesInTrace;
            int nb = newsize;
            int ncopy = oldsize;
            if (ncopy > (newsize - istart)) ncopy = (newsize - istart);
            if (this.iformat == 1 || iformat == 2 || iformat == 5)
            {
                nb *= 4;
                ncopy *= 4;
                istart *= 4;
            }
            else if (iformat == 3)
            {
                nb *= 2;
                ncopy *= 2;
                istart *= 2;
            }
            byte[] newbuff = new byte[nb];
            Array.Clear(newbuff, 0, nb);


            Array.Copy(this.iSEGYTraceData.TraceDataBuffer, 0, newbuff,istart,  ncopy);
  
            this.TraceData.TraceDataBuffer = newbuff;
            this.TraceHeader.numberOfSamplesInTrace = (ushort)newsize;

        }

    }
}
