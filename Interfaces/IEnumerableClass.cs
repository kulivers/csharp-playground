using System;
using System.Collections;

namespace Interfaces
{
    public class Person
    {
        public Person(string fName, string lName)
        {
            firstName = fName;
            lastName = lName;
        }

        public string firstName;
        public string lastName;
    }

    // Collection of Person objects. This class
    // implements IEnumerable so that it can be used
    // with ForEach syntax.
    public class People : IEnumerable
    {
        private Person[] _people;

        public People(Person[] pArray)
        {
            _people = new Person[pArray.Length];

            for (var i = 0; i < pArray.Length; i++) _people[i] = pArray[i];
        }

        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _people.GetEnumerator();
        }

        public PeopleEnumerator GetEnumerator()
        {
            return new PeopleEnumerator(_people);
        }
    }

    // When you implement IEnumerable, you must also implement IEnumerator.
    public class PeopleEnumerator : IEnumerator
    {
        public Person[] _people;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private int position = -1;

        public PeopleEnumerator(Person[] list)
        {
            _people = list;
        }

        public bool MoveNext()
        {
            position++;
            return position < _people.Length;
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public Person Current
        {
            get
            {
                try
                {
                    return _people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}