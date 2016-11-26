using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.Algorithms.UserBasedSimilarity;

namespace DataMining.Learning.DataObjects.Core
{
    internal class CorrelationLookup
    {
        private readonly Dictionary<string, List<string>> _correlationsLookup;

        private CorrelationLookup(Dictionary<string, List<string>> correlationsLookup)
        {
            _correlationsLookup = correlationsLookup;
        }

        public static CorrelationLookup Construct(IEnumerable<Correlation> correlations)
        {
            var lookup = correlations.SelectMany(corr => new[]
                                                    {
                                                        new {Key = corr.First, Correlation = corr},
                                                        new {Key = corr.Second, Correlation = corr}
                                                    })
                                 .GroupBy(item => item.Key, item => item.Correlation)
                                 .ToDictionary(group => group.Key, group => group.OrderByDescending(corr => corr)
                                                                                 .Select(cor => group.Key == cor.First
                                                                                                    ? cor.Second
                                                                                                    : cor.First)
                                                                                 .ToList());

            return new CorrelationLookup(lookup);
        }

        public IReadOnlyCollection<string> GetOrderedBySimilarity(string key)
        {
            return _correlationsLookup[key].AsReadOnly();
        }
    }
}
