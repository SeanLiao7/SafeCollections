using System;
using System.Collections;
using System.Collections.Generic;

namespace SafeCollections
{
    public sealed class SafeList<T> : IList<T>
    {
        private readonly List<T> _list = new List<T>();
        private readonly object _locker = new object();

        public int Count
        {
            get
            {
                lock (_locker)
                    return _list.Count;
            }
        }

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                lock (_locker)
                    return _list[index];
            }
            set
            {
                lock (_locker)
                    _list[index] = value;
            }
        }

        public void Add(T item)
        {
            lock (_locker)
                _list.Add(item);
        }

        public void Clear()
        {
            lock (_locker)
                _list.Clear();
        }

        public bool Contains(T item)
        {
            lock (_locker)
                return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_locker)
                _list.CopyTo(array, arrayIndex);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new SafeEnumerator<T>(_list, _locker);

        public IEnumerator GetEnumerator() => new SafeEnumerator<T>(_list, _locker);

        public int IndexOf(T item)
        {
            lock (_locker)
                return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            lock (_locker)
                _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            lock (_locker)
                return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            lock (_locker)
                _list.RemoveAt(index);
        }

        public int RemoveAll(Predicate<T> match)
        {
            lock (_locker)
            {
                return _list.RemoveAll(match);
            }
        }
    }
}
