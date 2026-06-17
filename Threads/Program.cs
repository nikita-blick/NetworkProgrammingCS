#define THREADS_EXAMPLE_1
//#define THREADS_EXAMPLE_2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
	class Program
	{
		static Mutex mutex = new Mutex();
		static bool finish = false;
		static void Main(string[] args)
		{

#if THREADS_EXAMPLE_1
			//Plus();
			//Minus();
			Thread plus_thread = new Thread(Plus);
			Thread minus_thread = new Thread(Minus);
			plus_thread.Start();
			minus_thread.Start();
			Console.ReadLine();
			finish = true;
			plus_thread.Join(); //Останавливает поток
			minus_thread.Join();
#endif

#if THREADS_EXAMPLE_2
			Thread thread = new Thread(ThreadProc);
			thread.Start();

			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine($"MainThread:{i}");
				Thread.Sleep(1);
			}

			thread.Join(); 
#endif

		}
		static void ThreadProc()
		{
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine($"ThreadProc:{i}");
				Thread.Sleep(1);
			}
		}
		static void Plus()
		{
			while (!finish)
			{
				mutex.WaitOne();
				Console.Write("+ ");
				Thread.Sleep(1);
				mutex.ReleaseMutex();
			}
		}
		static void Minus()
		{
			while (!finish)
			{
				mutex.WaitOne();
				Console.Write("- ");
				Thread.Sleep(1);
				mutex.ReleaseMutex();
			}
		}
	}
}