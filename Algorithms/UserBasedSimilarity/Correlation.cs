using System;

namespace DataMining.Learning.Algorithms.UserBasedSimilarity
{
    public abstract class Correlation : IComparable<Correlation>, IEquatable<Correlation>
    {
        protected Correlation(string first, string second, double value)
        {
            First = first;
            Second = second;
            Value = value;
        }

        public string First { get; private set; }

        public string Second { get; private set; }

        public double Value { get; private set; }

        public int CompareTo(Correlation other)
        {
            if (CurrentCorrelationName() != other.CurrentCorrelationName())
            {
                throw new InvalidOperationException(string.Format("Cannot compare 2 non-similiar types {0} and {1}",
                                                                  CurrentCorrelationName(), other.CurrentCorrelationName()));
            }

            return CompareInternal(other.Value);
        }

        public bool Equals(Correlation other)
        {
            return CompareTo(other) == 0;
        }

        public sealed override string ToString()
        {
            return string.Format("{0} of {1} to {2} is equal to {3}", CurrentCorrelationName(), First, Second, Value);
        }

        protected abstract int CompareInternal(double other);

        protected abstract string CurrentCorrelationName();
    }
}
