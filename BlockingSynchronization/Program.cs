class Program
{
	static Mutex _mutex;
	static string programName = "Example Project";
	static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
	static int counter = 0;
	static void Main(string[] args)
	{
		#region ThreadId
		//Console.WriteLine($"Main Thread Id: {Environment.CurrentManagedThreadId}");

		//Thread thread = new(() =>
		//{
		//	for (int i = 0; i < 10; i++)
		//	{
		//		Console.WriteLine(i);
		//	}
		//	Console.WriteLine($"Worker Thread Id: {Environment.CurrentManagedThreadId}");
		//});
		//thread.Start();

		//Console.Read();
		#endregion

		#region ThreadState
		//Thread thread = new(() =>
		//{
		//	int i = 10;
		//	while (i>=0)
		//	{
		//		i--;
		//		Thread.Sleep(1000);
		//	}
		//	Console.WriteLine("Worker thread görevini tamamladı.");
		//});
		//thread.Start();

		//ThreadState state = ThreadState.Running;
		//while (true)
		//{
		//	if (state == ThreadState.Stopped)
		//		break;

		//	if (state != thread.ThreadState)
		//	{
		//		state = thread.ThreadState;
		//		Console.WriteLine(thread.ThreadState);
		//	}
		//}

		//Console.Read();
		#endregion

		#region Locking
		//int i = 1;
		//object _locking = new();
		//Thread thread = new(() =>
		//{
		//	lock (_locking)
		//	{
		//		while (i <= 10)
		//		{
		//			i++;
		//			Console.WriteLine($"Thread 1 : {i}");
		//		}
		//	}
		//});

		//Thread thread2 = new Thread(() =>
		//{
		//	lock(_locking)
		//	{
		//		while (i > 0)
		//		{
		//			i--;
		//			Console.WriteLine($"Thread 2 : {i}");
		//		}
		//	}
		//});
		//thread.Start();
		//thread2.Start();

		#endregion

		#region Join
		//int i = 1;
		//Thread thread = new(() =>
		//{

		//	while (i <= 10)
		//	{
		//		i++;
		//		Console.WriteLine($"Thread 1 : {i}");
		//	}

		//});

		//Thread thread2 = new Thread(() =>
		//{

		//	while (i > 0)
		//	{
		//		i--;
		//		Console.WriteLine($"Thread 2 : {i}");
		//	}

		//});
		//thread.Start();
		//thread.Join();
		//thread2.Start();
		#endregion

		#region Canceling
		//bool isAborted = false;
		//Thread thread = new(() =>
		//{
		//	while (!isAborted)
		//	{
		//		Console.WriteLine("Thread başladı");
		//		for (int i = 0; i < 10; i++)
		//		{
		//			Console.WriteLine(i);
		//		}
		//	}			
		//});
		//thread.Start();
		//Thread.Sleep(1000);
		//isAborted = true;

		#endregion

		#region Monitor.Enter ve Monitor.Exit
		//int i = 1;
		//object _locking = new();
		//Thread thread = new(() =>
		//{
		//	try
		//	{
		//		Monitor.Enter(_locking);
		//		while (i <= 10)
		//		{
		//			i++;
		//			Console.WriteLine($"Thread 1 : {i}");
		//		}
		//	}
		//	finally
		//	{
		//		Monitor.Exit(_locking);
		//	}						
		//});

		//Thread thread2 = new Thread(() =>
		//{
		//	try
		//	{
		//		Monitor.Enter(_locking);
		//		while (i > 0)
		//		{
		//			i--;
		//			Console.WriteLine($"Thread 2 : {i}");
		//		}
		//	}
		//	finally
		//	{
		//		Monitor.Exit(_locking);
		//	}						
		//});
		//thread.Start();
		//thread2.Start();
		#endregion

		#region lockTaken Parameter
		//int i = 1;
		//object _locking = new();
		//Thread thread = new(() =>
		//{
		//	try
		//	{
		//		bool lockTaken = false;
		//		Monitor.Enter(_locking,ref lockTaken);
		//		if (lockTaken)
		//		{
		//			while (i <= 10)
		//			{
		//				i++;
		//				Console.WriteLine($"Thread 1 : {i}");
		//			}
		//		}				
		//	}
		//	finally
		//	{
		//		Monitor.Exit(_locking);
		//	}
		//});

		//Thread thread2 = new Thread(() =>
		//{
		//	try
		//	{
		//		bool lockTaken = false;
		//		Monitor.Enter(_locking, ref lockTaken);
		//		if (lockTaken)
		//		{
		//			while (i > 0)
		//			{
		//				i--;
		//				Console.WriteLine($"Thread 2 : {i}");
		//			}
		//		}				
		//	}
		//	finally
		//	{
		//		Monitor.Exit(_locking);
		//	}
		//});
		//thread.Start();
		//thread2.Start();

		#endregion

		#region Monitor.TryEnter
		//int i = 1;
		//object _locking = new();
		//Thread thread = new(() =>
		//{
		//	bool result = Monitor.TryEnter(_locking, 100);
		//	//bool lockTaken = false
		//	//Monitor.TryEnter(_locking, 100,ref lockTaken);
		//	//if(lockTaken) ... şeklinde de devam edebiliriz yani lockTaken parametresini de kullanıp kontrol sağlayabiliriz.
		//	if (result)
		//	{
		//		try
		//		{
		//			while (i <= 10)
		//			{
		//				i++;
		//				Console.WriteLine($"Thread 1 : {i}");
		//			}
		//		}
		//		finally
		//		{
		//			Monitor.Exit(_locking);
		//		}
		//	}
		//});

		//Thread thread2 = new Thread(() =>
		//{
		//	var result = Monitor.TryEnter(_locking, 300);
		//	if (result)
		//	{
		//		try
		//		{
		//			while (i > 0)
		//			{
		//				i--;
		//				Console.WriteLine($"Thread 2 : {i}");
		//			}
		//		}
		//		finally
		//		{
		//			Monitor.Exit(_locking);
		//		}
		//	}
		//});
		//thread.Start();
		//thread2.Start();
		#endregion

		#region Mutex
		//Mutex mutex = new Mutex();
		//Thread thread = new(() =>
		//{
		//	mutex.WaitOne();
		//	for (int i = 0; i < 10; i++)
		//	{
		//		Console.WriteLine($"Thread 1 {i}");
		//	}
		//	mutex.ReleaseMutex();
		//});

		//Thread thread2 = new(() =>
		//{
		//	mutex.WaitOne();
		//	for (int i = 10; i >0; i++)
		//	{
		//		Console.WriteLine($"Thread 2 {i}");
		//	}
		//	mutex.ReleaseMutex();
		//});
		//thread.Start();
		//thread2.Start();
		#endregion

		#region Single Instance Application with Mutex

		//Mutex.TryOpenExisting(programName, out _mutex);
		//if (_mutex == null)
		//{
		//	_mutex = new(true, programName);
		//	Console.WriteLine("Program ayakta....");
		//	Console.Read();
		//}
		//else
		//{
		//	_mutex.Close();
		//	return;
		//}
		#endregion

		#region Semaphore
		//List<int> numbers = new();
		//Semaphore semaphore = new(1, 2); //ilk parametre erişim izni alabilecek thread sayısı, erişim izni alabilecek thread
		//								 //sayısını tutan bir sayaçtır aslında, ikinci parametre ise erişim verilecek maksimum thread sayısı
		//Thread thread = new(() =>
		//{
		//	semaphore.WaitOne();
		//	int i = 0;
		//	while (i < 10)
		//	{
		//		Console.WriteLine($"Thread 1 :{++i}");
		//		numbers.Add(i);
		//		Thread.Sleep(100);
		//	}
		//	semaphore.Release(); //release ettikten sonra semaphore sayacın değerini bir arttırır.
		//});

		//Thread thread2 = new(() =>
		//{
		//	semaphore.WaitOne();
		//	int i = 10;
		//	while (i < 20)
		//	{
		//		Console.WriteLine($"Thread 2 :{++i}");
		//		numbers.Add(i);
		//		Thread.Sleep(100);
		//	}
		//	semaphore.Release();
		//});

		//Thread thread3 = new(() =>
		//{
		//	semaphore.WaitOne();
		//	int i = 30;
		//	while (i < 40)
		//	{
		//		Console.WriteLine($"Thread 3 :{++i}");
		//		numbers.Add(i);
		//		Thread.Sleep(100);
		//	}
		//	semaphore.Release();
		//});

		//thread.Start();
		//thread2.Start();
		//thread3.Start();
		#endregion

		#region SemaphoreSlim
		//List<int> numbers = new();
		//using SemaphoreSlim semaphoreSlim = new(2, 3); //ilk parametre erişim izni alabilecek thread sayısı, erişim izni alabilecek thread
		//								 //sayısını tutan bir sayaçtır aslında, ikinci parametre ise erişim verilecek maksimum thread sayısı
		//Thread thread = new(() =>
		//{
		//	semaphoreSlim.Wait(100);
		//	int i = 0;
		//	while (i < 10)
		//	{
		//		Console.WriteLine($"Thread 1 :{++i}");
		//		numbers.Add(i);
		//		Thread.Sleep(100);
		//	}
		//	semaphoreSlim.Release(); //release ettikten sonra semaphore sayacın değerini bir arttırır.
		//});

		//Thread thread2 = new(() =>
		//{
		//	semaphoreSlim.Wait(100);
		//	int i = 10;
		//	while (i < 20)
		//	{
		//		Console.WriteLine($"Thread 2 :{++i}");
		//		numbers.Add(i);
		//		Thread.Sleep(100);
		//	}
		//	semaphoreSlim.Release();
		//});

		//Thread thread3 = new(() =>
		//{
		//	semaphoreSlim.Wait(100);
		//	int i = 30;
		//	while (i < 40)
		//	{
		//		Console.WriteLine($"Thread 3 :{++i}");
		//		numbers.Add(i);
		//		Thread.Sleep(100);
		//	}
		//	semaphoreSlim.Release();
		//});

		//thread.Start();
		//thread2.Start();
		//thread3.Start();


		#endregion

		#region ReaderWriterLock & ReaderWriterLockSlim Sync.

		//for (int i = 0; i < 5; i++)
		//	new Thread(Read).Start();

		//for (int i = 0; i < 2; i++)
		//	new Thread(Write).Start();

		//static void Read()
		//{
		//	for (int i = 0; i < 10; i++)
		//	{
		//		try
		//		{
		//			counter++;
		//			readerWriterLockSlim.EnterReadLock();
		//			Console.WriteLine($"Reader : Thread {Thread.CurrentThread.ManagedThreadId} is reading : {counter}");
		//		}
		//		finally
		//		{
		//			readerWriterLockSlim.ExitReadLock();
		//		}
		//	}
		//}

		//static void Write()
		//{
		//	try
		//	{
		//		readerWriterLockSlim.EnterWriteLock();
		//		for (int i = 0; i < 10; i++)
		//		{
		//			counter++;
		//			Console.WriteLine($"Writer : Thread {Thread.CurrentThread.ManagedThreadId} is writing : {counter}");
		//		}
		//	}
		//	finally
		//	{
		//		readerWriterLockSlim.ExitWriteLock();
		//	}
		//}

		#endregion
		Console.Read();
	}
}
