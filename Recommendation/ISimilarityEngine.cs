using DataMining.Learning.DataObjects;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Recommendation
{
    public interface ISimilarityEngine<TBase>
    {
        void Train(IUserItemRelation data);

        SimilarityResult Predict(TBase target, int maxNumberInResult);

        bool IsTrained { get; }
    }
}
