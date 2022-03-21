namespace Iris.Database
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Blob { get; set; }
        public Letter Letter { get; set; }
        public int LetterId { get; set; }
    }
}
