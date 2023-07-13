using System;
using System.Threading;
using System.Threading.Tasks;
namespace Chapter3.Recipe1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int threadId = 0;

            // 使用 Task.Run() 方法将操作放入线程池中执行
            var task = Task.Run(() => Test(out threadId));
            await task;

            Console.WriteLine($"Thread id: {threadId}");

            // 使用 ThreadPool.QueueUserWorkItem() 方法将操作放入线程池中执行
            ThreadPool.QueueUserWorkItem(Callback, "a delegate asynchronous call");

            // 等待异步操作完成
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        private static void Callback(object state)
        {
            Console.WriteLine("Starting a callback...");
            Console.WriteLine($"State passed to a callbak: {state}");
            Console.WriteLine($"Is thread pool thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Thread pool worker thread id: {Thread.CurrentThread.ManagedThreadId}");
        }

        private static string Test(out int threadId)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine($"Is thread pool thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            threadId = Thread.CurrentThread.ManagedThreadId;
            return $"Thread pool worker thread id was: {threadId}";
        }
    }
}