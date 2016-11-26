using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Learning.Algorithms.UserBasedSimilarity;
using DataMining.Learning.DataObjects.Core;

namespace DataMining.Learning
{
    internal static class Extensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            if (keys == null) throw new ArgumentNullException("keys");
            if (values == null) throw new ArgumentNullException("values");

            var keysIterator = keys.GetEnumerator();
            var valuesIterator = values.GetEnumerator();

            var dictionary = new Dictionary<TKey, TValue>();

            using (keysIterator)
            using (valuesIterator)
            {
                while (keysIterator.MoveNext())
                {
                    var secondHasNext = valuesIterator.MoveNext();

                    if (!secondHasNext)
                        throw new ArgumentException("Keys sequence has greater number of elements than the sequence with elements");

                    dictionary.Add(keysIterator.Current, valuesIterator.Current);
                }

                if (valuesIterator.MoveNext())
                    throw new ArgumentException("Values sequence has greater number of elements than the sequence with keys");
            }

            return dictionary;
        }

        public static IEnumerable<TResult> CrossPairwise<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return PairwiseImpl(source, resultSelector);
        }

        internal static Matrix<double> ToMatrix(this IEnumerable<Similarity> similarities)
        {
            var matrix = new Matrix<double>();

            foreach (var similarity in similarities)
            {
                matrix[similarity.First, similarity.Second] = similarity.Value;
            }

            return matrix;
        }

        internal static Matrix<TResult> ToMatrix<TSource, TResult>(this IEnumerable<TSource> source, 
                                                                       Func<TSource, string> rowSelector, 
                                                                       Func<TSource, string> columnSelector,
                                                                       Func<TSource, TResult> resultSelector)
        {
            var matrix = new Matrix<TResult>();

            foreach (var item in source)
            {
                matrix[rowSelector(item), columnSelector(item)] = resultSelector(item);
            }

            return matrix;
        }

        internal static CorrelationLookup ToCorrelationLookup(this IEnumerable<Correlation> correlations)
        {
            return CorrelationLookup.Construct(correlations);
        }

        private static IEnumerable<TResult> PairwiseImpl<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> resultSelector)
        {
            // allows to avoid self-pair like item1-item1
            var skipCount = 1;

            foreach (var item1 in source)
            {
                foreach (var item2 in source.Skip(skipCount))
                {
                    yield return resultSelector(item1, item2);
                }

                ++skipCount;
            }
        }
    }
}
