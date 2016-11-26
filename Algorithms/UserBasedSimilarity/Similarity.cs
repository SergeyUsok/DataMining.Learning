namespace DataMining.Learning.Algorithms.UserBasedSimilarity
{
    public class Similarity : Correlation
    {
        public Similarity(string first, string second, double value) 
            : base(first, second, value)
        {
        }

        protected override int CompareInternal(double other)
        {
            return Value.CompareTo(other);
        }

        protected override string CurrentCorrelationName()
        {
            return "Similarity";
        }
    }
}
