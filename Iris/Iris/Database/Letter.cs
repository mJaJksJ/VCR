namespace Iris.Database
{
    public class Letter
    {
        public int Id { get; set; }
        public Person Sender { get; set; }
        public int SenderId { get; set; }
        public IEnumerable<Person> Receivers { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }

        public string Text { get; set; }

        public IEnumerable<Attachment> Attacments { get; set; }

        public int AccoundId { get; set; }
        public Account Account { get; set; }

    }
}
