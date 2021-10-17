using System.Threading;
using System.Threading.Tasks;

namespace TAP
{
    namespace TimerTasks
    {
        public class DeferredTask
        {
            public static void RunTaskAfterNSec(Task task, int ms)
            {
                Timer timer = new Timer(_ => { task.Start(); }, null, ms, Timeout.Infinite);
            }
        }
    }
}