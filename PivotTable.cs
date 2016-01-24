using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning
{
    public class PivotTable<T>
    {
        private readonly Dictionary<string, int> _rowIndices;
        private readonly Dictionary<string, int> _columnIndices;
        private readonly T[,] _content;

        public PivotTable(IEnumerable<string> rowHeaders, IEnumerable<string> columnHeaders)
        {
            
        }

        public T this[string row, string column]
        {
            get { return _content[_rowIndices[row], _columnIndices[column]]; }
            set { _content[_rowIndices[row], _columnIndices[column]] = value; }
        }
    }
}
