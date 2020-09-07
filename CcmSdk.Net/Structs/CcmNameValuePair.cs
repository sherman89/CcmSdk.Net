using System.Runtime.InteropServices;

namespace CcmSdk.Net.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CcmNameValuePair
    {
        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string Name;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string Value;
    }
}
