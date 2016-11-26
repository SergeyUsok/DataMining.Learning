using System;
using System.Collections.Generic;
using DataMining.Learning.DataObjects;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;
using DataMining.Learning.Interfaces;

namespace DataMining.Learning.Recommendation
{
    // todo maybe add to IRelationSchema FirstKey Func<> and SecondKey Func<>, and make SimilarityEngine more generic
    // TODO Unit tests; Slope One; Fill the Github page
    public class Recommender<TBase>
    {
        private readonly ISimilarityEngine<TBase> _engine;

        public Recommender(ISimilarityEngine<TBase> engine)
        {
            if (engine == null) 
                throw new ArgumentNullException("engine");

            _engine = engine;
        }

        public IEnumerable<Item> Recommend()
        {
            throw new NotImplementedException();
        }
    }
}
