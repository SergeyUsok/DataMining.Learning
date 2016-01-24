namespace DataMining.Learning
{
    public interface  IAlgorithm<TInput, TModel>
        where TInput : IInputData
        where TModel : Model
    {
        TModel Build(TInput input);
    }
}