using System.Collections;
using System.Collections.Generic;

namespace Library.DataStructures
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        private T[] internalArray;
        private const int defaultCapacity = 4;

        public int Count { get; private set; }

        public int Capacity => internalArray.Length;

        public DynamicArray()
            :this(defaultCapacity) { }

        public DynamicArray(int capacity)
        {
            internalArray = new T[capacity];
        }

        public void Add(T item)
        {
            if(Count >= Capacity) {
                ResizeArray(Capacity * 2);
            }

            internalArray[Count] = item;
            Count++;
        }

        public void Remove(T item)
        {
            var index = IndexOf(item);

            if (index >= 0)
            {
                internalArray[index] = default;
                Count--;
                return;
            }
        }

        public int IndexOf(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < Count; i++)
            {
                if (comparer.Equals(internalArray[i])) {
                    return i;
                }
            }

            return -1;
        }

        public T First() => internalArray[0];

        public T Last() => internalArray[Count];

        public T this[int index] => internalArray[index];

        public bool Contains(T item) =>
            IndexOf(item) >= 0 ? true : false;

        private void ResizeArray(int arraySize)
        {
            var tempArray = internalArray;
            internalArray = new T[arraySize];
            tempArray.CopyTo(internalArray, 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++) {
                yield return internalArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
