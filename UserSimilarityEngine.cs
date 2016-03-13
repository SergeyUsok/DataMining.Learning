using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Learning.Algorithms.Similarity;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning
{
    public class UserSimilarityEngine : ISimilarityEngine<User,Item>
    {
        private readonly ICorrelationAlgorithm _algorithm;
        private CorrelationLookup _correlationLookup;
        private IEnumerable<BundledItem<User, Item>> _bundledItems;

        public UserSimilarityEngine(ICorrelationAlgorithm algorithm)
        {
            if (algorithm == null) 
                throw new ArgumentNullException("algorithm");
            
            _algorithm = algorithm;
        }

        public void Train(IRelationSchema<User, Item> data)
        {
            _correlationLookup = ConvertToVectors(data)
                                .CrossPairwise((vector1, vector2) => _algorithm.ComputeCorrelation(vector1, vector2))
                                .ToCorrelationLookup();

            _bundledItems = data.Primary.Select(u => CreateBundledItem(u, data.Association)); 
                
            IsTrained = true;
        }

        public void Predict()
        {
            if(!IsTrained)
                throw new InvalidOperationException("Cannot Predict on untrained data");

            throw new NotImplementedException();
        }

        public bool IsTrained { get; private set; }

        private IEnumerable<NamedVector<NamedValue>> ConvertToVectors(IRelationSchema<User, Item> data)
        {
            var list = new List<NamedVector<NamedValue>>();

            foreach (var user in data.Primary)
            {
                var namedValues = data.Association.GetRecords(user)
                                                  .Select(j => new NamedValue(j.Second.Name, j.Value))
                                                  .ToList();

                list.Add(new NamedVector<NamedValue>(user.Name, namedValues));
            }

            return list;
        }

        private BundledItem<User, Item> CreateBundledItem(User user, Junction<User, Item> junction)
        {
            return new BundledItem<User, Item>(user, junction.GetRecords(user).Select(j => j.Second));
        }
    }
}
