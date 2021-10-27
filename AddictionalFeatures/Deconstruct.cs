using System;

namespace AddictionalFeatures
{
    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void Deconstruct(out string name, out int age)
        {
            name = Name;
            age = Age;
        }
    }

    public class DeconstructClasses
    {
        public static void Localmain()
        {
            Person person = new() { Name = "Tom", Age = 33 };
            (string name, var age) = person;
            Console.WriteLine(name); // Tom
            Console.WriteLine(age); // 33
        }
    }
}