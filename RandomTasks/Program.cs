using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RandomTasks
{
    public static class VarExtention
    {
        public static IEnumerable<T> Yeald<T>(this T variable)
        {
            if (variable == null)
                yield break;
            yield return variable;
        }
    }

    class Car: IEquatable<Car>
    {
        
        public double Power { get; set; } = 5;
        public string Brand { get; set; } = "Kia";

        public bool Equals(Car other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Power.Equals(other.Power) && Brand == other.Brand;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Car)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Power, Brand);
        }
    }

    enum Priority
    {
        VeryLow,
        Low,
        Middle,
        High,
        VeryHigh
    }


    class Work : IStructuralEquatable 
    {
        public int Difficulty ;
        public int Mark ;

        public Work(int mark, int difficulty)
        {
            this.Mark = mark;
            this.Difficulty = difficulty;
        }

        public bool Equals(object? other, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var work1 = new Work(5, 1);
            var work2 = new Work(5, 1);
            var work3 = new Work(5, 3);
            Console.WriteLine(work1.Equals(work2));
            Console.WriteLine(work1.Equals(work3));
            Tuple
        }

        static void CompareCars()
        {
            var car1 = new Car() { Brand = "Nissan" };
            var car2 = new Car() { Brand = "Wols" };
            var car3 = new Car() { Power = 1.6 };
            var car4 = new Car() { Power = 1.6 };
            var car5 = new Car() { Brand = "Skoda" };

            Dictionary<Car, Priority> carPriorities = new Dictionary<Car, Priority>();
            carPriorities.Add(car1, Priority.VeryLow);
            carPriorities.Add(car2, Priority.Low);
            carPriorities.Add(car3, Priority.Middle);
            carPriorities.Add(car4, Priority.High);
            carPriorities.Add(car5, Priority.VeryHigh);

            Dictionary<Car, Priority>.KeyCollection keys = carPriorities.Keys;
            bool isEq = keys.ElementAt(2).Equals(keys.ElementAt(3));
            Console.WriteLine(isEq);
        }

        static void CompareCities()
        {
            //=-------
            var cities = new Dictionary<string, string>(){
                {"UK", "Chelsea"},
                {"UK", "London, Manchester, Birmingham"},
                {"USA", "Chicago, New York, Washington"},
                {"India", "Mumbai, New Delhi, Pune"}
            };
            foreach (var keyValuePair in cities)
            {
                Console.WriteLine(keyValuePair);
            }
        }
    }
}