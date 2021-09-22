using System;

namespace inheritance
{
    public class Person
    {
        public string Name { get; set; }

        public virtual void VirtualDisplay()
        {
            Console.WriteLine($"Person {Name}");
        }

        public void NotVirtualDisplay()
        {
            Console.WriteLine($"Person {Name}.");
        }
    }

    class Employee : Person
    {
        public string Company { get; set; }


        public void SomeEmployeeMethod()
        {
        }

        public override void VirtualDisplay()
        {
            Console.WriteLine($"Employee {Name}");
        }

        public void NotVirtualDisplay()
        {
            Console.WriteLine($"Employee {Name}.");
        }
    }

    static class Program
    {
        public static void Main()
        {
            Person person = new Employee { Name = "Sam", Company = "Microsoft" };
            // person.SomeEmployeeMethod(); //ERROR я понимаю почему 
            person.VirtualDisplay(); //”Employee Sam”
            person.NotVirtualDisplay(); //”Person Sam.”
            Console.ReadKey();
        }
    }
}