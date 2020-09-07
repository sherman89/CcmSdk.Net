namespace CcmSdk.Net.DTO
{
    public class IcaApplication
    {
        public int ApplicationID;
        public int SessionID;
        public string FriendlyName;
        public string Title;
        public string ClassName;

        /// <summary>
        /// Load this into a System.Drawing.Bitmap
        /// </summary>
        public byte[] IconByteArray;
    }
}
