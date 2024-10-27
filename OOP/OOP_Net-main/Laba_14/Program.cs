using Laba_14;
using System.Diagnostics;

public class Program
{
    static void Main(string[] args)
    {
        
        First();
        Second();
        Third();
        Fourth();
        Fifth();
    }

    private static void First()
    {
        var allProcesses = Process.GetProcesses();        // получаем массив со всеми процессами
        Console.WriteLine("Information about processes:");
        Console.Write("{0,-20}", "ID:");
        Console.Write("{0,-70}", "Process Name:");
        Console.Write("{0,-20}", "Priority:\n");
        foreach (var process in allProcesses)
        {
            Console.Write("{0,-20}", $"{process.Id}");
            Console.Write("{0,-70}", $"{process.ProcessName}");
            Console.Write("{0,-20}", $"{process.BasePriority}");
            Console.WriteLine();
        }
    }

    private static void Second()
    {
        var domain = AppDomain.CurrentDomain;                  
        Console.WriteLine("Information about current domain:");
        Console.WriteLine("\n\nCurrent domain:\t" + domain.FriendlyName);
        Console.WriteLine("Base directory:\t" + domain.BaseDirectory);
        Console.WriteLine("Configuration Details:\t" + domain.SetupInformation);
        Console.WriteLine("All assemblies in the domain:\n");

        foreach (var assembly in domain.GetAssemblies())
        {
            Console.WriteLine(assembly.GetName().Name);
        }
    }

    private static void Third()
    {
        Mutex mutex = new Mutex();  // позволяет обеспечить синхронизацию среди множества процессов.
                                    // Только один поток может получить блокировку и иметь доступ к синхронизированным областям кода.
        Thread NumbersThread = new Thread(new ParameterizedThreadStart(WriteNums));   // создаем новый поток
        NumbersThread.Start(7);                                                       // запускаем его

        Thread.Sleep(2000);         // приостанавливает выполнение потока, в котором он был вызван
        mutex.WaitOne();            // ожидает до тех пор, пока не будет получен мьютекс, для которого он был вызван (вход в критическую секцию)

        Console.WriteLine("\n--------------------");
        Console.WriteLine("Priority:   " + NumbersThread.Priority);
        Thread.Sleep(100);
        Console.WriteLine("Name tread:  " + NumbersThread.Name);
        Thread.Sleep(100);
        Console.WriteLine("ID tread:   " + NumbersThread.ManagedThreadId);
        Console.WriteLine("---------------------");
        Thread.Sleep(1000);

        mutex.ReleaseMutex();       // освобождение мьютекса потоком, выход из критической секции
        Thread.Sleep(2000);         // приостанавливает выполнение потока, в котором он был вызван

        void WriteNums(object number)   // ввод чисел 
        {
            int num = (int)number;
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(500);
            }
        }
    }

    private static void Fourth()
    {
        Console.WriteLine("\n\n\nthead evan and odd numbers");
        Thread evenThread = new Thread(Methods.EvenNumbers);        // поток чётных чисел
        evenThread.Priority = ThreadPriority.Normal;           // меняем приоритет по заданию
        evenThread.Start();                 // если закомментить Join(), второй поток не будет ждать первый
        evenThread.Join();                  // чтобы выводились поочередно, надо закомментить эту строку

        Console.WriteLine();
        Thread oddThread = new Thread(Methods.OddNumbers);          // поток нечётных чисел
        oddThread.Priority = ThreadPriority.Normal;
        oddThread.Start();
        oddThread.Join();                  // дожидаемся завершение работы потока
        Console.WriteLine("\n");
    }
    

    private static void Fifth()
    {
        TimerCallback timerCallback = new TimerCallback(WhatTimeIsIt);  // делегат для таймера

        // позволяет запускать определенные действия по истечению некоторого периода времени:
        Timer timer = new Timer(timerCallback, null, 500, 1000);       /* null - параметр, которого нет, 500 - время, через которое запустится процесс с таймером, 
                                                                            * 1000 - периодичность таймера (интервал между вызовами метода делегата). */
        Thread.Sleep(5000);                                             // 500 - ждем и не закрываем поток
        timer.Change(Timeout.Infinite, 2000);                           // уничтожение таймера

        void WhatTimeIsIt(object obj)
        {
            Console.WriteLine($"It's {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
        }
        Console.ReadLine();
        Console.ReadLine();
    }
}