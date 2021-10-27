using System;

namespace AddictionalFeatures
{
    public class Records //Working only on .Net 5
    {
        public record Person
        {
            public string Name { get; init; }
            public int Age { get; init; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            // public void Deconstruct(out string name, out int age)
            // {
            //     name = this.Name;
            //     age = this.Age;
            // }
            //SAME
            public void Deconstruct(out string name, out int age)
            {
                (name, age) = (Name, Age);
            }
        }

        // ------ That all can be:
        public record Person2(string name, int age);

        public static void LocalMain()
        {
            Person ega = new("Ega", 22);
            Person ega2 = new("Ega", 22);
            Person youngEga = ega2 with { Age = 14 };
            Console.WriteLine(ega == ega2); //True
            Console.WriteLine(ega.Equals(ega2));
            //--------------------------
            var (name, age) = ega;
            Console.WriteLine(name + " - " + age);
            //----------------------------
            Person2 leha = new("Lexa", 1000);
            Person2 leha2 = leha;
            Console.WriteLine(leha == leha2);
            var (name2, age2) = leha;
            Console.WriteLine(name2 + " - " + age2);
        }
    }
}