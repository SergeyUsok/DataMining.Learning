using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning
{
    public class Recommender<TItem, TInputData>
        where TInputData : IInputData
    {
        private readonly IAlgorithm<TInputData, Model> _algorithm;
        private readonly IInputDataProvider<TInputData> _dataProvider;
        private Model _model;

        public Recommender(IAlgorithm<TInputData, Model> algorithm, IInputDataProvider<TInputData> dataProvider)
        {
            _algorithm = algorithm;
            _dataProvider = dataProvider;
        }

        public TItem Recommend(TItem item)
        {
            if (_model == null)
            {
                var input = _dataProvider.GetInputData();
                _model = _algorithm.Build(input);
            }

            throw new NotImplementedException();
        }
    }
}
