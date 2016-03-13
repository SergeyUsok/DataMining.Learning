using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Learning.Interfaces;

namespace DataMining.Learning.DataObjects.Inputs
{
    public interface IRelationSchema<TFirst, TSecond>
    {
        IEnumerable<TFirst> Primary { get; }

        IEnumerable<TSecond> Secondary { get; }

        Junction<TFirst, TSecond> Association { get; }
    }
}
