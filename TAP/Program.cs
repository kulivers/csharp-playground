using System.Threading;

namespace ThreadPoolExample
{
    class Program
    {
        static void RWDeclaration()
        {

            var rwl = new ReaderWriterLockSlim();

            rwl.EnterReadLock();
            try
            {
                // ...
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        static void Main()
        {
            RWDeclaration();
            
        }
    }
}