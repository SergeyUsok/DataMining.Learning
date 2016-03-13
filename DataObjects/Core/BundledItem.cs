using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Learning.DataObjects.Core
{
    public class BundledItem<TItem, TBundled>
    {
        public BundledItem(TItem item, IEnumerable<TBundled> bundledItems)
        {
            Item = item;
            BundledItems = bundledItems;
        }

        public TItem Item { get; private set; }

        public IEnumerable<TBundled> BundledItems { get; private set; }
    }
}
