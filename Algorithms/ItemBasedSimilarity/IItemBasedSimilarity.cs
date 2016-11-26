using DataMining.Learning.DataObjects.Core;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Algorithms.ItemBasedSimilarity
{
    public interface IItemBasedSimilarity
    {
        Matrix<double> BuildSimilarityMatrix(IUserItemRelation data);
    }
}