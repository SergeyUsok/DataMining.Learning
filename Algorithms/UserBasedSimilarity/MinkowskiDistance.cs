using System;
using System.Linq;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.Mathematics;

namespace DataMining.Learning.Algorithms.UserBasedSimilarity
{
    public class MinkowskiDistance : ICorrelationAlgorithm
    {
        private readonly double _exponent;

        public MinkowskiDistance(double exponent)
        {
            if(exponent <= 0)
                throw new ArgumentOutOfRangeException("exponent", "Exponent should be greater than zero");

            _exponent = exponent;
        }

        public Correlation ComputeCorrelation(NamedVector<NamedValue> vector1, NamedVector<NamedValue> vector2)
        {
            var jointValues = vector1.Values.Join(vector2.Values, nv => nv.Name, nv => nv.Name,
                                                  (f, s) => new { X = f.Value, Y = s.Value }).ToList();

            var sum = jointValues.Sum(p => (p.X - p.X).ToAbsolute().ToPowerOf(_exponent));

            var result = sum.RootOfDegree(_exponent);

            return new Distance(vector1.Name, vector2.Name, result);
        }
    }
}
