using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning.Interfaces
{
    public interface IInputDataProvider<TFirst, TSecond>
    {
        IRelationSchema<TFirst, TSecond> GetInputData();
    }
}