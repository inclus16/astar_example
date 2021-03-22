using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Dto
{
    public class QueuePriorityItem<T>
    {
        private readonly T item;

        private readonly double priority;

        public T Item { get => item; }

        public double Priority { get => priority; }

        public QueuePriorityItem(T item, double priority)
        {
            this.item = item;
            this.priority = priority;
        }
    }
}
