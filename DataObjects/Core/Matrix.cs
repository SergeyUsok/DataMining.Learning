using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning.DataObjects.Core
{
    public class Matrix<T>
    {
        private readonly Dictionary<string, int> _rowIndicies = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _columnIndicies = new Dictionary<string, int>();
        private readonly Dictionary<int, T> _content = new Dictionary<int, T>();

        public T this[string row, string column]
        {
            get
            {
                var rowIndex = _rowIndicies[row];
                var columnIndex = _columnIndicies[column];

                return _content[GetKey(rowIndex, columnIndex)];
            }
            set
            {
                if (!_rowIndicies.ContainsKey(row))
                    _rowIndicies[row] = _rowIndicies.Count;

                if(!_columnIndicies.ContainsKey(column))
                    _columnIndicies[column] = _columnIndicies.Count;

                if (!_columnIndicies.ContainsKey(row))
                    _columnIndicies[row] = _columnIndicies.Count;

                if (!_rowIndicies.ContainsKey(column))
                    _rowIndicies[column] = _rowIndicies.Count;

                // make mirror matrix
                _content[GetKey(_rowIndicies[row], _columnIndicies[column])] = value;
                _content[GetKey(_columnIndicies[column], _rowIndicies[row])] = value;
            }
        }

        public IEnumerable<KeyValuePair<string, T>> GetSlice(string name)
        {
            var rowIndex = _rowIndicies[name];

            return _columnIndicies.Where(kv => kv.Key != name)
                                  .Select(kv => new KeyValuePair<string, T>(kv.Key, _content[GetKey(rowIndex, kv.Value)]));
        }

        // Using Cantor pairing function for generating unigue single int from pair of ints
        private int GetKey(int first, int second)
        {
            return ((first + second) / 2) * (first + second + 1) + second;
        }
    }
}
