using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Interfaces")]

namespace Collections
{
    internal class Item
    {
        private static int _id;
        public int Id { get; }
        public int Value { get; }

        public Item(int value)
        {
            Value = value;
            Id = _id;
            _id++;
        }
    }


    internal class IntCollection : ICollection<int>
    {
        private int[] _collection = { 11, 12, 13, 14, 15 };
        private int _position = -1;

        public IEnumerator<int> GetEnumerator()
        {
            yield return _collection[++_position];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(int item)
        {
            Array.Resize(ref _collection, _collection.Length + 1);
            _collection[_collection.Length] = item;
        }

        public void Clear()
        {
            _collection = new int[] { };
        }

        public bool Contains(int item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            //array - куда, arrayIndex - в array, какое место

            if (array.Length < _collection.Length + arrayIndex) throw new IndexOutOfRangeException();

            Array.Copy(_collection, 0, array, arrayIndex, _collection.Length);
        }

        public bool Remove(int item)
        {
            try
            {
                _collection = _collection.Where(val => val != item).ToArray();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Count => _collection.Length;

        public bool IsReadOnly => true;
    }

    internal class AddRangeToListExample
    {
        //void AddRange(ICollection col)
        public static void LocalMain()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 };
            var intCollection = new IntCollection(); //{ 11, 12, 13, 14, 15 };
            list.AddRange(intCollection);
            foreach (var item in list) Console.WriteLine(item);

            Console.WriteLine("?////////////////");
            Span<int> span = new int[] { 1, 2, 122, 13, 12, 31, 13 };
            var index = ^1;
            var range = index..1;
            var sub = span.ToArray()[range];
            foreach (var i in sub) Console.WriteLine(i);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            AddRangeToListExample.LocalMain();
        }
    }
}