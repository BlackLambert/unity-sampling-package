using System.Collections.Generic;

namespace PCGToolkit.Sampling
{
    public interface Selector<TItem>
    {
        public void Init(ICollection<TItem> items);
        public bool HasNext();
        public TItem GetNext();
    }
}