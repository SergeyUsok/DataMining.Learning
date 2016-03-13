using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataMining.Learning.DataObjects.Inputs
{
    public class Junction<TFirst, TSecond> : IEnumerable<JunctionRecord<TFirst, TSecond>>
    {
        private readonly Func<TFirst, string> _keySelector;
        private readonly Dictionary<string, IList<JunctionRecord<TFirst, TSecond>>> _records = new Dictionary<string, IList<JunctionRecord<TFirst, TSecond>>>();

        public Junction(Func<TFirst, string> keySelector)
        {
            _keySelector = keySelector;
        }

        public IEnumerable<JunctionRecord<TFirst, TSecond>> GetRecords(TFirst primary)
        {
            var key = _keySelector(primary);

            if (!_records.ContainsKey(key))
                return Enumerable.Empty<JunctionRecord<TFirst, TSecond>>();

            return _records[key];
        }

        public void Add(JunctionRecord<TFirst, TSecond> record)
        {
            var key = _keySelector(record.First);

            if (!_records.ContainsKey(key))
                _records[key] = new List<JunctionRecord<TFirst, TSecond>>();

            _records[key].Add(record);
        }

        public IEnumerator<JunctionRecord<TFirst, TSecond>> GetEnumerator()
        {
            return _records.Values.SelectMany(en => en).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
