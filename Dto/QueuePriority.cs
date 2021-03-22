using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Dto
{
    public class QueuePriority<T>
    {
        private List<QueuePriorityItem<T>> items;

        public QueuePriority()
        {
            items = new List<QueuePriorityItem<T>>();
        }

        public void Push(T item, double priority)
        {
            items.Add(new QueuePriorityItem<T>(item, priority));
        }

        public bool HasItem(T item)
        {
            return items.Select(x=>x.Item).Contains(item);
        }

        public T Take()
        {
            int index = items.FindIndex(0,x => x.Priority == items.Max(q => q.Priority));
            T item = items[index].Item;
            items.RemoveAt(index);
            return item;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }
    }
}
