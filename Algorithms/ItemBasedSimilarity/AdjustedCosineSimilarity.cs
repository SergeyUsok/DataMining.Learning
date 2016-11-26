using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.Algorithms.UserBasedSimilarity;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;
using DataMining.Learning.Mathematics;

namespace DataMining.Learning.Algorithms.ItemBasedSimilarity
{
    public class AdjustedCosineSimilarity : IItemBasedSimilarity
    {
        public Matrix<double> BuildSimilarityMatrix(IUserItemRelation data)
        {
            var userAverages = data.Primary.ToDictionary(u => u.Name,
                                                         u => data.Association.GetRecords(u).Average(jr => jr.Value));

            return data.Secondary.Select(item => CreateVector(item, data))
                                 .CrossPairwise((vector1, vector2) => ComputeSimilarity(vector1, vector2, userAverages))
                                 .ToMatrix();
        }

        private NamedVector<NamedValue> CreateVector(Item item, IUserItemRelation data)
        {
            var associated = data.Association
                                 .Where(jr => item.Name.Equals(jr.Second.Name));

            return new NamedVector<NamedValue>(item.Name,
                                               associated.Select(jr => new NamedValue(jr.First.Name, jr.Value))
                                                         .ToList());
        }

        private Similarity ComputeSimilarity(NamedVector<NamedValue> vector1, NamedVector<NamedValue> vector2,
                                             Dictionary<string, double> averages)
        {
            var sharedUsers = vector1.Values.Join(vector2.Values,
                                                  nv => nv.Name,
                                                  nv => nv.Name,
                                                  (first, second) =>
                                                  new
                                                      {
                                                          Item1 = first.Value,
                                                          Item2 = second.Value,
                                                          UserAverage = averages[first.Name]
                                                      })
                                     .ToList();

            var numerator = sharedUsers.Sum(user => (user.Item1 - user.UserAverage) * (user.Item2 - user.UserAverage));

            var root1 = sharedUsers.Sum(user => (user.Item1 - user.UserAverage).Square()).SquareRoot();
            var root2 = sharedUsers.Sum(user => (user.Item2 - user.UserAverage).Square()).SquareRoot();

            var denominator = root1*root2;

            var result = numerator/denominator;

            return new Similarity(vector1.Name, vector2.Name, result);
        }
    }
}
