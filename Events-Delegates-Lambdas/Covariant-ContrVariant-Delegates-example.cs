using System;

namespace CovariantContrVariantDelegates
{
    internal class Program
    {
        public static void Local_Main()
        {
            CoVariant.Do();
        }

        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static void DisplayRedMessage(string message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }
    }

    internal class CoVariant
    {
        private class Person
        {
            public string Name { get; set; }
        };

        private class Client : Person
        {
            public int money = 500;
        };

        private delegate Person PersonFactory(string name);

        public static void Do()
        {
            PersonFactory personDel;
            personDel = BuildClient; // ковариантность
            var p = personDel("Tom"); //personDel("Tom") = Client
            Console.WriteLine(p.Name);
            // Client p = personDel("Tom"); error, cant Person to Client
            //Мы не можем здесь писать Client p потому что хоть он и возращает Client, Но изза того что делегат Person? 
        }

        private static Client BuildClient(string name)
        {
            return new Client { Name = name };
        }
    }
}