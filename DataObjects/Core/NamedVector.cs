using System.Collections.Generic;

namespace DataMining.Learning.DataObjects.Core
{
    public class NamedVector<TValue>
    {
        public NamedVector(string name, IEnumerable<TValue> values)
        {
            Name = name;
            Values = values;
        }

        public string Name { get; private set; }

        public IEnumerable<TValue> Values { get; private set; }
    }
}