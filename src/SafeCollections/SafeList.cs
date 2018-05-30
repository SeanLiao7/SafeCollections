using System;
using System.Collections;
using System.Collections.Generic;

namespace SafeCollections
{
    public sealed class SafeList<T> : IList<T>
    {
        private readonly List<T> _list;
        private readonly object _locker;
        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public SafeList(List<T> list = null, object locker = null)
        {
            _list = list ?? new List<T>();
            _locker = locker ?? new object();
        }

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

        public void AddRange(IEnumerable<T> collection)
        {
            lock (_locker)
                _list.AddRange(collection);
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

        public bool Exists(Predicate<T> match)
        {
            lock (_locker)
                return _list.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            lock (_locker)
                return _list.Find(match);
        }

        public SafeList<T> FindAll(Predicate<T> match)
        {
            lock (_locker)
                return _list.FindAll(match).ToSafeList();
        }

        public void ForEach(Action<T> action)
        {
            lock (_locker)
                _list.ForEach(action);
        }

        public IEnumerator<T> GetEnumerator() => new SafeEnumerator<T>(_list, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

        public int RemoveAll(Predicate<T> match)
        {
            lock (_locker)
            {
                return _list.RemoveAll(match);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_locker)
                _list.RemoveAt(index);
        }
    }
}