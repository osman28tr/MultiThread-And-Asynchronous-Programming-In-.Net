#region ThreadPool
//ThreadPool.SetMinThreads(3, 3);
//ThreadPool.SetMaxThreads(5, 5);

//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 1");
//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 2");
//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 3");
//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 4");
//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 5");
//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 6");
//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 7");
//ThreadPool.QueueUserWorkItem(WorkerMethod, "Task 8");

//void WorkerMethod(object state)
//{
//	Console.WriteLine($"Running Thread Count : {ThreadPool.ThreadCount}");
//	string name = state.ToString();
//	Console.WriteLine($"{name} işi başladı");
//	Thread.Sleep(new Random().Next(1000, 5000));
//	Console.WriteLine($"{name} işi tamamlandı");
//}
#endregion

#region Wait Handles

//AutoResetEvent autoResetEvent = new(false);

//RegisteredWaitHandle registeredWaitHandle =
//	ThreadPool.RegisterWaitForSingleObject(autoResetEvent, WorkerMethod, "Task 1 Wait Handle", -1, true);

//autoResetEvent.Set();

//registeredWaitHandle.Unregister(autoResetEvent);
//void WorkerMethod(object state,bool timedout)
//{
//	Console.WriteLine($"Running Thread Count : {ThreadPool.ThreadCount}");
//	string name = state.ToString();
//	Console.WriteLine($"{name} işi başladı");
//	Thread.Sleep(new Random().Next(1000, 5000));
//	Console.WriteLine($"{name} işi tamamlandı");
//}
#endregion

#region WaitHandle.WaitAll
//Console.WriteLine("Merhaba");
//AutoResetEvent autoResetEvent1 = new(false);
//AutoResetEvent autoResetEvent2 = new(false);
//ManualResetEvent manualResetEvent1 = new(false);
//ManualResetEvent manualResetEvent2 = new(false);

//autoResetEvent1.Set();
//autoResetEvent2.Set();
//manualResetEvent1.Set();
//manualResetEvent2.Set();

//Thread thread = new(() =>
//{
//	WaitHandle.WaitAll(new WaitHandle[] { autoResetEvent1, autoResetEvent2, manualResetEvent1, manualResetEvent2 });
//	Console.WriteLine("Worker Thread");
//});

//thread.Start();

//Console.WriteLine("Merhaba, Main Thread");

#endregion

#region WaitHandle.WaitAny
//AutoResetEvent autoResetEvent1 = new(false);
//AutoResetEvent autoResetEvent2 = new(false);
//ManualResetEvent manualResetEvent1 = new(false);
//ManualResetEvent manualResetEvent2 = new(false);

//autoResetEvent1.Set();

//WaitHandle.WaitAny(new WaitHandle[] { autoResetEvent1, autoResetEvent2, manualResetEvent1, manualResetEvent2 });

//Console.WriteLine("Merhaba, Main Thread");
#endregion

#region WaitHandle.SignalAndWait

AutoResetEvent autoResetEvent1 = new(false);
AutoResetEvent autoResetEvent2 = new(false);

autoResetEvent1.Set();
autoResetEvent2.Set();

WaitHandle.SignalAndWait(autoResetEvent1, autoResetEvent2);

Console.WriteLine("Merhaba, Main Thread");

#endregion

Console.Read();