namespace DataMining.Learning
{
    public interface IInputDataProvider<TInputData>
        where TInputData : IInputData
    {
        TInputData GetInputData();
    }
}