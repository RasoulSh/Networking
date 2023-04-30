namespace Networking.Entities
{
    public abstract class NetworkFile
    {
        public abstract string FileName { get; set; }
        public abstract byte[] Contents { get; }
        public abstract string MimeType { get; }
    }
}