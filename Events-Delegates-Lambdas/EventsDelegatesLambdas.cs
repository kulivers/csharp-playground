// C# program to illustrate the

using System;
using System.Text.Unicode;

namespace EventsDelegatesLambdas
{
    public enum WorkType
    {
        GoToMeetings,
        Golf,
        GenerateSupports
    }

    public delegate void
        WorkPerfomed(object sender,
            WorkPerfomedEventArgs args); //здесь могут быть любые параметры, но просто это стандарт

    //также здесь могут быть и стандартные EventArgs:
    class Worker
    {
        public event WorkPerfomed WorkPerfomedEvent;
        public EventHandler WorkCompletedEvent;
        public EventHandler<WorkPerfomedEventArgs> WorkCompletedEventWithArgs;

        public void DoWork(int hours, WorkType workType)
        {
            for (int i = 0; i < hours; i++)
            {
                System.Threading.Thread.Sleep(500);
                OnWorkPerfomed(i + 1, workType);
            }

            OnWorkCompletedEvent(hours, workType);
        }

        protected virtual void OnWorkPerfomed(int hours, WorkType worktype)
        {
            WorkPerfomedEvent?.Invoke(this, new WorkPerfomedEventArgs(hours, worktype));
        }

        protected virtual void OnWorkCompletedEvent(int hours, WorkType workType)
        {
            WorkCompletedEvent?.Invoke(this, EventArgs.Empty);
            WorkCompletedEventWithArgs?.Invoke(this, new WorkPerfomedEventArgs(hours, workType));
        }
    }

    public class WorkPerfomedEventArgs : EventArgs
    {
        public int Hours { get; set; }
        public WorkType WorkType { get; set; }

        public WorkPerfomedEventArgs(int hours, WorkType workType)
        {
            Hours = hours;
            WorkType = workType;
        }
    }



    class Program
    {
        public static void Local_Main()
        {
            var worker = new Worker();
            //привязываем на эвент обработчик событий
            worker.WorkPerfomedEvent += (sender, args) =>
                Console.WriteLine(
                    $"WorkPerfomedEventHandler, sender: {sender}, args: {args.Hours} {args.WorkType}");
            //     delegate(object sender, WorkPerfomedEventArgs args)
            // {
            //     Console.WriteLine(
            //         $"WorkPerfomedEventHandler, sender: {sender}, args: {args.Hours} {args.WorkType}");
            // };
            worker.WorkCompletedEvent += WorkCompletedEventHandler;
            worker.WorkCompletedEventWithArgs += WorkCompletedEventHandler;
            worker.DoWork(8, WorkType.Golf);
        }

        static void WorkCompletedEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine($"Work!Completed!EventHandler, sender: {sender}, args: {args}");
        }
    }
}
// concept of method hiding