using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SEGYlib
{
    /// <summary>
    /// SEGYTraceHeader is used to access and change contents of the binary trace header data block
    /// </summary>
    public class SEGYTraceHeader
    {

        private byte[] iTraceHeaderBuffer;
        private bool isInitialized;
        private bool isBigEndian;
        /// <summary>
        /// SEGYTraceHeader is used to access and change contents of the binary trace header data block
        /// </summary>
        public SEGYTraceHeader()
        {
            isInitialized = false;
            isBigEndian = true;
        }

        /// <summary>
        /// initialize object
        /// </summary>
        public void Initialize(bool bigEndian)
        {
            isInitialized = true;
            isBigEndian = bigEndian;
        }
        /// <summary>
        /// SEGYTraceHeader storage block
        /// </summary>
        [XmlIgnore]
        public byte[] TraceHeaderBuffer
        {
            get
            {
                return iTraceHeaderBuffer;
            }
            set
            {
                iTraceHeaderBuffer = value;
                if(iTraceHeaderBuffer.Length == 240) isInitialized = true; // properly initialized
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public UInt32 traceSequenceNumberWithinLine
        {
            get
            {
                return (UInt32)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 0, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 0, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public UInt32 traceSequenceNumberWithinFile
        {
            get
            {
                return (UInt32)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 4, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 4, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint originalFieldRecordNumber
        {
            get
            {
                return (UInt32)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 8, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 8, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint traceNumberWithinOriginalFieldRecord
        {
            get
            {
                return (UInt32)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 12, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 12, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint energySourcePointNumber
        {
            get
            {
                return (UInt32)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 16, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 16, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint ensembleNumber
        {
            get
            {
                return (UInt32)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 20, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 20, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint traceNumberWithinEnsemble
        {
            get
            {
                return (UInt32)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 24, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 24, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public Int16 traceIdentificationCode
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 28, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 28, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort numberOfVerticallySummedTracesYieldingThisTrace
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 30, 2, false ,isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 30, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort numberOfHorizonatallySummedTracesYieldingThisTrace
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 32, 2, false ,isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 32, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort dataUse
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 34, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 34, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int distanceFromCenterOfSourcePointToCenterOfGroup
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 36, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 36, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int receiverGroupElevation
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 40, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 40, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int  surfaceElevationAtSource
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 44, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 44, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int sourceDepthBelowSurface
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 48, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 48, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int datumElevationAtReceiverGroup
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 52, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 52, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int datumElevationAtSource
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 56, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 56, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int waterDepthAtSource
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 60, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 60, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int waterDepthAtGroup
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 64, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 64, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short scalarForAllElevationsAndDepths
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 68,2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 68, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short scalarToBeAppliedToAllCoordinates
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 70, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 70, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int sourceCoordinateX
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 72, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 72, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int sourceCoordinateY
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 76, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 76, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int groupCoordinateX
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 80, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 80, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int groupCoordinateY
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 84, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 84, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort coordinateUnits
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 88,2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 88, 2, isBigEndian);

            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort weatheringVelocity
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 90, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 90, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort subweatheringVelocity
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 92, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 92, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort upholeTimeAtSourceMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 94, 2, false, isBigEndian);
            }
            set
            {
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort upholeTimeAtGroupMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 96, 2, false, isBigEndian);
            }
            set
            {
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort souceStaticCorrectionMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 98, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 98, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort groupStaticCorrectionMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 100, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 100, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short totalStaticMsec
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 102, 2, true, isBigEndian);
            }
            set
            {
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short lagTimeAMsec
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 104, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 104, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short lagTimeBMsec
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 106, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 106, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short delayRecordingTimeMsec
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 108, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 108, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort muteTimeStartTimeMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 110, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 110, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort muteTimeEndTimeMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 112, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 112, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort numberOfSamplesInTrace
        {
            get
            {
                return (ushort) SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 114, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 114, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort sampleIntervalUsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 116, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 116, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort gainTypeOfFieldInstruments
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 118, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 118, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short instrumentGainConstantDB
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 120, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 120, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short instrumentEarlyOrIntialGainDB
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 122, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 122, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort correlated
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 124, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 124, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort sweepFrequencyAtStart
        {
            get
            {
               return  (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 126 , 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 126, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort sweepFrequencyAtEnd
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 128, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 128, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort sweepLengthInMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 130, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 130, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort sweepType
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 132, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 132, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort sweepTaperLengthAtStartMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 134, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 134, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort sweepTaperLenghtAtEndMsec
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 136, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 136, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort taperType
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 138, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 138, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort aliasFrequencyHz
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 140, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 140, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short aliasFilterSlopeDBOctave
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 142, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 142, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort notchFrequencyHz
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 144, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 144, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short notchFilterSlopeDBOctave
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 146, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 146, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort lowCutFrequencyHz
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 148, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 148, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort highCutFrequencyHz
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 150, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 150, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short lowCutSlopeDBOctave
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 152, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 152, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short highCutSlopeDBOctave
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 154, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 154, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort yearDataRecorded
        {
            get
            {
                return (ushort) SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 156, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.TraceHeaderBuffer, 156, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort dayOfYear
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 158, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.TraceHeaderBuffer, 158, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort hourOfDay
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 160, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.TraceHeaderBuffer, 160, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort minuteOfHour
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 162, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.TraceHeaderBuffer, 162, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort secondOfMinute
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 164, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.TraceHeaderBuffer, 164, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort timeBasis
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 166, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.TraceHeaderBuffer, 166, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort traceWeightingFactor
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 168, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 168, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort geophoneGroupNumberOfRollSwitchPositionOne
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 170, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 170, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort geophoneGroupNumberofTraceNumberOneWithinOriginalFieldRecord
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 172, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 172, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort geophoneGroupNumberofLastTraceWithinOriginalFieldRecord
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 174, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 174, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort gapSize
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 176 ,2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 176, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public ushort overTravel
        {
            get
            {
                return (ushort)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 178, 2, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 178, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int xCoordinateOfEnsemble
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 180, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 180,4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int yCoordinateOfEnsemble
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 184, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 184, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint inLineNumber3D
        {
            get
            {
                return (uint)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 188, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 188, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint crossLineNumber3D
        {
            get
            {
                return (uint)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 192, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 192, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public uint shotpointNumber
        {
            get
            {
                return (uint)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 196, 4, false, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, false, this.iTraceHeaderBuffer, 196, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short scalarAppliedToShotPointNumber
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 200, 2, true, isBigEndian);
            }
            set
            {
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short traceValueMeasurementUnit
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 202, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 202, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int transductionConstantMantissa
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 204, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 204, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short transductionConstantExponent
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 208, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 208, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short transductionUnits
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 210, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 210, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short deviceTraceIdentifier
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 212, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 212, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short scalarUsedToScaleTraceHeaderMSecTimes
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 214, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer,214, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short sourceType
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 216, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 216, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int sourceEnergyDirectionMantissa
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 218, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 218,4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short sourceEnergyDirectionExponent
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 222, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 222, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public int sourceMeasurementMantissa
        {
            get
            {
                return (int)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 224, 4, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 224, 4, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short sourceMeasurementExponent
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 228, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 228, 2, isBigEndian);
            }
        }

        /// <summary>
        /// refer to SEGY rev 1 documentation
        /// </summary>
        public short sourceMeasurementUnit
        {
            get
            {
                return (short)SEGYUtilities.Bytes2Int(this.iTraceHeaderBuffer, 230, 2, true, isBigEndian);
            }
            set
            {
                SEGYUtilities.Int2Bytes((long)value, true, this.iTraceHeaderBuffer, 230, 2, isBigEndian);
            }
        }

        /// <summary>
        /// true if big endian
        /// </summary>
        public bool bigEndian
        {
            get
            {
                return isBigEndian;
            }
            set
            {
            }
        }
    }
}
