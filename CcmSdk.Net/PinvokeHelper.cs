using System;
using System.Drawing;
using System.IO;

namespace CcmSdk.Net
{
    internal static class PinvokeHelper
    {
        internal static byte[] GetIconBytesFromIntPtr(IntPtr hIconPtr)
        {
            using (var ms = new MemoryStream())
            {
                using (var ico = Icon.FromHandle(hIconPtr))
                {
                    ico.Save(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}
