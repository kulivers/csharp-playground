using System;

namespace ModifyAccessors
{
    public class ClassInOtherFile
    {
        AccessClass _accessClassObj = new AccessClass();

        void Display()
        {
            _accessClassObj.publicMethod();
            _accessClassObj.internalMethod();
            _accessClassObj.protectedInternalMethod();
            Console.WriteLine(_accessClassObj.internalVar);
            Console.WriteLine(_accessClassObj.publicVar);
            Console.WriteLine(_accessClassObj.protectedInternalVar);
        }
    }
}