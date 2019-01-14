namespace CsvIO
{
    internal class CaptionLine
    {
        internal string[] Title { get; set; }
        public override string ToString() => string.Join(",", Title);
    }
}