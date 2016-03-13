using System.Linq;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.Interfaces;
using DataMining.Learning.Mathematics;
using DataMining.Learning.Recommendation;

namespace DataMining.Learning.Algorithms.Similarity
{
    public class CosineSimilarity : ICorrelationAlgorithm
    {
        public Correlation ComputeCorrelation(NamedVector<NamedValue> vector1, NamedVector<NamedValue> vector2)
        {
            var jointValues = vector1.Values.Join(vector2.Values, nv => nv.Name, nv => nv.Name,
                                                  (f, s) => new {X = f.Value, Y = s.Value}).ToList();

            var dotProduct = jointValues.Sum(p => p.X * p.Y);

            var magnitudeX = jointValues.Sum(p => p.X.Square());
            var magnitudeY = jointValues.Sum(p => p.Y.Square());
            var denominator = magnitudeX.SquareRoot() * magnitudeY.SquareRoot();

            var result = dotProduct/denominator;

            return new Similarity(vector1.Name, vector2.Name, result);
        }
    }
}
