using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning.Algorithms.Similarity
{
    public class Distance : Correlation
    {
        public Distance(string first, string second, double value) 
            : base(first, second, value)
        {
        }

        // Distance is inversely proportional to Correlation
        protected override int CompareInternal(double other)
        {
            if (Value > other)
                return -1;

            if (Value < other)
                return 1;

            return 0;
        }

        protected override string CurrentCorrelationName()
        {
            return "Distance";
        }
    }
}
