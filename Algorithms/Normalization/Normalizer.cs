using System;

namespace DataMining.Learning.Algorithms.Normalization
{
    public class Normalizer
    {
        private readonly Range _range;

        public Normalizer(Range range)
        {
            if (range == null) 
                throw new ArgumentNullException("range");
           
            _range = range;
        }

        public double Normalize(double value)
        {
            if(!_range.Contains(value))
                throw new ArgumentOutOfRangeException("value");

            return (2*(value - _range.From) - (_range.To - _range.From))/(_range.To - _range.From);
        }

        public double Denormalize(double value)
        {
            if(value < -1 || value > 1)
                throw new ArgumentOutOfRangeException("value");

            return 0.5*(value + 1)*(_range.To - _range.From) + _range.From;
        }
    }
}
