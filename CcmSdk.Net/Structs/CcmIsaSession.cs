using System.Runtime.InteropServices;

namespace CcmSdk.Net.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CcmIcaSession
    {
        public int SessionId;
        public int ConnectionID;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string FriendlyName;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string NonSeamlessAppTitle;

        public uint IsFullScreen;
        public uint Ssl;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string EncryptionLevel;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string EngineVersion;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string ServerName;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string UserName;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string DomainName;

        public uint RxFrameCount;
        public uint TxFrameCount;
        public uint RxByteCount;
        public uint TxByteCount;
        public uint RxFrameErrorCount;
        public uint TxFrameErrorCount;
        public uint SeamlessMode;
        public uint ZlMode;
        public uint CGP;
        public uint SpeedBrowseEnabled;
        public uint LastLatency;
        public uint AverageLatency;
        public uint RoundTripDeviation;
        public uint HRes;
        public uint VRes;
        public uint ColorDepth;
        public uint AudioEnabled;
        public uint PdaEnabled;
        public uint TwnEnabled;
        public uint PnpEnabled;
    }
}
