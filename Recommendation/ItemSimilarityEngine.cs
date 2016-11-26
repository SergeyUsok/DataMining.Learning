using System;
using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.Algorithms.ItemBasedSimilarity;
using DataMining.Learning.DataObjects;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Recommendation
{
    public class ItemSimilarityEngine : ISimilarityEngine<Item>
    {
        private readonly IItemBasedSimilarity _algorithm;
        private Matrix<double> _similarityMatrix;
        private Dictionary<string, Item> _items;

        public ItemSimilarityEngine(IItemBasedSimilarity algorithm)
        {
            if (algorithm == null) 
                throw new ArgumentNullException("algorithm");

            _algorithm = algorithm;
        }

        public void Train(IUserItemRelation data)
        {
            _items = data.Secondary.ToDictionary(i => i.Name);
            _similarityMatrix = _algorithm.BuildSimilarityMatrix(data);
            IsTrained = true;
        }

        public SimilarityResult Predict(Item target, int maxNumberInResult)
        {
            if(!IsTrained)
                throw new InvalidOperationException("Engine must be trained first");

            var similarItems = _similarityMatrix.GetSlice(target.Name)
                                      .Take(maxNumberInResult)
                                      .OrderByDescending(kv => kv.Value)
                                      .Select(n => _items[n.Key])
                                      .ToList();

            return new SimilarityResult(similarItems);
        }

        public bool IsTrained { get; private set; }
    }
}