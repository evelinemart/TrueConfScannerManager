using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TrueConfScannerManager
{
    [Flags]
    internal enum TwDG : uint
    {                                   // DG_.....

        /// <summary>
        /// Data pertaining to control.
        /// </summary>
        Control = 0x0001,

        /// <summary>
        /// Data pertaining to raster images.
        /// </summary>
        Image = 0x0002,

        /// <summary>
        /// Data pertaining to audio.
        /// </summary>
        Audio = 0x0004,

        /// <summary>
        /// added to the identity by the DSM.
        /// </summary>
        DSM2 = 0x10000000,

        /// <summary>
        /// Set by the App to indicate it would prefer to use DSM2.
        /// </summary>
        APP2 = 0x20000000,

        /// <summary>
        /// Set by the DS to indicate it would prefer to use DSM2.
        /// </summary>
        DS2 = 0x40000000
    }

    internal enum TwDAT : ushort
    {                                   // DAT_....

        #region Data Argument Types for the DG_CONTROL Data Group.

        Null = 0x0000,
        Capability = 0x0001,
        Event = 0x0002,
        Identity = 0x0003,
        Parent = 0x0004,
        PendingXfers = 0x0005,
        SetupMemXfer = 0x0006,
        SetupFileXfer = 0x0007,
        Status = 0x0008,
        UserInterface = 0x0009,
        XferGroup = 0x000a,
        TwunkIdentity = 0x000b,
        CustomDSData = 0x000c,
        DeviceEvent = 0x000d,
        FileSystem = 0x000e,
        PassThru = 0x000f,
        Callback = 0x0010,   /* TW_CALLBACK        Added 2.0         */
        StatusUtf8 = 0x0011, /* TW_STATUSUTF8      Added 2.1         */
        Callback2 = 0x0012,

        #endregion

        #region Data Argument Types for the DG_IMAGE Data Group.

        ImageInfo = 0x0101,
        ImageLayout = 0x0102,
        ImageMemXfer = 0x0103,
        ImageNativeXfer = 0x0104,
        ImageFileXfer = 0x0105,
        CieColor = 0x0106,
        GrayResponse = 0x0107,
        RGBResponse = 0x0108,
        JpegCompression = 0x0109,
        Palette8 = 0x010a,
        ExtImageInfo = 0x010b,

        #endregion

        #region misplaced

        IccProfile = 0x0401,       /* TW_MEMORY        Added 1.91  This Data Argument is misplaced but belongs to the DG_IMAGE Data Group */
        ImageMemFileXfer = 0x0402, /* TW_IMAGEMEMXFER  Added 1.91  This Data Argument is misplaced but belongs to the DG_IMAGE Data Group */
        EntryPoint = 0x0403,       /* TW_ENTRYPOINT    Added 2.0   This Data Argument is misplaced but belongs to the DG_CONTROL Data Group */

        #endregion
    }

    internal enum TwMSG : ushort
    {                                   // MSG_.....

        #region Generic messages may be used with any of several DATs.

        /// <summary>
        /// Used in TW_EVENT structure.
        /// </summary>
        Null = 0x0000,

        /// <summary>
        /// Get one or more values.
        /// </summary>
        Get = 0x0001,

        /// <summary>
        /// Get current value.
        /// </summary>
        GetCurrent = 0x0002,

        /// <summary>
        /// Get default (e.g. power up) value.
        /// </summary>
        GetDefault = 0x0003,

        /// <summary>
        /// Get first of a series of items, e.g. DSs.
        /// </summary>
        GetFirst = 0x0004,

        /// <summary>
        /// Iterate through a series of items.
        /// </summary>
        GetNext = 0x0005,

        /// <summary>
        /// Set one or more values.
        /// </summary>
        Set = 0x0006,

        /// <summary>
        /// Set current value to default value.
        /// </summary>
        Reset = 0x0007,

        /// <summary>
        /// Get supported operations on the cap.
        /// </summary>
        QuerySupport = 0x0008,

        GetHelp = 0x0009,
        GetLabel = 0x000a,
        GetLabelEnum = 0x000b,
        SetConstraint = 0x000c,

        #endregion

        #region Messages used with DAT_NULL

        XFerReady = 0x0101,
        CloseDSReq = 0x0102,
        CloseDSOK = 0x0103,
        DeviceEvent = 0x0104,

        #endregion

        #region Messages used with a pointer to a DAT_STATUS structure

        /// <summary>
        /// Get status information
        /// </summary>
        CheckStatus = 0x0201,

        #endregion

        #region Messages used with a pointer to DAT_PARENT data

        /// <summary>
        /// Open the DSM
        /// </summary>
        OpenDSM = 0x0301,

        /// <summary>
        /// Close the DSM
        /// </summary>
        CloseDSM = 0x0302,

        #endregion

        #region Messages used with a pointer to a DAT_IDENTITY structure

        /// <summary>
        /// Open a data source
        /// </summary>
        OpenDS = 0x0401,

        /// <summary>
        /// Close a data source
        /// </summary>
        CloseDS = 0x0402,

        /// <summary>
        /// Put up a dialog of all DS
        /// </summary>
        UserSelect = 0x0403,

        #endregion

        #region Messages used with a pointer to a DAT_USERINTERFACE structure

        /// <summary>
        /// Disable data transfer in the DS
        /// </summary>
        DisableDS = 0x0501,

        /// <summary>
        /// Enable data transfer in the DS
        /// </summary>
        EnableDS = 0x0502,

        /// <summary>
        /// Enable for saving DS state only.
        /// </summary>
        EnableDSUIOnly = 0x0503,

        #endregion

        #region Messages used with a pointer to a DAT_EVENT structure

        ProcessEvent = 0x0601,

        #endregion

        #region Messages used with a pointer to a DAT_PENDINGXFERS structure

        EndXfer = 0x0701,
        StopFeeder = 0x0702,

        #endregion

        #region Messages used with a pointer to a DAT_FILESYSTEM structure

        ChangeDirectory = 0x0801,
        CreateDirectory = 0x0802,
        Delete = 0x0803,
        FormatMedia = 0x0804,
        GetClose = 0x0805,
        GetFirstFile = 0x0806,
        GetInfo = 0x0807,
        GetNextFile = 0x0808,
        Rename = 0x0809,
        Copy = 0x080A,
        AutoCaptureDir = 0x080B,

        #endregion

        #region Messages used with a pointer to a DAT_PASSTHRU structure

        PassThru = 0x0901,

        #endregion

        #region used with DAT_CALLBACK

        RegisterCallback = 0x0902,

        #endregion

        #region used with DAT_CAPABILITY

        ResetAll = 0x0A01

        #endregion
    }

    internal enum TwRC : ushort
    {                                   // TWRC_....

        /// <summary>
        /// Operation was successful.
        /// </summary>
        Success = 0x0000,

        /// <summary>
        /// May be returned by any operation. An error has occurred.
        /// </summary>
        Failure = 0x0001,

        /// <summary>
        /// Intended for use with DAT_CAPABILITY and DAT_IMAGELAYOUT. 
        /// Operation failed to completely perform
        /// the desired operation. For example, setting ICAP_BRIGHTNESS to
        /// 3 when its range is -1000 to 1000 with a step of 200. The data source
        /// may opt to set the value to 0 and return this status.
        /// </summary>
        CheckStatus = 0x0002,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations. Operation has been canceled.
        /// </summary>
        Cancel = 0x0003,

        /// <summary>
        /// Intended for use with DAT_EVENT. The data source processed the event.
        /// </summary>
        DSEvent = 0x0004,

        /// <summary>
        /// Intended for use with DAT_EVENT. The data source did not process the event.
        /// </summary>
        NotDSEvent = 0x0005,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations. The image has been fully transferred.
        /// </summary>
        XferDone = 0x0006,

        /// <summary>
        /// Intended for use with DAT_IDENTITY and DAT_FILESYSTEM.
        /// </summary>
        EndOfList = 0x0007,

        /// <summary>
        /// Intended for use with DAT_EXTIMAGEINFO. 
        /// The requested TWEI_ data is either not supported by this data source, or is not supported for this particular image.
        /// </summary>
        InfoNotSupported = 0x0008,

        /// <summary>
        /// Intended for use with DAT_EXTIMAGEINFO. There is no data available for the requested TWEI_ item.
        /// </summary>
        DataNotAvailable = 0x0009,

        /// <summary>
        /// The busy.
        /// </summary>
        Busy = 10,

        /// <summary>
        /// The scanner locked.
        /// </summary>
        ScannerGlobalLocked = 11
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct WINMSG
    {
        internal IntPtr hwnd;
        internal int message;
        internal IntPtr wParam;
        internal IntPtr lParam;
    }

    internal enum TwCC : ushort
    {                                   // TWCC_....

        /// <summary>
        /// Operation was successful. This value should only be paired with TWRC_SUCCESS.
        /// </summary>
        Success = 0x0000,

        /// <summary>
        /// May be returned by any operation. The data source is in a critical state.
        /// </summary>
        Bummer = 0x0001,

        /// <summary>
        /// May be returned for any operation except ones that reduce state
        /// (DAT_PENDINGXFERS / MSG_ENDXER, DAT_PENDINGXFERS / MSG_RESET, 
        /// DAT_USERINTERFACE / MSG_DISABLEDS, DAT_IDENTITY / MSG_CLOSEDS, 
        /// DAT_PARENT / MSG_CLOSEDSM).
        /// </summary>
        LowMemory = 0x0002,

        /// <summary>
        /// Intended for use with DAT_IDENTITY / MSG_OPENDS. The device is not online.
        /// </summary>
        NoDS = 0x0003,

        /// <summary>
        /// Intended for use with DAT_IDENTITY / MSG_OPENDS. The data
        /// source cannot support any more connections to this device.
        /// </summary>
        MaxConnections = 0x0004,

        /// <summary>
        /// The operation failed, but the user has already been informed by the data source.
        /// </summary>
        OperationError = 0x0005,

        /// <summary>
        /// Intended for use with DAT_CAPABILITY. Returned by pre-1.7
        /// data sources to indicate that the capability is not supported, that the
        /// value was bad, or that the desired value could not be set at this time.
        /// </summary>
        BadCap = 0x0006,

        /// <summary>
        /// May be returned by any operation. The requested 
        /// DG_* / DAT_* / MSG_* is not supported by the data source.
        /// </summary>
        BadProtocol = 0x0009,

        /// <summary>
        /// May be returned by any operation. The capability or operation has
        /// rejected the requested setting.
        /// </summary>
        BadValue = 0x000a,

        /// <summary>
        /// The seq error.
        /// </summary>
        SeqError = 0x000b,

        /// <summary>
        /// May be returned by any operation (save for the DAT_PARENT
        /// operations). The TW_IDENTITY for the destination (the data
        /// source) does not match any items opened by MSG_OPENDS.
        /// </summary>
        BadDest = 0x000c,

        /// <summary>
        /// Intended for use with DAT_CAPABILITY. The capability is not supported.
        /// </summary>
        CapUnsupported = 0x000d,

        /// <summary>
        /// Intended for use with DAT_CAPABILITY. The capability does not support the requested operation.
        /// </summary>
        CapBadOperation = 0x000e,

        /// <summary>
        /// Intended for use with DAT_CAPABILITY. The capability being
        /// MSG_SET or MSG_RESET cannot be modified due to a setting for a
        /// related capability. For instance, this may be returned by
        /// ICAP_CITTKFACTOR if ICAP_COMPRESSION is set to any value
        /// other than TWCP_GROUP32D.
        /// </summary>
        CapSeqError = 0x000f,

        /// <summary>
        /// Intended for DAT_IMAGEFILEXFER and DAT_FILESYSTEM, the
        /// specified file or directory cannot be modified or deleted.
        /// </summary>
        Denied = 0x0010,

        /// <summary>
        /// Intended for DAT_FILESYSTEM. The specified file or directory already exists.
        /// </summary>
        FileExists = 0x0011,

        /// <summary>
        /// Intended for DAT_IMAGEFILEXFER and DAT_FILESYSTEM. The
        /// specified file or directory cannot be found.
        /// </summary>
        FileNotFound = 0x0012,

        /// <summary>
        /// Intended for use with DAT_FILESYSTEM. Directory is in use, and cannot be deleted.
        /// </summary>
        NotEmpty = 0x0013,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        PaperJam = 0x0014,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        PaperDoubleFeed = 0x0015,

        /// <summary>
        /// Intended for DAT_IMAGEFILEXFER and DAT_FILESYSTEM, the
        /// specified file or directory could not be written, usually indicating a
        /// disk full condition, though it may also indicate a file or directory
        /// that the user has no permission to write.
        /// </summary>
        FileWriteError = 0x0016,

        /// <summary>
        /// May be returned for any operation in state 4 or higher, except ones
        /// that reduce state (DAT_PENDINGXFERS / MSG_ENDXER,
        /// DAT_PENDINGXFERS / MSG_RESET, DAT_USERINTERFACE / MSG_DISABLEDS, 
        /// DAT_IDENTITY / MSG_CLOSEDS, DAT_PARENT / MSG_CLOSEDSM).
        /// </summary>
        CheckDeviceOnline = 0x0017,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        InterGlobalLock = 24,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        DamagedCorner = 25,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        FocusError = 26,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        DocTooLight = 27,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        DocTooDark = 28,

        /// <summary>
        /// Intended for use with the DAT_IMAGE*XFER operations.
        /// </summary>
        NoMedia = 29,
    }

    internal enum TwOn : ushort
    {                                   // TWON_....

        /// <summary>
        /// Indicates TW_ARRAY container
        /// </summary>
        Array = 0x0003,

        /// <summary>
        /// Indicates TW_ENUMERATION container
        /// </summary>
        Enum = 0x0004,

        /// <summary>
        /// Indicates TW_ONEVALUE container
        /// </summary>
        One = 0x0005,

        /// <summary>
        /// Indicates TW_RANGE container
        /// </summary>
        Range = 0x0006,
        DontCare = 0xffff
    }

    internal enum TwType : ushort
    {                                   // TWTY_....
        Int8 = 0x0000,
        Int16 = 0x0001,
        Int32 = 0x0002,
        UInt8 = 0x0003,
        UInt16 = 0x0004,
        UInt32 = 0x0005,
        Bool = 0x0006,
        Fix32 = 0x0007,
        Frame = 0x0008,
        Str32 = 0x0009,
        Str64 = 0x000a,
        Str128 = 0x000b,
        Str255 = 0x000c,
        Str1024 = 0x000d,
        Uni512 = 0x000e,
        Handle = 0x000f
    }

    internal sealed class TwTypeHelper
    {
        private static Dictionary<TwType, Type> _typeof = new Dictionary<TwType, Type> {
                {TwType.Int8,typeof(sbyte)},
                {TwType.Int16,typeof(short)},
                {TwType.Int32,typeof(int)},
                {TwType.UInt8,typeof(byte)},
                {TwType.UInt16,typeof(ushort)},
                {TwType.UInt32,typeof(uint)},
                {TwType.Bool,typeof(TwBool)},
                {TwType.Fix32,typeof(TwFix32)},
                {TwType.Frame,typeof(TwFrame)},
                {TwType.Str32,typeof(TwStr32)},
                {TwType.Str64,typeof(TwStr64)},
                {TwType.Str128,typeof(TwStr128)},
                {TwType.Str255,typeof(TwStr255)},
                {TwType.Str1024,typeof(TwStr1024)},
                {TwType.Uni512,typeof(TwUni512)},
                {TwType.Handle,typeof(IntPtr)}
            };
        private static Dictionary<int, TwType> _typeofAux = new Dictionary<int, TwType> {
                {32,TwType.Str32},
                {64,TwType.Str64},
                {128,TwType.Str128},
                {255,TwType.Str255},
                {1024,TwType.Str1024},
                {512,TwType.Uni512}
            };

        /// <summary>
        /// Возвращает соответствующий twain-типу управляемый тип.
        /// </summary>
        /// <param name="type">Код типа данный twain.</param>
        /// <returns>Управляемый тип.</returns>
        internal static Type TypeOf(TwType type)
        {
            return TwTypeHelper._typeof[type];
        }

        /// <summary>
        /// Возвращает соответствующий управляемому типу twain-тип.
        /// </summary>
        /// <param name="type">Управляемый тип.</param>
        /// <returns>Код типа данный twain.</returns>
        internal static TwType TypeOf(Type type)
        {
            Type _type = type.IsEnum ? Enum.GetUnderlyingType(type) : type;
            foreach (var _item in TwTypeHelper._typeof)
            {
                if (_item.Value == _type)
                {
                    return _item.Key;
                }
            }
            if (type == typeof(bool))
            {
                return TwType.Bool;
            }
            if (type == typeof(float))
            {
                return TwType.Fix32;
            }
            if (type == typeof(RectangleF))
            {
                return TwType.Frame;
            }
            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Возвращает соответствующий объекту twain-тип.
        /// </summary>
        /// <param name="obj">Объект.</param>
        /// <returns>Код типа данный twain.</returns>
        internal static TwType TypeOf(object obj)
        {
            if (obj is string)
            {
                return TwTypeHelper._typeofAux[((string)obj).Length];
            }
            return TwTypeHelper.TypeOf(obj.GetType());
        }

        /// <summary>
        /// Возвращает размер twain-типа в неуправляемом блоке памяти.
        /// </summary>
        /// <param name="type">Код типа данный twain.</param>
        /// <returns>Размер в байтах.</returns>
        internal static int SizeOf(TwType type)
        {
            return Marshal.SizeOf(TwTypeHelper._typeof[type]);
        }

        /// <summary>
        /// Приводит внутренние типы компонента к общим типам среды.
        /// </summary>
        /// <param name="type">Код twain-типа.</param>
        /// <param name="value">Экземпляр объекта.</param>
        /// <returns>Экземпляр объекта.</returns>
        internal static object CastToCommon(TwType type, object value)
        {
            switch (type)
            {
                case TwType.Bool:
                    return (bool)(TwBool)value;
                case TwType.Fix32:
                    return (float)(TwFix32)value;
                case TwType.Frame:
                    return (RectangleF)(TwFrame)value;
                case TwType.Str128:
                case TwType.Str255:
                case TwType.Str32:
                case TwType.Str64:
                case TwType.Uni512:
                case TwType.Str1024:
                    return value.ToString();
            }
            return value;
        }

        /// <summary>
        /// Приводит общие типы среды к внутренним типам компонента.
        /// </summary>
        /// <param name="type">Код twain-типа.</param>
        /// <param name="value">Экземпляр объекта.</param>
        /// <returns>Экземпляр объекта.</returns>
        internal static object CastToTw(TwType type, object value)
        {
            switch (type)
            {
                case TwType.Bool:
                    return (TwBool)(bool)value;
                case TwType.Fix32:
                    return (TwFix32)(float)value;
                case TwType.Frame:
                    return (TwFrame)(RectangleF)value;
                case TwType.Str32:
                    return (TwStr32)value.ToString();
                case TwType.Str64:
                    return (TwStr64)value.ToString();
                case TwType.Str128:
                    return (TwStr128)value.ToString();
                case TwType.Str255:
                    return (TwStr255)value.ToString();
                case TwType.Uni512:
                    return (TwUni512)value.ToString();
                case TwType.Str1024:
                    return (TwStr1024)value.ToString();
            }

            Type _type = value.GetType();
            if (_type.IsEnum && Enum.GetUnderlyingType(_type) == TwTypeHelper.TypeOf(type))
            {
                return Convert.ChangeType(value, Enum.GetUnderlyingType(_type));
            }

            return value;
        }

        /// <summary>
        /// Выполняет преобразование значения в экземпляр внутреннего типа компонента.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="type">Код twain-типа.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Экземпляр объекта.</returns>
        internal static object ValueToTw<T>(TwType type, T value)
        {
            int _size = Marshal.SizeOf(typeof(T));
            IntPtr _mem = Marshal.AllocHGlobal(_size);
            NativeMethods.ZeroMemory(_mem, (IntPtr)_size);
            try
            {
                Marshal.StructureToPtr(value, _mem, true);
                return Marshal.PtrToStructure(_mem, TwTypeHelper.TypeOf(type));
            }
            finally
            {
                Marshal.FreeHGlobal(_mem);
            }
        }

        /// <summary>
        /// Выполняет преобразование экземпляра внутреннего типа компонента в значение.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="value">Экземпляр объекта.</param>
        /// <returns>Значение.</returns>
        internal static T ValueFromTw<T>(object value)
        {
            int _size = Math.Max(Marshal.SizeOf(typeof(T)), Marshal.SizeOf(value));
            IntPtr _mem = Marshal.AllocHGlobal(_size);
            NativeMethods.ZeroMemory(_mem, (IntPtr)_size);
            try
            {
                Marshal.StructureToPtr(value, _mem, true);
                return (T)Marshal.PtrToStructure(_mem, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(_mem);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class TwTypeAttribute : Attribute
    {

        internal TwTypeAttribute(TwType type)
        {
            TwType = type;
        }

        internal TwType TwType
        {
            get;
            private set;
        }
    }

    internal enum TwCap : ushort
    {
        /* image data sources MAY support these caps */
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        XferCount = 0x0001,         // all data sources are REQUIRED to support these caps
        ICompression = 0x0100,      // ICAP_...
        IPixelType = 0x0101,
        IUnits = 0x0102,              //default is TWUN_INCHES
        IXferMech = 0x0103,
        AutoBright = 0x1100,
        Brightness = 0x1101,
        Contrast = 0x1103,
        CustHalftone = 0x1104,
        ExposureTime = 0x1105,
        Filter = 0x1106,
        FlashUsed = 0x1107,
        Gamma = 0x1108,

        [TwType(TwType.Str32)]
        Halftones = 0x1109,

        Highlight = 0x110a,
        ImageFileFormat = 0x110c,
        LampState = 0x110d,
        LightSource = 0x110e,
        Orientation = 0x1110,
        PhysicalWidth = 0x1111,
        PhysicalHeight = 0x1112,
        Shadow = 0x1113,
        Frames = 0x1114,
        XNativeResolution = 0x1116,
        YNativeResolution = 0x1117,
        XResolution = 0x1118,
        YResolution = 0x1119,
        MaxFrames = 0x111a,
        Tiles = 0x111b,
        BitOrder = 0x111c,
        CcittKFactor = 0x111d,
        LightPath = 0x111e,
        PixelFlavor = 0x111f,
        PlanarChunky = 0x1120,
        Rotation = 0x1121,
        SupportedSizes = 0x1122,
        Threshold = 0x1123,
        XScaling = 0x1124,
        YScaling = 0x1125,
        BitOrderCodes = 0x1126,
        PixelFlavorCodes = 0x1127,
        JpegPixelType = 0x1128,
        TimeFill = 0x112a,
        BitDepth = 0x112b,
        BitDepthReduction = 0x112c,  /* Added 1.5 */
        UndefinedImageSize = 0x112d,  /* Added 1.6 */
        ImageDataSet = 0x112e,  /* Added 1.7 */
        ExtImageInfo = 0x112f,  /* Added 1.7 */
        MinimumHeight = 0x1130,  /* Added 1.7 */
        MinimumWidth = 0x1131,  /* Added 1.7 */
        AutoDiscardBlankPages = 0x1134,  /* Added 2.0 */
        FlipRotation = 0x1136,  /* Added 1.8 */
        BarCodeDetectionEnabled = 0x1137,  /* Added 1.8 */
        SupportedBarCodeTypes = 0x1138,  /* Added 1.8 */
        BarCodeMaxSearchPriorities = 0x1139,  /* Added 1.8 */
        BarCodeSearchPriorities = 0x113a,  /* Added 1.8 */
        BarCodeSearchMode = 0x113b,  /* Added 1.8 */
        BarCodeMaxRetries = 0x113c,  /* Added 1.8 */
        BarCodeTimeout = 0x113d,  /* Added 1.8 */
        ZoomFactor = 0x113e,  /* Added 1.8 */
        PatchCodeDetectionEnabled = 0x113f,  /* Added 1.8 */
        SupportedPatchCodeTypes = 0x1140,  /* Added 1.8 */
        PatchCodeMaxSearchPriorities = 0x1141,  /* Added 1.8 */
        PatchCodeSearchPriorities = 0x1142,  /* Added 1.8 */
        PatchCodeSearchMode = 0x1143,  /* Added 1.8 */
        PatchCodeMaxRetries = 0x1144,  /* Added 1.8 */
        PatchCodeTimeout = 0x1145,  /* Added 1.8 */
        FlashUsed2 = 0x1146,  /* Added 1.8 */
        ImageFilter = 0x1147,  /* Added 1.8 */
        NoiseFilter = 0x1148,  /* Added 1.8 */
        OverScan = 0x1149,  /* Added 1.8 */
        AutomaticBorderDetection = 0x1150,  /* Added 1.8 */
        AutomaticDeskew = 0x1151,  /* Added 1.8 */
        AutomaticRotate = 0x1152,  /* Added 1.8 */
        JpegQuality = 0x1153,  /* Added 1.9 */
        FeederType = 0x1154,
        IccProfile = 0x1155,
        AutoSize = 0x1156,
        AutomaticCropUsesFrame = 0x1157,
        AutomaticLengthDetection = 0x1158,
        AutomaticColorEnabled = 0x1159,
        AutomaticColorNonColorPixelType = 0x115a,
        ColorManagementEnabled = 0x115b,
        ImageMerge = 0x115c,
        ImageMergeHeightThreshold = 0x115d,
        SupportedExtImageInfo = 0x115e,
        FilmType = 0x115f,
        Mirror = 0x1160,
        JpegSubSampling = 0x1161,

        /* all data sources MAY support these caps */
        [TwType(TwType.Str128)]
        Author = 0x1000,

        [TwType(TwType.Str255)]
        Caption = 0x1001,

        FeederEnabled = 0x1002,
        FeederLoaded = 0x1003,

        [TwType(TwType.Str32)]
        TimeDate = 0x1004,

        SupportedCaps = 0x1005,
        ExtendedCaps = 0x1006,
        AutoFeed = 0x1007,
        ClearPage = 0x1008,
        FeedPage = 0x1009,
        RewindPage = 0x100a,
        Indicators = 0x100b,   /* Added 1.1 */
        SupportedCapsExt = 0x100c,   /* Added 1.6 */
        PaperDetectable = 0x100d,   /* Added 1.6 */
        UIControllable = 0x100e,   /* Added 1.6 */
        DeviceOnline = 0x100f,   /* Added 1.6 */
        AutoScan = 0x1010,   /* Added 1.6 */
        ThumbnailsEnabled = 0x1011,   /* Added 1.7 */
        Duplex = 0x1012,   /* Added 1.7 */
        DuplexEnabled = 0x1013,   /* Added 1.7 */
        EnableDSUIOnly = 0x1014,   /* Added 1.7 */
        CustomDSData = 0x1015,   /* Added 1.7 */
        Endorser = 0x1016,   /* Added 1.7 */
        JobControl = 0x1017,   /* Added 1.7 */
        Alarms = 0x1018,   /* Added 1.8 */
        AlarmVolume = 0x1019,   /* Added 1.8 */
        AutomaticCapture = 0x101a,   /* Added 1.8 */
        TimeBeforeFirstCapture = 0x101b,   /* Added 1.8 */
        TimeBetweenCaptures = 0x101c,   /* Added 1.8 */
        ClearBuffers = 0x101d,   /* Added 1.8 */
        MaxBatchBuffers = 0x101e,   /* Added 1.8 */

        [TwType(TwType.Str32)]
        DeviceTimeDate = 0x101f,   /* Added 1.8 */

        PowerSupply = 0x1020,   /* Added 1.8 */
        CameraPreviewUI = 0x1021,   /* Added 1.8 */
        DeviceEvent = 0x1022,   /* Added 1.8 */

        [TwType(TwType.Str255)]
        SerialNumber = 0x1024,   /* Added 1.8 */

        Printer = 0x1026,   /* Added 1.8 */
        PrinterEnabled = 0x1027,   /* Added 1.8 */
        PrinterIndex = 0x1028,   /* Added 1.8 */
        PrinterMode = 0x1029,   /* Added 1.8 */

        [TwType(TwType.Str255)]
        PrinterString = 0x102a,   /* Added 1.8 */

        [TwType(TwType.Str255)]
        PrinterSuffix = 0x102b,   /* Added 1.8 */

        Language = 0x102c,   /* Added 1.8 */
        FeederAlignment = 0x102d,   /* Added 1.8 */
        FeederOrder = 0x102e,   /* Added 1.8 */
        ReacquireAllowed = 0x1030,   /* Added 1.8 */
        BatteryMinutes = 0x1032,   /* Added 1.8 */
        BatteryPercentage = 0x1033,   /* Added 1.8 */
        CameraSide = 0x1034,
        Segmented = 0x1035,
        CameraEnabled = 0x1036,
        CameraOrder = 0x1037,
        MicrEnabled = 0x1038,
        FeederPrep = 0x1039,
        FeederPocket = 0x103a,
        AutomaticSenseMedium = 0x103b,

        [TwType(TwType.Str255)]
        CustomInterfaceGuid = 0x103c,

        SupportedCapsSegmentUnique = 0x103d,
        SupportedDats = 0x103e,
        DoubleFeedDetection = 0x103f,
        DoubleFeedDetectionLength = 0x1040,
        DoubleFeedDetectionSensitivity = 0x1041,
        DoubleFeedDetectionResponse = 0x1042,
        PaperHandling = 0x1043,
        IndicatorsMode = 0x1044,
        PrinterVerticalOffset = 0x1045,
        PowerSaveTime = 0x1046,
        PrinterCharRotation = 0x1047,
        PrinterFontStyle = 0x1048,
        PrinterIndexLeadChar = 0x1049,
        PrinterIndexMaxValue = 0x104A,
        PrinterIndexNumDigits = 0x104B,
        PrinterIndexStep = 0x104C,
        PrinterIndexTrigger = 0x104D,
        PrinterStringPreview = 0x104E,
        SheetCount = 0x104F // Controls the number of sheets scanned (compare to CAP_XFERCOUNT that controls images)
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    }

    internal enum TwLanguage : ushort
    {
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        DANISH = 0,             /* Danish                 */
        DUTCH = 1,              /* Dutch                  */
        ENGLISH = 2,            /* International English  */
        FRENCH_CANADIAN = 3,    /* French Canadian        */
        FINNISH = 4,            /* Finnish                */
        FRENCH = 5,             /* French                 */
        GERMAN = 6,             /* German                 */
        ICELANDIC = 7,          /* Icelandic              */
        ITALIAN = 8,            /* Italian                */
        NORWEGIAN = 9,          /* Norwegian              */
        PORTUGUESE = 10,        /* Portuguese             */
        SPANISH = 11,           /* Spanish                */
        SWEDISH = 12,           /* Swedish                */
        ENGLISH_USA = 13,       /* U.S. English           */
        AFRIKAANS = 14,
        ALBANIA = 15,
        ARABIC = 16,
        ARABIC_ALGERIA = 17,
        ARABIC_BAHRAIN = 18,
        ARABIC_EGYPT = 19,
        ARABIC_IRAQ = 20,
        ARABIC_JORDAN = 21,
        ARABIC_KUWAIT = 22,
        ARABIC_LEBANON = 23,
        ARABIC_LIBYA = 24,
        ARABIC_MOROCCO = 25,
        ARABIC_OMAN = 26,
        ARABIC_QATAR = 27,
        ARABIC_SAUDIARABIA = 28,
        ARABIC_SYRIA = 29,
        ARABIC_TUNISIA = 30,
        ARABIC_UAE = 31, /* United Arabic Emirates */
        ARABIC_YEMEN = 32,
        BASQUE = 33,
        BYELORUSSIAN = 34,
        BULGARIAN = 35,
        CATALAN = 36,
        CHINESE = 37,
        CHINESE_HONGKONG = 38,
        CHINESE_PRC = 39, /* People's Reinternal of China */
        CHINESE_SINGAPORE = 40,
        CHINESE_SIMPLIFIED = 41,
        CHINESE_TAIWAN = 42,
        CHINESE_TRADITIONAL = 43,
        CROATIA = 44,
        CZECH = 45,
        DUTCH_BELGIAN = 46,
        ENGLISH_AUSTRALIAN = 47,
        ENGLISH_CANADIAN = 48,
        ENGLISH_IRELAND = 49,
        ENGLISH_NEWZEALAND = 50,
        ENGLISH_SOUTHAFRICA = 51,
        ENGLISH_UK = 52,
        ESTONIAN = 53,
        FAEROESE = 54,
        FARSI = 55,
        FRENCH_BELGIAN = 56,
        FRENCH_LUXEMBOURG = 57,
        FRENCH_SWISS = 58,
        GERMAN_AUSTRIAN = 59,
        GERMAN_LUXEMBOURG = 60,
        GERMAN_LIECHTENSTEIN = 61,
        GERMAN_SWISS = 62,
        GREEK = 63,
        HEBREW = 64,
        HUNGARIAN = 65,
        INDONESIAN = 66,
        ITALIAN_SWISS = 67,
        JAPANESE = 68,
        KOREAN = 69,
        KOREAN_JOHAB = 70,
        LATVIAN = 71,
        LITHUANIAN = 72,
        NORWEGIAN_BOKMAL = 73,
        NORWEGIAN_NYNORSK = 74,
        POLISH = 75,
        PORTUGUESE_BRAZIL = 76,
        ROMANIAN = 77,
        RUSSIAN = 78,
        SERBIAN_LATIN = 79,
        SLOVAK = 80,
        SLOVENIAN = 81,
        SPANISH_MEXICAN = 82,
        SPANISH_MODERN = 83,
        THAI = 84,
        TURKISH = 85,
        UKRANIAN = 86,
        /* More stuff added for 1.8 */
        ASSAMESE = 87,
        BENGALI = 88,
        BIHARI = 89,
        BODO = 90,
        DOGRI = 91,
        GUJARATI = 92,
        HARYANVI = 93,
        HINDI = 94,
        KANNADA = 95,
        KASHMIRI = 96,
        MALAYALAM = 97,
        MARATHI = 98,
        MARWARI = 99,
        MEGHALAYAN = 100,
        MIZO = 101,
        NAGA = 102,
        ORISSI = 103,
        PUNJABI = 104,
        PUSHTU = 105,
        SERBIAN_CYRILLIC = 106,
        SIKKIMI = 107,
        SWEDISH_FINLAND = 108,
        TAMIL = 109,
        TELUGU = 110,
        TRIPURI = 111,
        URDU = 112,
        VIETNAMESE = 113
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    }

    internal enum TwCountry : ushort
    {
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        AFGHANISTAN = 1001,
        ALGERIA = 213,
        AMERICANSAMOA = 684,
        ANDORRA = 033,
        ANGOLA = 1002,
        ANGUILLA = 8090,
        ANTIGUA = 8091,
        ARGENTINA = 54,
        ARUBA = 297,
        ASCENSIONI = 247,
        AUSTRALIA = 61,
        AUSTRIA = 43,
        BAHAMAS = 8092,
        BAHRAIN = 973,
        BANGLADESH = 880,
        BARBADOS = 8093,
        BELGIUM = 32,
        BELIZE = 501,
        BENIN = 229,
        BERMUDA = 8094,
        BHUTAN = 1003,
        BOLIVIA = 591,
        BOTSWANA = 267,
        BRITAIN = 6,
        BRITVIRGINIS = 8095,
        BRAZIL = 55,
        BRUNEI = 673,
        BULGARIA = 359,
        BURKINAFASO = 1004,
        BURMA = 1005,
        BURUNDI = 1006,
        CAMAROON = 237,
        CANADA = 2,
        CAPEVERDEIS = 238,
        CAYMANIS = 8096,
        CENTRALAFREP = 1007,
        CHAD = 1008,
        CHILE = 56,
        CHINA = 86,
        CHRISTMASIS = 1009,
        COCOSIS = 1009,
        COLOMBIA = 57,
        COMOROS = 1010,
        CONGO = 1011,
        COOKIS = 1012,
        COSTARICA = 506,
        CUBA = 005,
        CYPRUS = 357,
        CZECHOSLOVAKIA = 42,
        DENMARK = 45,
        DJIBOUTI = 1013,
        DOMINICA = 8097,
        DOMINCANREP = 8098,
        EASTERIS = 1014,
        ECUADOR = 593,
        EGYPT = 20,
        ELSALVADOR = 503,
        EQGUINEA = 1015,
        ETHIOPIA = 251,
        FALKLANDIS = 1016,
        FAEROEIS = 298,
        FIJIISLANDS = 679,
        FINLAND = 358,
        FRANCE = 33,
        FRANTILLES = 596,
        FRGUIANA = 594,
        FRPOLYNEISA = 689,
        FUTANAIS = 1043,
        GABON = 241,
        GAMBIA = 220,
        GERMANY = 49,
        GHANA = 233,
        GIBRALTER = 350,
        GREECE = 30,
        GREENLAND = 299,
        GRENADA = 8099,
        GRENEDINES = 8015,
        GUADELOUPE = 590,
        GUAM = 671,
        GUANTANAMOBAY = 5399,
        GUATEMALA = 502,
        GUINEA = 224,
        GUINEABISSAU = 1017,
        GUYANA = 592,
        HAITI = 509,
        HONDURAS = 504,
        HONGKONG = 852,
        HUNGARY = 36,
        ICELAND = 354,
        INDIA = 91,
        INDONESIA = 62,
        IRAN = 98,
        IRAQ = 964,
        IRELAND = 353,
        ISRAEL = 972,
        ITALY = 39,
        IVORYCOAST = 225,
        JAMAICA = 8010,
        JAPAN = 81,
        JORDAN = 962,
        KENYA = 254,
        KIRIBATI = 1018,
        KOREA = 82,
        KUWAIT = 965,
        LAOS = 1019,
        LEBANON = 1020,
        LIBERIA = 231,
        LIBYA = 218,
        LIECHTENSTEIN = 41,
        LUXENBOURG = 352,
        MACAO = 853,
        MADAGASCAR = 1021,
        MALAWI = 265,
        MALAYSIA = 60,
        MALDIVES = 960,
        MALI = 1022,
        MALTA = 356,
        MARSHALLIS = 692,
        MAURITANIA = 1023,
        MAURITIUS = 230,
        MEXICO = 3,
        MICRONESIA = 691,
        MIQUELON = 508,
        MONACO = 33,
        MONGOLIA = 1024,
        MONTSERRAT = 8011,
        MOROCCO = 212,
        MOZAMBIQUE = 1025,
        NAMIBIA = 264,
        NAURU = 1026,
        NEPAL = 977,
        NETHERLANDS = 31,
        NETHANTILLES = 599,
        NEVIS = 8012,
        NEWCALEDONIA = 687,
        NEWZEALAND = 64,
        NICARAGUA = 505,
        NIGER = 227,
        NIGERIA = 234,
        NIUE = 1027,
        NORFOLKI = 1028,
        NORWAY = 47,
        OMAN = 968,
        PAKISTAN = 92,
        PALAU = 1029,
        PANAMA = 507,
        PARAGUAY = 595,
        PERU = 51,
        PHILLIPPINES = 63,
        PITCAIRNIS = 1030,
        PNEWGUINEA = 675,
        POLAND = 48,
        PORTUGAL = 351,
        QATAR = 974,
        REUNIONI = 1031,
        ROMANIA = 40,
        RWANDA = 250,
        SAIPAN = 670,
        SANMARINO = 39,
        SAOTOME = 1033,
        SAUDIARABIA = 966,
        SENEGAL = 221,
        SEYCHELLESIS = 1034,
        SIERRALEONE = 1035,
        SINGAPORE = 65,
        SOLOMONIS = 1036,
        SOMALI = 1037,
        SOUTHAFRICA = 27,
        SPAIN = 34,
        SRILANKA = 94,
        STHELENA = 1032,
        STKITTS = 8013,
        STLUCIA = 8014,
        STPIERRE = 508,
        STVINCENT = 8015,
        SUDAN = 1038,
        SURINAME = 597,
        SWAZILAND = 268,
        SWEDEN = 46,
        SWITZERLAND = 41,
        SYRIA = 1039,
        TAIWAN = 886,
        TANZANIA = 255,
        THAILAND = 66,
        TOBAGO = 8016,
        TOGO = 228,
        TONGAIS = 676,
        TRINIDAD = 8016,
        TUNISIA = 216,
        TURKEY = 90,
        TURKSCAICOS = 8017,
        TUVALU = 1040,
        UGANDA = 256,
        USSR = 7,
        UAEMIRATES = 971,
        UNITEDKINGDOM = 44,
        USA = 1,
        URUGUAY = 598,
        VANUATU = 1041,
        VATICANCITY = 39,
        VENEZUELA = 58,
        WAKE = 1042,
        WALLISIS = 1043,
        WESTERNSAHARA = 1044,
        WESTERNSAMOA = 1045,
        YEMEN = 1046,
        YUGOSLAVIA = 38,
        ZAIRE = 243,
        ZAMBIA = 260,
        ZIMBABWE = 263,
        /* Added for 1.8 */
        ALBANIA = 355,
        ARMENIA = 374,
        AZERBAIJAN = 994,
        BELARUS = 375,
        BOSNIAHERZGO = 387,
        CAMBODIA = 855,
        CROATIA = 385,
        CZECHREinternal = 420,
        DIEGOGARCIA = 246,
        ERITREA = 291,
        ESTONIA = 372,
        GEORGIA = 995,
        LATVIA = 371,
        LESOTHO = 266,
        LITHUANIA = 370,
        MACEDONIA = 389,
        MAYOTTEIS = 269,
        MOLDOVA = 373,
        MYANMAR = 95,
        NORTHKOREA = 850,
        PUERTORICO = 787,
        RUSSIA = 7,
        SERBIA = 381,
        SLOVAKIA = 421,
        SLOVENIA = 386,
        SOUTHKOREA = 82,
        UKRAINE = 380,
        USVIRGINIS = 340,
        VIETNAM = 84
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    }

    internal enum TwPixelType : ushort
    {
        BW = 0, /* Black and White */
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        Gray = 1,
        RGB = 2,
        Palette = 3,
        CMY = 4,
        CMYK = 5,
        YUV = 6,
        YUVK = 7,
        CIEXYZ = 8,
        LAB = 9,
        SRGB = 10,
        SCRGB = 11,
        INFRARED = 16
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    }

    internal enum TwCompression : ushort
    {
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        None = 0,
        PackBits = 1,

        /// <summary>
        /// Follows CCITT spec (no End Of Line)
        /// </summary>
        Group31D = 2,

        /// <summary>
        /// Follows CCITT spec (has End Of Line)
        /// </summary>
        Group31Deol = 3,

        /// <summary>
        /// Follows CCITT spec (use cap for K Factor)
        /// </summary>
        Group32D = 4,

        /// <summary>
        /// Follows CCITT spec
        /// </summary>
        Group4 = 5,

        /// <summary>
        /// Use capability for more info
        /// </summary>
        Jpeg = 6,

        /// <summary>
        /// Must license from Unisys and IBM to use
        /// </summary>
        Lzw = 7,

        /// <summary>
        /// For Bitonal images  -- Added 1.7 KHL
        /// </summary>
        Jbig = 8,

        /* Added 1.8 */
        Png = 9,
        Rle4 = 10,
        Rle8 = 11,
        BitFields = 12,
        Zip = 13,
        Jpeg2000 = 14
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    }

    internal enum TwDE : ushort
    {
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        CustomEvents = 0x8000,
        CheckAutomaticCapture = 0,
        CheckBattery = 1,
        CheckDeviceOnline = 2,
        CheckFlash = 3,
        CheckPowerSupply = 4,
        CheckResolution = 5,
        DeviceAdded = 6,
        DeviceOffline = 7,
        DeviceReady = 8,
        DeviceRemoved = 9,
        ImageCaptured = 10,
        ImageDeleted = 11,
        PaperDoubleFeed = 12,
        PaperJam = 13,
        LampFailure = 14,
        PowerSave = 15,
        PowerSaveNotify = 16
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    internal sealed class TwStr32
    {

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
        internal string Value;

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(TwStr32 value)
        {
            return value != null ? value.Value : null;
        }

        public static implicit operator TwStr32(string value)
        {
            return new TwStr32
            {
                Value = value
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    internal sealed class TwStr64
    {

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 66)]
        internal string Value;

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(TwStr64 value)
        {
            return value != null ? value.Value : null;
        }

        public static implicit operator TwStr64(string value)
        {
            return new TwStr64
            {
                Value = value
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    internal sealed class TwStr128
    {

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 130)]
        internal string Value;

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(TwStr128 value)
        {
            return value != null ? value.Value : null;
        }

        public static implicit operator TwStr128(string value)
        {
            return new TwStr128
            {
                Value = value
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    internal sealed class TwStr255
    {

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        internal string Value;

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(TwStr255 value)
        {
            return value != null ? value.Value : null;
        }

        public static implicit operator TwStr255(string value)
        {
            return new TwStr255
            {
                Value = value
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
    internal sealed class TwUni512
    {

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        internal string Value;

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(TwUni512 value)
        {
            return value != null ? value.Value : null;
        }

        public static implicit operator TwUni512(string value)
        {
            return new TwUni512
            {
                Value = value
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    internal sealed class TwStr1024
    {

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1026)]
        internal string Value;

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(TwStr1024 value)
        {
            return value != null ? value.Value : null;
        }

        public static implicit operator TwStr1024(string value)
        {
            return new TwStr1024
            {
                Value = value
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct TwBool
    {

        internal ushort Value;

        private bool ToBool()
        {
            return Value != 0;
        }

        public static implicit operator bool(TwBool value)
        {
            return value.ToBool();
        }

        public static implicit operator TwBool(bool value)
        {
            return new TwBool
            {
                Value = (ushort)(value ? 1 : 0)
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct TwFix32
    {                                   // TW_FIX32

        /// <summary>
        /// Целая часть.
        /// </summary>
        internal short Whole;

        /// <summary>
        /// Дробная часть.
        /// </summary>
        internal ushort Frac;

        /// <summary>
        /// Приводит тип к числу с плавающей запятой.
        /// </summary>
        /// <returns>Число с плавающей точкой.</returns>
        private float ToFloat()
        {
            return (float)Whole + ((float)Frac / 65536.0f);
        }

        /// <summary>
        /// Создает экземпляр TwFix32 из числа с плавающей точкой.
        /// </summary>
        /// <param name="f">Число с плавающей точкой.</param>
        /// <returns>Экземпляр TwFix32.</returns>
        public static implicit operator TwFix32(float f)
        {
            int i = (int)((f * 65536.0f) + 0.5f);
            return new TwFix32()
            {
                Whole = (short)(i >> 16),
                Frac = (ushort)(i & 0x0000ffff)
            };
        }

        /// <summary>
        /// Создает экземпляр TwFix32 из целого числа.
        /// </summary>
        /// <param name="value">Целое число.</param>
        /// <returns>Экземпляр TwFix32.</returns>
        public static explicit operator TwFix32(uint value)
        {
            return new TwFix32()
            {
                Whole = (short)(value & 0x0000ffff),
                Frac = (ushort)(value >> 16)
            };
        }

        /// <summary>
        /// Приводит тип к числу с плавающей запятой.
        /// </summary>
        /// <param name="value">Экземпляр TwFix32.</param>
        /// <returns>Число с плавающей точкой.</returns>
        public static implicit operator float(TwFix32 value)
        {
            return value.ToFloat();
        }

        /// <summary>
        /// Приводит тип к целому числу.
        /// </summary>
        /// <param name="value">Экземпляр TwFix32.</param>
        /// <returns>Целое число.</returns>
        public static explicit operator uint(TwFix32 value)
        {
            return (uint)(ushort)value.Whole + ((uint)value.Frac << 16);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct TwFrame
    {

        /// <summary>
        /// Gets or sets the x-coordinate of the left edge.
        /// </summary>
        internal TwFix32 Left;

        /// <summary>
        /// Gets or sets the y-coordinate of the top edge.
        /// </summary>
        internal TwFix32 Top;

        /// <summary>
        /// Gets or sets the x-coordinate that is the sum of TwFrame.Left and width of image.
        /// </summary>
        internal TwFix32 Right;

        /// <summary>
        /// Gets or sets the y-coordinate that is the sum of TwFrame.Top and length of image.
        /// </summary>
        internal TwFix32 Bottom;

        private RectangleF ToRectangle()
        {
            return new RectangleF(
                Left,
                Top,
                Right - Left,
                Bottom - Top);
        }

        public static implicit operator RectangleF(TwFrame value)
        {
            return value.ToRectangle();
        }

        public static implicit operator TwFrame(RectangleF value)
        {
            return new TwFrame()
            {
                Left = value.Left,
                Top = value.Top,
                Right = value.Right,
                Bottom = value.Bottom
            };
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    internal class TwIdentity
    {									// TW_IDENTITY

        /// <summary>
        /// Unique number.  In Windows, application hWnd.
        /// </summary>
        internal uint Id;

        /// <summary>
        /// Identifies the piece of code
        /// </summary>
        internal TwVersion Version;

        /// <summary>
        /// Application and DS must set to TWON_PROTOCOLMAJOR
        /// </summary>
        internal ushort ProtocolMajor;

        /// <summary>
        /// Application and DS must set to TWON_PROTOCOLMINOR
        /// </summary>
        internal ushort ProtocolMinor;

        /// <summary>
        /// Bit field OR combination of DG_ constants
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        internal TwDG SupportedGroups;

        /// <summary>
        /// Manufacturer name, e.g. "Hewlett-Packard"
        /// </summary>
        internal TwStr32 Manufacturer;

        /// <summary>
        /// Product family name, e.g. "ScanJet"
        /// </summary>
        internal TwStr32 ProductFamily;

        /// <summary>
        /// Product name, e.g. "ScanJet Plus"
        /// </summary>
        internal TwStr32 ProductName;

        public override bool Equals(object obj)
        {
            if (obj != null && obj is TwIdentity)
            {
                return ((TwIdentity)obj).Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    internal struct TwVersion
    {                                   // TW_VERSION

        /// <summary>
        /// Major revision number of the software.
        /// </summary>
        internal ushort MajorNum;

        /// <summary>
        /// Incremental revision number of the software.
        /// </summary>
        internal ushort MinorNum;

        /// <summary>
        /// e.g. TWLG_SWISSFRENCH
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwLanguage Language;

        /// <summary>
        /// e.g. TWCY_SWITZERLAND
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwCountry Country;

        /// <summary>
        /// e.g. "1.0b3 Beta release"
        /// </summary>
        internal TwStr32 Info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwUserInterface
    {                           // TW_USERINTERFACE

        /// <summary>
        /// TRUE if DS should bring up its UI
        /// </summary>
        internal TwBool ShowUI;               // bool is strictly 32 bit, so use short

        /// <summary>
        /// For Mac only - true if the DS's UI is modal
        /// </summary>
        internal TwBool ModalUI;

        /// <summary>
        /// For windows only - Application window handle
        /// </summary>
        internal IntPtr ParentHand;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwStatus
    {                                   // TW_STATUS

        /// <summary>
        /// Any TwCC constant
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwCC ConditionCode;      // TwCC

        /// <summary>
        /// Future expansion space
        /// </summary>
        internal ushort Reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwEvent
    {                                   // TW_EVENT

        /// <summary>
        /// Windows pMSG or Mac pEvent.
        /// </summary>
        internal IntPtr EventPtr;

        /// <summary>
        /// TwMSG from data source, e.g. TwMSG.XFerReady
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwMSG Message;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwImageInfo
    {                               // TW_IMAGEINFO

        /// <summary>
        /// Resolution in the horizontal
        /// </summary>
        internal TwFix32 XResolution;

        /// <summary>
        /// Resolution in the vertical
        /// </summary>
        internal TwFix32 YResolution;

        /// <summary>
        /// Columns in the image, -1 if unknown by DS
        /// </summary>
        internal int ImageWidth;

        /// <summary>
        /// Rows in the image, -1 if unknown by DS
        /// </summary>
        internal int ImageLength;

        /// <summary>
        /// Number of samples per pixel, 3 for RGB
        /// </summary>
        internal short SamplesPerPixel;

        /// <summary>
        /// Number of bits for each sample
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        internal short[] BitsPerSample;

        /// <summary>
        /// Number of bits for each padded pixel
        /// </summary>
        internal short BitsPerPixel;

        /// <summary>
        /// True if Planar, False if chunky
        /// </summary>
        internal TwBool Planar;

        /// <summary>
        /// How to interp data; photo interp
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwPixelType PixelType;

        /// <summary>
        /// How the data is compressed
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwCompression Compression;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwPendingXfers
    {                               // TW_PENDINGXFERS
        internal ushort Count;
        internal uint EOJ;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwCapability : IDisposable
    {                   // TW_CAPABILITY

        /// <summary>
        /// Id of capability to set or get, e.g. TwCap.Brightness
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwCap Cap;

        /// <summary>
        /// TwOn.One, TwOn.Range, TwOn.Array or TwOn.Enum
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwOn ConType;

        /// <summary>
        /// Handle to container of type Dat
        /// </summary>
        internal IntPtr Handle;

        private TwCapability()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwCapability"/> class.
        /// </summary>
        /// <param name="cap">The cap.</param>
        internal TwCapability(TwCap cap)
        {
            Cap = cap;
            ConType = TwOn.DontCare;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwCapability"/> class.
        /// </summary>
        /// <param name="cap">The cap.</param>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        internal TwCapability(TwCap cap, uint value, TwType type)
        {
            Cap = cap;
            ConType = TwOn.One;
            _SetValue(new TwOneValue()
            {
                ItemType = type,
                Item = value
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwCapability"/> class.
        /// </summary>
        /// <param name="cap">The cap.</param>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        internal TwCapability(TwCap cap, string value, TwType type)
        {
            Cap = cap;
            ConType = TwOn.One;
            int _twOneCustumValueSize = Marshal.SizeOf(typeof(TwOneCustumValue));
            Handle = NativeMethods.GlobalAlloc(0x42, _twOneCustumValueSize + Marshal.SizeOf(TwTypeHelper.TypeOf(type)));
            IntPtr _ptr = NativeMethods.GlobalLock(Handle);
            try
            {
                Marshal.StructureToPtr(new TwOneCustumValue { ItemType = type }, _ptr, true);
                Marshal.StructureToPtr(TwTypeHelper.CastToTw(type, value), (IntPtr)(_ptr.ToInt64() + _twOneCustumValueSize), true);
            }
            finally
            {
                NativeMethods.GlobalUnlock(Handle);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwCapability"/> class.
        /// </summary>
        /// <param name="cap">The cap.</param>
        /// <param name="range">The range.</param>
        internal TwCapability(TwCap cap, TwRange range)
        {
            Cap = cap;
            ConType = TwOn.Range;
            _SetValue(range);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwCapability"/> class.
        /// </summary>
        /// <param name="cap">The cap.</param>
        /// <param name="array">The array.</param>
        /// <param name="arrayValue">The array value.</param>
        internal TwCapability(TwCap cap, TwArray array, object[] arrayValue)
        {
            Cap = cap;
            ConType = TwOn.Array;
            int _twArraySize = Marshal.SizeOf(typeof(TwArray));
            int _twItemSize = Marshal.SizeOf(TwTypeHelper.TypeOf(array.ItemType));
            Handle = NativeMethods.GlobalAlloc(0x42, _twArraySize + (_twItemSize * arrayValue.Length));
            IntPtr _pTwArray = NativeMethods.GlobalLock(Handle);
            try
            {
                Marshal.StructureToPtr(array, _pTwArray, true);
                for (long i = 0, _ptr = _pTwArray.ToInt64() + _twArraySize; i < arrayValue.Length; i++, _ptr += _twItemSize)
                {
                    Marshal.StructureToPtr(TwTypeHelper.CastToTw(array.ItemType, arrayValue[i]), (IntPtr)_ptr, true);
                }
            }
            finally
            {
                NativeMethods.GlobalUnlock(Handle);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwCapability"/> class.
        /// </summary>
        /// <param name="cap">The cap.</param>
        /// <param name="enumeration">The enumeration.</param>
        /// <param name="enumerationValue">The enumeration value.</param>
        internal TwCapability(TwCap cap, TwEnumeration enumeration, object[] enumerationValue)
        {
            Cap = cap;
            ConType = TwOn.Enum;
            int _twEnumerationSize = Marshal.SizeOf(typeof(TwEnumeration));
            int _twItemSize = Marshal.SizeOf(TwTypeHelper.TypeOf(enumeration.ItemType));
            Handle = NativeMethods.GlobalAlloc(0x42, _twEnumerationSize + (_twItemSize * enumerationValue.Length));
            IntPtr _pTwEnumeration = NativeMethods.GlobalLock(Handle);
            try
            {
                Marshal.StructureToPtr(enumeration, _pTwEnumeration, true);
                for (long i = 0, _ptr = _pTwEnumeration.ToInt64() + _twEnumerationSize; i < enumerationValue.Length; i++, _ptr += _twItemSize)
                {
                    Marshal.StructureToPtr(TwTypeHelper.CastToTw(enumeration.ItemType, enumerationValue[i]), (IntPtr)_ptr, true);
                }
            }
            finally
            {
                NativeMethods.GlobalUnlock(Handle);
            }
        }

        /// <summary>
        /// Возвращает результат для указаной возможности.
        /// </summary>
        /// <returns>Экземпляр TwArray, TwEnumeration, _TwRange или _TwOneValue.</returns>
        internal object GetValue()
        {
            IntPtr _ptr = NativeMethods.GlobalLock(Handle);
            try
            {
                switch (ConType)
                {
                    case TwOn.Array:
                        return new __TwArray((TwArray)Marshal.PtrToStructure(_ptr, typeof(TwArray)), (IntPtr)(_ptr.ToInt64() + Marshal.SizeOf(typeof(TwArray))));
                    case TwOn.Enum:
                        return new __TwEnumeration((TwEnumeration)Marshal.PtrToStructure(_ptr, typeof(TwEnumeration)), (IntPtr)(_ptr.ToInt64() + Marshal.SizeOf(typeof(TwEnumeration))));
                    case TwOn.Range:
                        return Marshal.PtrToStructure(_ptr, typeof(TwRange));
                    case TwOn.One:
                        TwOneCustumValue _value = Marshal.PtrToStructure(_ptr, typeof(TwOneCustumValue)) as TwOneCustumValue;
                        switch (_value.ItemType)
                        {
                            case TwType.Str32:
                            case TwType.Str64:
                            case TwType.Str128:
                            case TwType.Str255:
                            case TwType.Str1024:
                            case TwType.Uni512:
                                return Marshal.PtrToStructure((IntPtr)(_ptr.ToInt64() + Marshal.SizeOf(typeof(TwOneCustumValue))), TwTypeHelper.TypeOf(_value.ItemType)).ToString();
                            default:
                                return Marshal.PtrToStructure(_ptr, typeof(TwOneValue));
                        }
                }
                return null;
            }
            finally
            {
                NativeMethods.GlobalUnlock(Handle);
            }
        }

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                NativeMethods.GlobalFree(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion

        private void _SetValue<T>(T value)
        {
            Handle = NativeMethods.GlobalAlloc(0x42, Marshal.SizeOf(typeof(T)));
            IntPtr _ptr = NativeMethods.GlobalLock(Handle);
            try
            {
                Marshal.StructureToPtr(value, _ptr, true);
            }
            finally
            {
                NativeMethods.GlobalUnlock(Handle);
            }
        }
    }

    internal interface ITwArray
    {

        TwType ItemType
        {
            get;
            set;
        }

        uint NumItems
        {
            get;
            set;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwArray : ITwArray
    {                                    //TWON_ARRAY. Container for array of values (a simplified TW_ENUMERATION)
        [MarshalAs(UnmanagedType.U2)]
        private TwType _itemType;
        private uint _numItems;    /* How many items in ItemList           */
                                   //[MarshalAs(UnmanagedType.ByValArray,SizeConst=1)]
                                   //internal byte[] ItemList; /* Array of ItemType values starts here */

        public TwType ItemType
        {
            get
            {
                return _itemType;
            }
            set
            {
                _itemType = value;
            }
        }

        public uint NumItems
        {
            get
            {
                return _numItems;
            }
            set
            {
                _numItems = value;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwEnumeration : ITwArray
    {                              //TWON_ENUMERATION. Container for a collection of values.
        [MarshalAs(UnmanagedType.U2)]
        private TwType _ItemType;
        private uint _numItems;     /* How many items in ItemList                 */
        private uint _currentIndex; /* Current value is in ItemList[CurrentIndex] */
        private uint _defaultIndex; /* Powerup value is in ItemList[DefaultIndex] */
                                    //[MarshalAs(UnmanagedType.ByValArray,SizeConst=1)]
                                    //internal byte[] ItemList;  /* Array of ItemType values starts here       */

        public TwType ItemType
        {
            get
            {
                return _ItemType;
            }
            set
            {
                _ItemType = value;
            }
        }

        public uint NumItems
        {
            get
            {
                return _numItems;
            }
            set
            {
                _numItems = value;
            }
        }

        internal uint CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
            }
        }

        internal uint DefaultIndex
        {
            get
            {
                return _defaultIndex;
            }
            set
            {
                _defaultIndex = value;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwOneValue
    {                                 //TW_ONEVALUE. Container for one value.
        [MarshalAs(UnmanagedType.U2)]
        internal TwType ItemType;
        internal uint Item;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwOneCustumValue
    {                                 //TW_ONEVALUE. Container for one value.
        [MarshalAs(UnmanagedType.U2)]
        internal TwType ItemType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwRange
    {                                    //TWON_RANGE. Container for a range of values.
        [MarshalAs(UnmanagedType.U2)]
        internal TwType ItemType;
        internal uint MinValue;     /* Starting value in the range.           */
        internal uint MaxValue;     /* Final value in the range.              */
        internal uint StepSize;     /* Increment from MinValue to MaxValue.   */
        internal uint DefaultValue; /* Power-up value.                        */
        internal uint CurrentValue; /* The value that is currently in effect. */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class TwDeviceEvent
    {

        /// <summary>
        /// One of the TWDE_xxxx values.
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        internal TwDE Event;

        private ushort reserved;

        /// <summary>
        /// The name of the device that generated the event.
        /// </summary>
        internal TwStr255 DeviceName;

        /// <summary>
        /// Battery Minutes Remaining.
        /// </summary>
        internal uint BatteryMinutes;

        /// <summary>
        /// Battery Percentage Remaining.
        /// </summary>
        internal short BatteryPercentAge;

        /// <summary>
        /// Power Supply.
        /// </summary>
        internal int PowerSupply;

        /// <summary>
        /// Resolution.
        /// </summary>
        internal TwFix32 XResolution;

        /// <summary>
        /// Resolution.
        /// </summary>
        internal TwFix32 YResolution;

        /// <summary>
        /// Flash Used2.
        /// </summary>
        internal uint FlashUsed2;

        /// <summary>
        /// Automatic Capture.
        /// </summary>
        internal uint AutomaticCapture;

        /// <summary>
        /// Automatic Capture.
        /// </summary>
        internal uint TimeBeforeFirstCapture;

        /// <summary>
        /// Automatic Capture.
        /// </summary>
        internal uint TimeBetweenCaptures;
    }

    internal interface __ITwArray
    {

        TwType ItemType
        {
            get;
        }

        uint NumItems
        {
            get;
        }

        object[] Items
        {
            get;
        }
    }

    internal interface __ITwEnumeration : __ITwArray
    {

        int CurrentIndex
        {
            get;
        }

        int DefaultIndex
        {
            get;
        }
    }

    internal class __TwArray : __ITwArray
    {
        private ITwArray _data;
        private object[] _items;

        public __TwArray(ITwArray data, IntPtr items)
        {
            _data = data;
            _items = new object[_data.NumItems];
            for (long i = 0, _offset = 0, _sizeof = TwTypeHelper.SizeOf(_data.ItemType); i < _data.NumItems; i++, _offset += _sizeof)
            {
                _items[i] = TwTypeHelper.CastToCommon(_data.ItemType, Marshal.PtrToStructure((IntPtr)(items.ToInt64() + _offset), TwTypeHelper.TypeOf(_data.ItemType)));
            }
        }

        public TwType ItemType
        {
            get
            {
                return _data.ItemType;
            }
        }

        public uint NumItems
        {
            get
            {
                return _data.NumItems;
            }
        }

        public object[] Items
        {
            get
            {
                return _items;
            }
        }
    }

    internal class __TwEnumeration : __TwArray, __ITwEnumeration
    {
        private TwEnumeration _data;

        public __TwEnumeration(TwEnumeration data, IntPtr items) : base(data, items)
        {
            _data = data;
        }

        public int CurrentIndex
        {
            get
            {
                return (int)_data.CurrentIndex;
            }
        }

        public int DefaultIndex
        {
            get
            {
                return (int)_data.DefaultIndex;
            }
        }
    }
}
