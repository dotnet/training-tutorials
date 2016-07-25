namespace ConsoleApplication
{
    public class Quotation
    {
        public string Quote { get; set; }
        public string Author { get; set;}

        public override string ToString()
        {
            return Quote + " - " + Author;
        }
    }
}