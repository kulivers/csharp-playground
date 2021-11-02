using System;

namespace Interfaces
{
    internal interface ISaveble
    {
        void ExplicitlySave();

        void UsualSave()
        {
            Console.WriteLine("Here is default implamentaion");
        }
    }

    internal class ExplicitInterfaceExample : ISaveble
    {
        void ISaveble.ExplicitlySave()
        {
            Console.WriteLine("this is explicitly Save, it is belong to interface");
        }

        public void ExplicitlySave()
        {
            Console.WriteLine("this is explicitly Save, it is belong to class");
        }

        public void UsualSave()
        {
            Console.WriteLine("We didnt it, cuz we dont want method with same name" +
                              "(that's all what we want from explitily implementation)");
        }

    }
}