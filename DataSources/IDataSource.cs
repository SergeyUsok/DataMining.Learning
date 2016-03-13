using System.Collections.Generic;
using DataMining.Learning.DataObjects.Core;

namespace DataMining.Learning.DataSources
{
    public interface IDataSource
    {
        IEnumerable<SourceEntry> GetSourceEntries();
    }
}
