using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.DataObjects
{
    public class SimilarityResult : IPredictedResult<IEnumerable<Item>>
    {
        private readonly IEnumerable<Item> _items;

        public SimilarityResult(IEnumerable<Item> items)
        {
            _items = items;
        }

        public IEnumerable<Item> GetResult()
        {
            return _items;
        }

        public static readonly SimilarityResult Empty = new SimilarityResult(Enumerable.Empty<Item>());
    }
}