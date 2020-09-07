using System;
using System.Runtime.InteropServices;

namespace CcmSdk.Net.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CcmIcaApplication
    {
        public int ApplicationID;
        public int SessionID;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string FriendlyName;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string Title;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string ClassName;

        // TODO: What to do with these 2 fields?
        public uint IconSize;
        public IntPtr IconData;

        public IntPtr hIcon;
    }
}
