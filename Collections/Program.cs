using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

[assembly: InternalsVisibleTo("Interfaces")]

namespace Collections
{
    class Item
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


    class IntCollection : ICollection<int>
    {
        int[] _collection = { 11, 12, 13, 14, 15 };
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

        public void CopyTo(int[] array, int arrayIndex)
        {
            //array - куда, arrayIndex - в array, какое место
            
            if (array.Length < _collection.Length + arrayIndex)
            {
                throw new IndexOutOfRangeException();
            }
            
            Array.Copy(_collection, 0, array, arrayIndex, _collection.Length);
        }

        public int Count => _collection.Length;

        public bool IsReadOnly => true;
    }

    class AddRangeToListExample
    {
        //void AddRange(ICollection col)
        static public void LocalMain()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            IntCollection intCollection = new IntCollection(); //{ 11, 12, 13, 14, 15 };
            list.AddRange(intCollection);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("?////////////////");
            Span<int> span = new int[] { 1, 2, 122, 13, 12, 31, 13 };
            Index index = ^1;
            Range range = index..1;
            var sub =span.ToArray()[range];
            foreach (var i in sub)
            {
                Console.WriteLine(i);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AddRangeToListExample.LocalMain();
        }
    }
}