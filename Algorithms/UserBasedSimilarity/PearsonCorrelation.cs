using System.Linq;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.Mathematics;

namespace DataMining.Learning.Algorithms.UserBasedSimilarity
{
    public class PearsonCorrelation : ICorrelationAlgorithm
    {
        public Correlation ComputeCorrelation(NamedVector<NamedValue> vector1, NamedVector<NamedValue> vector2)
        {
            var jointValues = vector1.Values.Join(vector2.Values, nv => nv.Name, nv => nv.Name,
                                                  (f, s) => new { X = f.Value, Y = s.Value }).ToList();

            // calculating numerator

            var dotProduct = jointValues.Sum(p => p.X * p.Y);
            var productOfSums = jointValues.Sum(p => p.X) * jointValues.Sum(p => p.Y);

            var numerator = dotProduct - (productOfSums / jointValues.Count);

            // calculating denominator

            var squareOfSumX = jointValues.Sum(p => p.X).Square();
            var magnitudeX = jointValues.Sum(p => p.X.Square());
            var squareRoot1 = (magnitudeX - squareOfSumX / jointValues.Count).SquareRoot();

            var squareOfSumY = jointValues.Sum(p => p.Y).Square();
            var magnitudeY = jointValues.Sum(p => p.Y.Square());
            var sqaureRoot2 = (magnitudeY - squareOfSumY / jointValues.Count).SquareRoot();

            var denominator = squareRoot1 * sqaureRoot2;
            
            // calculating result
            var result = numerator/denominator;

            return new Similarity(vector1.Name, vector2.Name, result);
        }
    }
}
