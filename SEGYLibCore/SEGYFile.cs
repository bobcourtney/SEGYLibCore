using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGYlib
{
    /// <summary>
    /// Top level class for reading and writing SEGY files
    /// </summary>
    [Serializable()]


    public class SEGYFile
    {
        /// <summary>
        /// SEGYFile is the top level object for reading and writing SEGY rev 1 files
        /// </summary>
        private string? Filename;
        /// <summary>
        /// true for big endian file; false little endian
        /// </summary>
        public bool isBigEndian;
        [NonSerialized()] private System.IO.BinaryReader? SEGYreader;
        [NonSerialized()] private System.IO.BinaryWriter? SEGYwriter;


        private SEGYFileHeader? iSEGYFileHeader;
        private List<SEGYTrace>? iListSegyTrace;
        private SEGYTrace? iSEGYTrace;
        private long positionOfEndofFileHeaders;

        /// <summary>
        /// access to File Header Class
        /// </summary>
        public SEGYFileHeader? FileHeader
        {
            get
            {
                return iSEGYFileHeader;
            }
            set
            {
                iSEGYFileHeader = value;
            }
        }

        /// <summary>
        /// List of traces including data and trace headers
        /// </summary>
        public List<SEGYTrace>? Traces
        {
            get
            {
                return iListSegyTrace;
            }
            set
            {
                iListSegyTrace = value;
            }
        }

        /// <summary>
        /// number of traces in Trace list
        /// </summary>
        public int NumberOfTracesInBuffer
        {
            get
            {
                if (this.iListSegyTrace != null)
                {
                    return this.iListSegyTrace.Count;
                } else
                {
                    return 0;
                }
            }
            set
            {
            }
        }

        /// <summary>
        /// last trace read from file
        /// </summary>
        public SEGYTrace? currentTrace
        {
            get
            {
                return iSEGYTrace;
            }
            set
            {
                iSEGYTrace = value;
            }
        }

        /// <summary>
        /// open or create a SEGY file
        /// returns 0 if unsuccessful; 1 if non zero length file ; 2 is empty file
        /// </summary>
        /// <param name="inputFilename">SEGY file name</param>
        /// <returns>0 - not successful; 1 - opened an existing file; 2 - created a new file</returns>
        public int Open(string inputFilename)
        {
            try
            {
                Filename = inputFilename;
                this.SEGYreader = new System.IO.BinaryReader(System.IO.File.Open(inputFilename, System.IO.FileMode.OpenOrCreate));
                this.SEGYwriter = new System.IO.BinaryWriter(this.SEGYreader.BaseStream);

                if ( this.SEGYreader.BaseStream.Length <= 3600 )
                {
                    // this in a empty file
                    return 2;
                } 

                if ( this.FileHeader == null)
                { 
                    this.FileHeader = new SEGYFileHeader();
                    this.FileHeader.ReadFileHeader(this.SEGYreader);
                }
                this.isBigEndian = this.FileHeader.isBigEndian();
                this.positionOfEndofFileHeaders = this.SEGYreader.BaseStream.Position;
            }

            catch ( SystemException e)
            {
                return 0;
            }
            // read file header

            return 1; ;
        }

        /// <summary>
        /// move file pointer to the end of the file header blocks
        /// </summary>
        public void MoveFilePointerToStartOfTraces()
        {
            if (SEGYreader != null )SEGYreader.BaseStream.Position = this.positionOfEndofFileHeaders;
            if (SEGYwriter != null) SEGYwriter.BaseStream.Position = this.positionOfEndofFileHeaders;
        }


        /// <summary>
        /// test to see if input file is a SEGY file
        /// </summary>
        /// <returns>true is the input file has acceptable FileHeader.dataSampleFormatCode and FileHeader.dataSampleFormatCode </returns>
        public bool isSEGY()
        {
            if (this.FileHeader == null) return false;
            if (this.FileHeader.dataSampleFormatCode < 1 || this.FileHeader.dataSampleFormatCode > 8) return false;
            return true;
        }

        /// <summary>
        /// read the file headers
        /// </summary>
        /// <returns>true is successful</returns>
        public bool ReadFileHeader()
        {
            if (SEGYreader == null || this.FileHeader == null)  return false;
            try
            { 
                SEGYreader.BaseStream.Position = 0;
                this.FileHeader.ReadFileHeader(this.SEGYreader);
                return true;
            }

            catch (SystemException e)
            {
                System.Windows.MessageBox.Show("Error = " + e.Message);
                return false;
            }
        }


        /// <summary>
        /// position the stream reader/writer at the start of the n'th trace
        /// </summary>
        /// <param name="n">trace id</param>
        /// <returns>true is successful</returns>
        public bool GoToStartOfTrace( int n)
        {
            if (this.iListSegyTrace.Count < n) return false;
            long offset = this.iListSegyTrace[n].positionOfTraceInFile;
            SEGYreader.BaseStream.Position = offset;
            return true;
        }

        /// <summary>
        /// read the next trace in the file
        /// </summary>        
        /// <returns>true is successful</returns>
        public bool ReadNextTrace()
        {
            if (this.iSEGYFileHeader == null) return false;

            this.iSEGYTrace = new SEGYTrace();
            this.iSEGYTrace.Intialize(this.isBigEndian, this.iSEGYFileHeader.dataSampleFormatCode);

            long currentFilePosition = this.SEGYreader.BaseStream.Position;
            this.iSEGYTrace.positionOfTraceInFile = currentFilePosition;

            // read trace header 
            byte[] tmpTraceHeader = this.SEGYreader.ReadBytes(240);
            if ( tmpTraceHeader.Length < 240 )
            {
                this.SEGYreader.BaseStream.Position = currentFilePosition;
                return false;

            }

            this.iSEGYTrace.TraceHeader.TraceHeaderBuffer = tmpTraceHeader; // store byte stream of trace header


            // read trace data
            int bytesToRead = this.iSEGYTrace.TraceHeader.numberOfSamplesInTrace;
            if (bytesToRead <= 0 )
            {
                // use fileheader value
                bytesToRead = iSEGYFileHeader.numberOfSamplesPerDataTrace;
            }
            if ( this.iSEGYFileHeader.dataSampleFormatCode == 1 || this.iSEGYFileHeader.dataSampleFormatCode == 2  || this.iSEGYFileHeader.dataSampleFormatCode == 5 )
            {
                bytesToRead *= 4;
            }
            else if (this.iSEGYFileHeader.dataSampleFormatCode == 3)
            {
                bytesToRead *= 2;
            }
            else if (this.iSEGYFileHeader.dataSampleFormatCode == 8)
            {
                bytesToRead *= 1;
            }
            else
            {
                System.Windows.MessageBox.Show("this.iSEGYFileHeader.dataSampleFormatCode = " + this.iSEGYFileHeader.dataSampleFormatCode.ToString() + " is not supported");
                return false;
            }
            byte[] tmpTraceData = this.SEGYreader.ReadBytes(bytesToRead);
            if (tmpTraceData.Length < bytesToRead)
            {
                this.SEGYreader.BaseStream.Position = currentFilePosition;
                return false;

            }
            this.iSEGYTrace.TraceData.TraceDataBuffer = tmpTraceData;
           

            return true;

        }

        /// <summary>
        /// read all trace headers including trace data
        /// </summary>
        public void ReadAllTraces()
        {
            MoveFilePointerToStartOfTraces();
            iListSegyTrace = new List<SEGYTrace>();
            while(ReadNextTrace())
            {
                iListSegyTrace.Add(iSEGYTrace);
            }
        }
        /// <summary>
        /// read all trace headers but don't load trace data
        /// </summary>
        public void ReadAllTraceHeaders()
        {
            MoveFilePointerToStartOfTraces();
            iListSegyTrace = new List<SEGYTrace>();
            while (ReadNextTrace())
            {
                iSEGYTrace.TraceData.TraceDataBuffer = null; // dump the trace data
                iListSegyTrace.Add(iSEGYTrace);
            }
        }
        /// <summary>
        /// read the next n traces in the file
        /// </summary>
        /// <param name="n">number of traces to read</param>
        /// <returns>true is successful</returns>
        public bool ReadNTraces(int n)
        {
            iListSegyTrace = new List<SEGYTrace>();
            for ( int i = 0; i < n; i++)
            {
                if (!ReadNextTrace()) return false;
                iListSegyTrace.Add(iSEGYTrace);
            }
            return true;

        }

        /// <summary>
        /// close I/O channels
        /// </summary>
        public void Close()
        {
            if (SEGYreader != null) SEGYreader.Close();
            if ( SEGYwriter != null)
            {
                SEGYwriter.Close();
            }
        }

        /// <summary>
        /// skip ntraces
        /// </summary>
        /// <param name="skip">number of traces to skip</param>        
        /// <returns>true is successful</returns>
        public bool SkipNTracesOnRead(int skip)
        {
           for ( int i = 0; i < skip; i++)
           {
               if( !ReadNextTrace() ) return false;
           }
            return true;
        }

        /// <summary>
        /// add a trace to the end of the Traces list
        /// </summary>
        /// <param name="trace">add a trace</param>
        public void AddTrace(SEGYTrace trace)
        {
            if ( Traces.Count == 0 )
            {
                trace.positionOfTraceInFile = positionOfEndofFileHeaders;
             } else {
                trace.positionOfTraceInFile = Traces[Traces.Count - 1].positionOfTraceInFile + Traces[Traces.Count - 1].totalLengthOfTraceData;
             }
            Traces.Add(trace);
        }

        /// <summary>
        /// re-read the file and reindex the trace locations
        /// </summary>
        public void ReindexTracePositions()
        {
            for ( int i = 0; i < Traces.Count; i++)
            {
                SEGYTrace trace = Traces[i];
                if (i == 0)
                {
                    trace.positionOfTraceInFile = positionOfEndofFileHeaders;
                }
                else
                {
                    trace.positionOfTraceInFile = Traces[i - 1].positionOfTraceInFile + Traces[i - 1].totalLengthOfTraceData;
                }
            }
        }


        /// <summary>
        /// remove trace i from the Traces list
        /// </summary>
        /// <param name="i">trace number to remove from list</param>
        /// <returns>true is successful</returns>
        public bool RemoveTrace(int i)
        {
            if (i >= Traces.Count) return false;
            Traces.RemoveAt(i);
            return true;

        }
        /// <summary>
        /// delete all trace storage
        /// </summary>    
        /// <returns>true is successful</returns>
        public bool RemoveAllTraces()
        {
            Traces.Clear();
            return true;

        }
        /// <summary>
        /// write the entire file to disk
        /// </summary>
        /// <param name="outputFileName">output file name</param>        
        /// <returns>true is successful</returns>
        public bool Write(string outputFileName)
        {
            if ( this.SEGYwriter == null ) {Open(outputFileName);}
            else if ( String.Compare(outputFileName,this.Filename) != 0 )
            {
                this.Close();
                this.Open(outputFileName);
            }

            Write(FileHeader);
            Write(Traces);

            return true;
        }

        /// <summary>
        /// write the file header  to disk
        /// </summary>
        /// <param name="fileHeader">input file header</param>        
        /// <returns>true is successful</returns>
        public bool Write(SEGYFileHeader fileHeader)
        {
            if (this.SEGYwriter == null) return false; // no output stream defined
            this.SEGYwriter.BaseStream.Position = 0;

            bool ret = fileHeader.WriteFileHeader(this.SEGYwriter);
            SEGYreader.BaseStream.Position = SEGYwriter.BaseStream.Position;

            positionOfEndofFileHeaders = SEGYwriter.BaseStream.Position;

            return ret;
        }

        /// <summary>
        /// write a trace to disk
        /// </summary>
        /// <param name="trace">input trace header</param>        
        /// <returns>true is successful</returns>
        public bool Write(SEGYTrace trace)
        {
            if (positionOfEndofFileHeaders < 3600) return false; // no file header written yet

            return trace.Write(this.SEGYwriter);

        }

        /// <summary>
        /// write the list Traces to disk
        /// </summary>
        /// <param name="traces">List of SEGYTrace instances</param>        
        /// <returns>true is successful</returns>
        public bool Write(List<SEGYTrace> traces)
        {
            for ( int i =0; i < traces.Count; i++)
            {
                if( !traces[i].Write(SEGYwriter))
                {
                    SEGYwriter.BaseStream.Position = positionOfEndofFileHeaders;
                    return false;
                }
            }

            SEGYreader.BaseStream.Position = SEGYwriter.BaseStream.Position;

            return true;
        }

        /// <summary>
        /// make a deep copy of the Traces List
        /// </summary>        
        /// <returns>pointer to new List of SEGYTraces</returns>
        public List<SEGYTrace> CopyAllTraces()
        {
            List<SEGYTrace> newList = new List<SEGYTrace>();
            SEGYTrace newTrace;
            for ( int i =0; i < this.Traces.Count; i++)
            {
                newTrace = Traces[i].Copy();
                newList.Add(newTrace);
            }
            return newList;
        }

        /// <summary>
        /// write the file to XML
        /// </summary>
        /// <param name="outputXMLFileName">output XML file name</param>        
        /// <returns>true is successful</returns>
        public bool WriteXML(string outputXMLFileName)
        {

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(this.GetType());
            System.IO.FileStream file = System.IO.File.Create(outputXMLFileName);
            writer.Serialize(file, this);
            file.Close();
            return true;
        }
        /// <summary>
        /// write the file header to XML
        /// </summary>
        /// <param name="outputXMLFileName">output XML file name</param>
        /// <param name="fileheader">input file header</param>        
        /// <returns>true is successful</returns>
        public bool WriteXML(string outputXMLFileName, SEGYFileHeader fileheader)
        {

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(fileheader.GetType());
            System.IO.FileStream file = System.IO.File.Create(outputXMLFileName);
            writer.Serialize(file, fileheader);
            file.Close();
            return true;
        }
        /// <summary>
        /// write the trace to XML
        /// </summary>
        /// <param name="outputXMLFileName">output XML file name</param>
        /// <param name="trace">input trace</param>        
        /// <returns>true is successful</returns>
        public bool WriteXML(string outputXMLFileName, SEGYTrace trace)
        {

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(trace.GetType());
            System.IO.FileStream file = System.IO.File.Create(outputXMLFileName);

            writer.Serialize(file, trace);
            file.Close();
            return true;
        }
        /// <summary>
        /// read an SEGY file in XML format
        /// </summary>
        /// <param name="inputXMLFileName">input SEGYFile XML file name</param>
        /// <returns>pointer to SEGYFile</returns>
        public static SEGYFile ReadXML(string inputXMLFileName)
        {
            SEGYFile overview = new SEGYFile();
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(overview.GetType());
            System.IO.StreamReader file = new System.IO.StreamReader(
                inputXMLFileName);
            overview = (SEGYFile)reader.Deserialize(file);
            file.Close();
            return overview;
        }
        /// <summary>
        /// read an SEGY file header in XML format
        /// </summary>
        /// <param name="inputXMLFileName">input SEGYFileHeader XML file name</param>
        /// <returns>point to SEGYFileHeader</returns>
        public static SEGYFileHeader ReadXMLFileHeader(string inputXMLFileName)
        {
            SEGYFileHeader overview = new SEGYFileHeader();
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(overview.GetType());
            System.IO.StreamReader file = new System.IO.StreamReader(
                inputXMLFileName);
            overview = (SEGYFileHeader)reader.Deserialize(file);
            file.Close();
            return overview;
        }
        /// <summary>
        /// read an SEGY trace in XML format
        /// </summary>
        /// <param name="inputXMLFileName">input SEGYTrace XML file name</param>
        /// <returns>pointer to SEGYTrace</returns>
        public static SEGYTrace ReadXMLTrace(string inputXMLFileName)
        {
            SEGYTrace overview = new SEGYTrace();
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(overview.GetType());
            System.IO.StreamReader file = new System.IO.StreamReader(
                inputXMLFileName);
            overview = (SEGYTrace)reader.Deserialize(file);
            file.Close();
            return overview;
        }

        /// <summary>
        /// move to a random position in the filestream
        /// </summary>
        /// <param name="pos">desired psotion in file stream</param>
        /// <returns>pointer to SEGYTrace</returns>
        public void MoveToStreamPosition(long pos)
        {
            if (pos < 0 || pos >= this.SEGYreader.BaseStream.Length) return;
            this.SEGYreader.BaseStream.Position = pos;
        }
  
    }

}
