using System;
using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.Algorithms.UserBasedSimilarity;
using DataMining.Learning.DataObjects;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Recommendation
{
    public class UserSimilarityEngine : ISimilarityEngine<User>
    {
        private readonly ICorrelationAlgorithm _algorithm;
        private CorrelationLookup _correlationLookup;
        private Dictionary<string, BundledItem<User, Item>> _bundledItems;

        public UserSimilarityEngine(ICorrelationAlgorithm algorithm)
        {
            if (algorithm == null) 
                throw new ArgumentNullException("algorithm");
            
            _algorithm = algorithm;
        }

        public void Train(IUserItemRelation data)
        {
            _correlationLookup = ConvertToVectors(data)
                                .CrossPairwise((vector1, vector2) => _algorithm.ComputeCorrelation(vector1, vector2))
                                .ToCorrelationLookup();

            _bundledItems = data.Primary.Select(u => CreateBundledItem(u, data.Association))
                                        .ToDictionary(b => b.Item.Name);

            IsTrained = true;
        }

        public SimilarityResult Predict(User user, int maxNumberInResult)
        {
            if(!IsTrained)
                throw new InvalidOperationException("Cannot Predict on untrained data");

            if (!_bundledItems.ContainsKey(user.Name))
                return SimilarityResult.Empty;

            var mostSimilarUser = _correlationLookup.GetOrderedBySimilarity(user.Name).First();

            return new SimilarityResult(_bundledItems[mostSimilarUser].BundledItems.Take(maxNumberInResult).ToList());
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
            return new BundledItem<User, Item>(user, junction.GetRecords(user).Select(j => j.Second).ToList());
        }
    }
}
