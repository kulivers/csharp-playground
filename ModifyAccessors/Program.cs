using System;

namespace ModifyAccessors
{
    class NotChildInOtherProject //paste in other project
    {
        AccessClass accessClassObj = new AccessClass();

        void Display()
        {
            accessClassObj.publicMethod();
            Console.WriteLine(accessClassObj.publicVar);
        }
    }
    class ChildInOtherProject : AccessClass //paste in other project
    {
        AccessClass accessClassObj = new AccessClass();

        void Display()
        {
            Console.WriteLine(publicVar);
            Console.WriteLine(protectedInternalVar);
            Console.WriteLine(protectedVar);
            accessClassObj.publicMethod();
        }
    }

    public class NotChildClass
    {
        AccessClass accessClassObj = new AccessClass();

        void Display()
        {
            Console.WriteLine(accessClassObj.internalVar);
            Console.WriteLine(accessClassObj.publicVar);
            Console.WriteLine(accessClassObj.protectedInternalVar);
            accessClassObj.internalMethod();
            accessClassObj.publicMethod();
            accessClassObj.protectedInternalMethod();
        }        
    }
    public class ChildClass : AccessClass
    {
        void Display()
        {
            Console.WriteLine(publicVar);
            Console.WriteLine(internalVar);
            Console.WriteLine(protectedVar);
            Console.WriteLine(protectedInternalVar);
            Console.WriteLine(protectedPrivateVar);
            publicMethod();
            internalMethod();
            protectedMethod();
            protectedInternalMethod();
            protectedPrivateMethod();
            
        }
    }
    public class AccessClass
    {
        static void Main(string[] args){}

        // все равно, что private int defaultVar;
        int defaultVar;

        // поле доступно только из текущего класса
        private int privateVar;

        // доступно из текущего класса и производных классов, которые определены в этом же проекте
        protected private int protectedPrivateVar;

        // доступно из текущего класса и производных классов
        protected int protectedVar;

        // доступно в любом месте текущего проекта
        internal int internalVar;

        // доступно в любом месте текущего проекта и из классов-наследников в других проектах
        protected internal int protectedInternalVar;

        // доступно в любом месте программы, а также для других программ и сборок
        public int publicVar = 2;

        // по умолчанию имеет модификатор private
        void defaultMethod() => Console.WriteLine($"defaultVar = {defaultVar}");

        // метод доступен только из текущего класса
        private void privateMethod() => Console.WriteLine($"privateVar = {privateVar}");

        // доступен из текущего класса и производных классов, которые определены в этом же проекте
        protected private void protectedPrivateMethod() =>
            Console.WriteLine($"protectedPrivateVar = {protectedPrivateVar}");

        // доступен из текущего класса и производных классов
        protected void protectedMethod() => Console.WriteLine($"protectedVar = {protectedVar}");

        // доступен в любом месте текущего проекта
        internal void internalMethod() => Console.WriteLine($"internalVar = {internalVar}");

        // доступен в любом месте текущего проекта и из классов-наследников в других проектах
        protected internal void protectedInternalMethod() =>
            Console.WriteLine($"protectedInternalVar = {protectedInternalVar}");

        // доступен в любом месте программы, а также для других программ и сборок
        public void publicMethod() => Console.WriteLine($"publicVar = {publicVar}");
    }
}