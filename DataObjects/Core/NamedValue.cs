using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning.DataObjects.Core
{
    public class NamedValue
    {
        public NamedValue(string name, double value)
        {
            if (name == null) 
                throw new ArgumentNullException("name");

            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public double Value { get; private set; }
    }
}
