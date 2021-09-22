using System;

namespace AddictionalFeatures
{
    class Employee
    {
        public virtual void Work()
        {
            Console.WriteLine("Да работаю я, работаю");
        }
    }

    class Manager : Employee
    {
        public override void Work()
        {
            Console.WriteLine("Отлично, работаем дальше");
        }

        public bool IsOnVacation { get; set; }
    }

    public class PatternMatching
    {
        static void UseEmployee1(Employee emp)
        {
            Manager manager = emp as Manager;
            if (manager != null && manager.IsOnVacation == false)
            {
                manager.Work();
            }
            else
            {
                Console.WriteLine("Преобразование прошло неудачно");
            }
        }

        static void UseEmployee2(Employee emp)
        {
            try
            {
                Manager manager = (Manager)emp;
                if (!manager.IsOnVacation)
                    manager.Work();
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void UseEmployee3(Employee emp)
        {
            if (emp is Manager)
            {
                Manager manager = (Manager)emp;
                if (!manager.IsOnVacation)
                    manager.Work();
            }
            else
            {
                Console.WriteLine("Преобразование не допустимо");
            }
        }

        static void PatternMatchingUseEmployee(Employee emp)
        {
            if (emp is Manager manager && manager.IsOnVacation == false)
            {
                manager.Work();
            }
            else
            {
                Console.WriteLine("Преобразование не допустимо");
            }
        }

        static void PatternMatchingSwitch(Employee emp)
        {
            switch (emp)
            {
                case Manager manager:
                    manager.Work();
                    break;
                case null:
                    Console.WriteLine("Объект не задан");
                    break;
                default:
                    Console.WriteLine("Объект не менеджер");
                    break;
            }
        }

        public static void LocalMain()
        {
            Employee emp = new Manager(); //Employee();
            PatternMatchingSwitch(emp);
            Console.Read();
        }
    }
}