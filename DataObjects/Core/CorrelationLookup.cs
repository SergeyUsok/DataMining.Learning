using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.Algorithms.Similarity;

namespace DataMining.Learning.DataObjects.Core
{
    internal class CorrelationLookup
    {
        private readonly Dictionary<string, List<Correlation>> _correlationsLookup;

        private CorrelationLookup(Dictionary<string, List<Correlation>> correlationsLookup)
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
                                 .ToDictionary(group => group.Key, group => group.OrderByDescending(corr => corr).ToList());

            return new CorrelationLookup(lookup);
        }

        public IReadOnlyCollection<Correlation> GetOrderedCorrelations(string key)
        {
            return _correlationsLookup[key].AsReadOnly();
        }
    }
}
