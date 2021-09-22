using System;

namespace AddictionalFeatures
{
    public class Student
    {
        public string Name { get; set; } = "Tom";
        public int Age { get; set; } = 15;
    }
    
    public class AnonymusTypes
    {
        public static void LocalMain()
        {
            var user = new { Name = "Tom", Age = 34 };
            var student = new { Name = "Alice", Age = 21 };
            var manager = new { Name = "Bob", Age = 26, Company = "Microsoft" };
            
            Console.WriteLine(user.GetType().Name); // <>f__AnonymousType0'2
            Console.WriteLine(student.GetType().Name); // <>f__AnonymousType0'2
            Console.WriteLine(manager.GetType().Name); // <>f__AnonymousType1'3
            
            Console.WriteLine(student.GetType().Name); // <>f__AnonymousType0'2
            var student2 = new Student();
            // student = (Student)student2;//ERROR
            
            
            //we also can make arrays:
            var people = new[]
            {
                new {Name="Tom"},
                new {Name="Bob"}
            };

        }
    }
}