using DataMining.Learning.DataObjects.Core;

namespace DataMining.Learning.Algorithms.Similarity
{
    public interface ICorrelationAlgorithm
    {
        Correlation ComputeCorrelation(NamedVector<NamedValue> vector1, NamedVector<NamedValue> vector2);
    }
}
