using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEGYlib
{
    /// <summary>
    /// Class for storing and retrieving data stored in the SEGY file Header
    /// </summary>
    public class SEGYFileHeader
    {

        private BinaryReader br;
        private BinaryWriter bw;
        private bool detectedSEGYVer0 ;
        /// <summary>
        /// includes start tape header
        /// </summary>
        private List<Byte[]> iExtendedTextHeader;
        private byte[] iBinaryFileHeader;
        private bool BigEndian;
        /// <summary>
        /// file position of start of trace data
        /// </summary>
        public long positionOfStartOfDataTraces;
        /// <summary>
        /// true if Text Header is ASCII; false if EBCDIC
        /// </summary>
        public bool isSEGYFileHeaderAscii;


        /// <summary>
        /// constructor
        /// </summary>
        public SEGYFileHeader()
        {
            br = null;
            iExtendedTextHeader = new List<byte[]>();
            iBinaryFileHeader =  new byte[400];
            BigEndian = false; // by default, segy is little endian
            detectedSEGYVer0 = false; // some SEG's rev 0 have crap in the wrong places;
        }

        /// <summary>
        /// lead 3200 byte tape header plus any other extended blocks
        /// </summary>
 
        public List<byte[]> ExtendedTextHeader
        {
            get
            {
                return iExtendedTextHeader; 
            }
            set
            {
                iExtendedTextHeader = value;
            }
        }

        /// <summary>
        /// access to byte block of Binary File header
        /// </summary>
        public byte[] BinaryFileHeader
        {
            get
            {
                return iBinaryFileHeader;
            }
            set
            {
                iBinaryFileHeader = value;
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 dataSampleFormatCode
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 24, 2, true, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 24, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public uint jobIdentificationNumberz
        {
            get
            {
                return (uint)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 0, 4, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 0, 4, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public uint lineNumber
        {
            get
            {
               return (uint)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 4, 4, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 4, 4, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public uint reelNumber
        {
            get
            {
                return (uint)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 8, 4, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 8, 4, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 numberOfDataTracesPerEnsemble
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 12, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 12, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 numberOfAuxilaryTracesPerEnsemble
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 14,2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 14, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sampleIntervalInMicroseconds
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 16, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 16, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sampleIntervalInMicrosecondsInOriginalFieldRecording
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 18, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 18, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 numberOfSamplesPerDataTrace
        {
            get
            {
                 return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 20, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 20, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 numberOfSamplesPerDataTraceForOriginalFieldRecording
        {
            get
            {
                 return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 22, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 22, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 ensembleFold
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 26, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 26, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public Int16 traceSortingCode
        {
            get
            {
                 return (Int16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 22, 2, true, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, iBinaryFileHeader, 22, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 verticalSumCode
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 30, 2, false, BigEndian);
            }

             set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 30, 2, BigEndian);
            }

        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sweepFrequencyStart
        {
            get
            {
               return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 32, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 32, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sweepFrequencyEnd
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 34, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 34, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sweepLength
        {
            get
            {
                 return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 36, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 36, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sweepCode
        {
            get
            {
                 return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 38, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 38, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 traceNumberSweepChannel
        {
            get
            {
                 return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 40, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 40, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sweepTraceTaperLengthAtStart
        {
            get
            {
               return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 42, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 42, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 sweepTraceTaperLengthAtEnd
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 44, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 44, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 taperType
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 46, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 46, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 correlatedDataTraces
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 48, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 48, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 binaryGainRecovered
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 50, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 50, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 amplitudeRecoveryMethod
        {
            get
            {
               return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 52, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 52, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 measurementSystem
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 54, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 54, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 impulseSignalPolarity
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 56, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 56, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 vibratoryPolarityCode
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 58, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 58, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 segyFormatRevisionNumber
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 300, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 300, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 fixedLengthTraceFlag
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 302, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 302, 2, BigEndian);
            }
        }

        /// <summary>
        /// attribute  defined though segy rev 1 standard
        /// </summary>
        public UInt16 numberOfExtendedTextualFileHeaderRecordsFollowing
        {
            get
            {
                return (UInt16)SEGYUtilities.Bytes2Int(iBinaryFileHeader, 304, 2, false, BigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, iBinaryFileHeader, 304, 2, BigEndian);
            }
        }

        /// <summary>
        /// byte length of file header including extended tape header and binary file header
        /// </summary>
        public int lengthOfFileHeader
        {
            get
            {
                return (400 + ExtendedTextHeader.Count * 3200); // 
            }
            set
            {
            }
        }

        /// <summary>
        /// read the file header from disk
        /// </summary>
        /// <param name="br">binary reader stream</param>
        /// <returns>true if successful</returns>
        public bool ReadFileHeader(BinaryReader br)
        {
            this.br = br;

            // read text header
            byte[] t0 = br.ReadBytes(3200);
            this.ExtendedTextHeader.Add(t0); // note 1st TextHeader block is alwats the firsr 3200 bytes in the file

            // read file header
            byte[] fh = br.ReadBytes(400);
            this.BinaryFileHeader = fh;

            // check byte order
            BigEndian = isBigEndian();

            positionOfStartOfDataTraces = br.BaseStream.Position; // record the positionTraceReacorded of the start of the data traces


            // check SEGY revision - SEGY Version 1.0 encoded as 0100 base 16.
            if (this.segyFormatRevisionNumber >= 256)
            {
                if ((int)this.numberOfExtendedTextualFileHeaderRecordsFollowing < 100 )
                { 
                    // may have extended headers
                    for (int i = 0; i < (int)this.numberOfExtendedTextualFileHeaderRecordsFollowing; i++)
                    {
                        t0 = br.ReadBytes(3200);
                        this.ExtendedTextHeader.Add(t0);
                    }
                }
                else
                {
                    this.detectedSEGYVer0 = true; // probably 
                }

            }
            long saveposition = br.BaseStream.Position;
            // sometimes the file is SEGY rev 0 and there is crap in the header fields
            // let's check to see is the next trace makes sense

            SEGYTraceHeader th = new SEGYTraceHeader();
            th.Initialize(this.isBigEndian());
            th.TraceHeaderBuffer = br.ReadBytes(240);
            if (th.yearDataRecorded < 1950 || th.yearDataRecorded > 2050 ) this.detectedSEGYVer0 = true;
            if (th.dayOfYear < 1  || th.dayOfYear > 366 ) this.detectedSEGYVer0 = true;
            if (th.hourOfDay < 0  || th.hourOfDay > 24 ) this.detectedSEGYVer0 = true;
            if (th.minuteOfHour< 0 || th.minuteOfHour > 60) this.detectedSEGYVer0 = true;
            if (th.secondOfMinute < 0 || th.secondOfMinute > 60 ) this.detectedSEGYVer0 = true;
            if (this.detectedSEGYVer0 )
            {
                br.BaseStream.Position = positionOfStartOfDataTraces;
                this.ExtendedTextHeader.RemoveRange(1, ExtendedTextHeader.Count - 1);
            }
            else
            {
                br.BaseStream.Position = br.BaseStream.Position - 240; // backup last header read
            }

            positionOfStartOfDataTraces = br.BaseStream.Position; // record the positionTraceReacorded of the start of the data traces

            isSEGYFileHeaderAscii = isFileHeaderASCII();

            return true;

        }

        /// <summary>
        /// write the file header to disk
        /// </summary>
        /// <param name="bw">output binary writer stream</param>
        /// <returns>true if successful</returns>
        public bool WriteFileHeader(BinaryWriter bw)
        {
            this.bw = bw;

            bw.BaseStream.Position = 0; 
            // lead text header
            bw.Write(this.ExtendedTextHeader[0]);

            bw.Write(this.BinaryFileHeader);

            if ( !this.detectedSEGYVer0 )
            { 
                for (int i = 1; i < ExtendedTextHeader.Count; i++) bw.Write(ExtendedTextHeader[i]);
            }

            positionOfStartOfDataTraces = bw.BaseStream.Position; // record the positionTraceReacorded of the start of the data traces


            return true;

        }
        /// <summary>
        /// true for big endian and false for little endian
        /// </summary>
        /// <returns>true if the file header is big endian</returns>
        public bool isBigEndian()
        {
            if (br == null ) return false;
            // check byte 25 and 26 in file header
            if (this.BinaryFileHeader[24] == 0) return true;
            return false;
        }

        /// <summary>
        /// make a deep copy of the Header
        /// </summary>
        /// <returns>a deep copy of the SEGYFileHeader structure</returns>
        public SEGYFileHeader Copy()
        {
            SEGYFileHeader newFileHeader = new SEGYFileHeader();

            // copy binary section
            byte[] newBinarySection = new byte[400];
            Array.Copy(this.BinaryFileHeader, newBinarySection, 400);

            // copy Tape headers
            List<Byte[]> newExtendedTextHeader = new List<byte[]>();
            for ( int i =0; i < this.ExtendedTextHeader.Count; i++)
            {
                byte[] newHeader = new byte[3200];
                Array.Copy(this.ExtendedTextHeader[i], newHeader, 3200);
                newExtendedTextHeader.Add(newHeader);
            }
            newFileHeader.BinaryFileHeader = newBinarySection;
            newFileHeader.ExtendedTextHeader = newExtendedTextHeader;
            newFileHeader.BigEndian = this.BigEndian;
            return newFileHeader;
            
        }

        /// <summary>
        /// is the file header encoded with ASCII or EBCDIC
        /// </summary>
        /// <returns>true if file header text is ASCII formatted</returns>
        public bool isFileHeaderASCII()
        {
            byte[] header = this.ExtendedTextHeader[0];
            bool isAscii = true;
            for ( int i = 0; i < header.Length; i++)
            {
                if ( header[i] > 127)
                {
                    isAscii = false;
                    break;
                }
            }
            return isAscii;
        }

        /// <summary>
        /// get a string for the extended tape header
        /// </summary>
        /// <param name="block">extended trace header block number</param>
        /// <returns>a 3200 character string ; null if the block number is invalid </returns>
        public string GetFileHeaderText(int block)
        {
            if ( block >= ExtendedTextHeader.Count) return null;

            byte[] buffer = (byte[]) ExtendedTextHeader[block].Clone();
                   
            if ( !isSEGYFileHeaderAscii)
            {
  
                Encoding ascii = Encoding.ASCII ;
                Encoding ebcdic = Encoding.GetEncoding("IBM037");     
                buffer = Encoding.Convert(ebcdic, ascii, buffer);

            }

            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        /// <summary>
        /// get the Text header by 80 character lines
        /// </summary>
        /// <param name="block">extended trace header block number</param>
        /// <param name="line">linb number ( 0 to 39)</param>
        /// <returns>an 80 character string ; null if the block number is invalid </returns>
        public string GetFileHeaderTextByLine(int block, int line)
        {
            if (block >= ExtendedTextHeader.Count) return null;

            byte[] buffer = new byte[80];
            Array.Copy(ExtendedTextHeader[block], line * 80, buffer, 0, 80);

            if (!isSEGYFileHeaderAscii)
            {

                Encoding ascii = Encoding.ASCII;
                Encoding ebcdic = Encoding.GetEncoding("IBM037");
                buffer = Encoding.Convert(ebcdic, ascii, buffer);

            }

            return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// set the Text Header by 80 character line
        /// </summary>
        /// <param name="block">extended block number</param>
        /// <param name="line">line number ( 0 to 39)</param>
        /// <param name="str">input string</param>
        public void SetFileHeader(int block, int line, string str)
        {


            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str.ToCharArray()) ;

            if (!isSEGYFileHeaderAscii)
            {

                Encoding utf8 = Encoding.UTF8;
                Encoding ebcdic = Encoding.GetEncoding("IBM037");
                buffer = Encoding.Convert(utf8, ebcdic, buffer);

            }

            Array.Copy(buffer, 80, ExtendedTextHeader[block], line * 80, 80);
        }
    }
}
