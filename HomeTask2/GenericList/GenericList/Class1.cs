using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GenericList
{
    internal class GenList<T> : IDisposable, IEnumerable<T>, IEnumerator<T>
    {
        private T[] items = new T[]
        {
        };

        private int position = -1;

        public T Current
        {
            get
            {
                return items[position];
            }
        }

        object IEnumerator.Current 
        {
            get
            {
                return items[position];
            }
        }

        public void Dispose()
        {
           position = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            position++;
            return position < items.Length;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }


        public void Add(T item) 
        {
            T[] newItems = new T[items.Length+1];

            for (int i = 0; i < items.Length; i++)
            {
                newItems[i] = items[i];
            }

            newItems[items.Length] = item;

            items = newItems;
        }

        public T Remove(T item)
        {
            T[] newItems = new T[items.Length - 1];

            int newItemsIndex = 0;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(item))
                {
                    continue;
                }

                newItems[newItemsIndex] = items[i];
                newItemsIndex++;
            }

            items = newItems;

            return item;
        }

        public T RemoveAt(int removeIndex) 
        {
            if (removeIndex > items.Length - 1 || removeIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(removeIndex), "Invalid index of element for removing");
            }
            
            T[] newItems = new T[items.Length - 1];

            int newItemsIndex = 0;

            for (int i = 0; i < items.Length; i++)
            {
                if (i == removeIndex)
                {
                    continue;
                }

                newItems[newItemsIndex] = items[i];
                newItemsIndex++;
            }

            T itemForReturn = items[removeIndex];

            items = newItems;

            return itemForReturn;
        }

        public void RemoveAll()
        {
            T[] newItems = new T[] 
            {
            };

            items = newItems;
        }

        public T Find(Predicate<T> predicate) 
        {
            foreach (T presentItem in items)
            {
                if (predicate(presentItem))
                {
                    return presentItem;
                }

            }

            return default;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for(int i = 0; i < items.Length; i++)
            {
                if (predicate(items[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T newItem)
        {
            if (index > items.Length || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index of element for removing");
            }

            T[] newItems = new T[items.Length + 1];

            int newItemsIndex = 0;

            for (int i = 0; i < items.Length; i++)
            {
                if (i == index)
                {
                    newItems[newItemsIndex] = newItem;
                    continue;
                }

                newItems[newItemsIndex] = items[i];
                newItemsIndex++;
            }

            if (index == items.Length)
            {
                newItems[newItemsIndex] = newItem;
            }

            items = newItems;
        }
    }
}
