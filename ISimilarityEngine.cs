using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Learning.DataObjects.Inputs;

namespace DataMining.Learning
{
    public interface ISimilarityEngine<TFirst, TSecond>
    {
        void Train(IRelationSchema<TFirst, TSecond> data);

        void Predict();

        bool IsTrained { get; }
    }
}
