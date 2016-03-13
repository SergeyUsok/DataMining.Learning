using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Learning.Exceptions;

namespace DataMining.Learning.DataObjects.Core
{
    public sealed class SourceEntry
    {
        private readonly IDictionary<string, string> _source;

        public SourceEntry(IDictionary<string, string> source)
        {
            if (source == null) 
                throw new ArgumentNullException("source");
            
            _source = source;
        }

        public SourceEntry(IEnumerable<string> keys, IEnumerable<string> values)
        {
            if (keys == null) throw new ArgumentNullException("keys");
            if (values == null) throw new ArgumentNullException("values");

            _source = keys.ToDictionary(values);
        }

        public TResult Property<TResult>(string name)
        {
            var stringProperty = Property(name);

            return (TResult) TypeDescriptor.GetConverter(typeof (TResult)).ConvertFromString(stringProperty);
        }

        public string Property(string name)
        {
            if (_source.ContainsKey(name))
                throw new PropertyMissingException(name);

            return _source[name];
        }
    }
}
