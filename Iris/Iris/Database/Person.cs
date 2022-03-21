namespace Iris.Database
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Letter> ReceivedLetters { get; set; }
        public IEnumerable<Letter> SentLetters { get; set; }
    }
}
