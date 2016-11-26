using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning.DataObjects
{
    public interface IPredictedResult<T>
    {
        T GetResult();
    }
}
