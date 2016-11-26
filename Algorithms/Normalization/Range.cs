using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning.Algorithms.Normalization
{
    public class Range
    {
        public Range(double from, double to)
        {
            if(from >= to)
                throw new ArgumentOutOfRangeException("from", "from should be less than to");

            From = from;
            To = to;
        }

        public double From { get; private set; }

        public double To { get; private set; }

        public bool Contains(double value)
        {
            return From <= value && value <= To;
        }

        public static Range Of(double from, double to)
        {
            return new Range(from, to);
        }
    }
}
