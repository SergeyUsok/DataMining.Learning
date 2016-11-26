using System.Collections.Generic;
using System.Linq;
using DataMining.Learning.Algorithms.Normalization;
using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.Mathematics;

namespace DataMining.Learning.Algorithms.Predictors
{
    public class AssessmentPredictor
    {
        private readonly Matrix<double> _matrix;
        private readonly Normalizer _normalizer;

        public AssessmentPredictor(Matrix<double> matrix, Normalizer normalizer)
        {
            _matrix = matrix;
            _normalizer = normalizer;
        }

        public double Predict(string itemName, Dictionary<string, double> userAssessments)
        {
            if (userAssessments.ContainsKey(itemName))
                return userAssessments[itemName];

            var pairs = _matrix.GetSlice(itemName)
                                               .Select(
                                                   keyedValue =>
                                                   new
                                                       {
                                                           Similarity = keyedValue.Value,
                                                           Rating = _normalizer.Normalize(userAssessments[keyedValue.Key])
                                                       })
                                               .ToList();

            var normalizedResult = pairs.Sum(pair => pair.Similarity*pair.Rating)/
                                   pairs.Sum(p => p.Similarity.ToAbsolute());

            return _normalizer.Denormalize(normalizedResult);
        }
    }
}
