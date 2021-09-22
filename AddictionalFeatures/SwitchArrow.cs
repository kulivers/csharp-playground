using System;

namespace AddictionalFeatures
{
    public class SwitchArrow
    {
        public string Name { get; set; } // имя пользователя
        public string Status { get; set; } // статус пользователя
        public string Language { get; set; } // язык пользователя

        static string GetMessage(SwitchArrow p) => p switch
        {
            { Language: "english" } => "Hello!",
            { Language: "german", Status: "admin" } => "Hallo, admin!",
            { Language: "french" } => "Salut!",
            { } => "undefined"
        };

        int SimpleSelect(int op, int a, int b)
        {
            switch (op)
            {
                case 1: return a + b;
                case 2: return a - b;
                case 3: return a * b;
                default: throw new ArgumentException("Недопустимый код операции");
            }
        }

        static int ArrowSelect(int op, int a, int b)
        {
            int result = op switch
            {
                1 => a + b,
                2 => a - b,
                3 => a * b,
                _ => throw new ArgumentException("Недопустимый код операции")
            };
            return result;
        }

        static string GetWelcome(string lang, string daytime) => (lang, daytime) switch
        {
            ("english", "morning") => "Good morning",
            ("english", "evening") => "Good evening",
            ("german", "morning") => "Guten Morgen",
            ("german", "evening") => "Guten Abend",
            _ => "Здрасьть"
        };

        static decimal LogicalRelationalSwitch(decimal sum)
        {
            return sum switch
            {
                <= 0 => 0, // если sum меньше или равно 0, возвращаем 0
                < 50000 => sum * 0.05m, // если sum меньше 50000, возвращаем sum * 0.05m
                < 100000 => sum * 0.1m, // если sum меньше 100000, возвращаем sum * 0.1m
                _ => sum * 0.2m // в остальных случаях возвращаем sum * 0.2m
            };
        }

        public static void LocalMain()
        {
            
            Console.WriteLine("just check methods in class");
        }
    }
}