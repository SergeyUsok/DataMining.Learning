using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Learning.DataObjects;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Recommendation
{
    public class WeightedSlopeOneEngine : ISimilarityEngine<Item>
    {
        public void Train(IUserItemRelation data)
        {
            var deviationsMatrix = data.Secondary.Select(item => CreateVector(item, data))
                                 .CrossPairwise((vector1, vector2) => ComputeDeviations(vector1, vector2))
                                 .ToMatrix(t => t.Item1, t => t.Item2, t => t.Item3);

            throw new NotImplementedException();
        }

        public SimilarityResult Predict(Item target, int maxNumberInResult)
        {
            if (!IsTrained)
                throw new InvalidOperationException("Cannot Predict on untrained data");

            throw new NotImplementedException();
        }

        public double Predict(User user, Item item)
        {
            
        }

        public bool IsTrained { get; private set; }

        private NamedVector<NamedValue> CreateVector(Item item, IUserItemRelation data)
        {
            var associated = data.Association
                                 .Where(jr => item.Name.Equals(jr.Second.Name));

            return new NamedVector<NamedValue>(item.Name,
                                               associated.Select(jr => new NamedValue(jr.First.Name, jr.Value))
                                                         .ToList());
        }

        private Tuple<string, string, double> ComputeDeviations(NamedVector<NamedValue> vector1, NamedVector<NamedValue> vector2)
        {
            var sharedUsers = vector1.Values.Join(vector2.Values,
                                                  nv => nv.Name,
                                                  nv => nv.Name,
                                                  (first, second) =>
                                                  new
                                                  {
                                                      First = first.Value,
                                                      Second = second.Value
                                                  })
                                     .ToList();

            var deviation = sharedUsers.Sum(obj => (obj.First - obj.Second)/sharedUsers.Count);

            return Tuple.Create(vector1.Name, vector2.Name, deviation);
        }
    }
}
