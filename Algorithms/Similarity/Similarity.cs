using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning.Algorithms.Similarity
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
