#region AutoResetEvent
//AutoResetEvent autoResetEvent = new AutoResetEvent(false); //Sinyal gönderilmeye hazır
//Thread thread1 = new Thread(() =>
//{
//	Console.WriteLine("Thread 1");
//	autoResetEvent.Set(); //Sinyal gönderildi
//});

//Thread thread2 = new Thread(() =>
//{
//	autoResetEvent.WaitOne(); //Sinyal beklemede
//	Console.WriteLine("Thread 2");
//	autoResetEvent.Set();
//});

//Thread thread3 = new Thread(() =>
//{
//	autoResetEvent.WaitOne();
//	Console.WriteLine("Thread 3");
//	autoResetEvent.Set();
//});

//thread1.Start();
//thread2.Start();
//thread3.Start();
#endregion

#region ManualResetEvent
//ManualResetEvent manualResetEvent = new ManualResetEvent(false); //Sinyal gönderilmeye hazır
//Thread thread1 = new Thread(() =>
//{
//	Console.WriteLine("Thread 1");
//	manualResetEvent.Set(); //Sinyal gönderildi	
//});

//Thread thread2 = new Thread(() =>
//{
//	manualResetEvent.WaitOne(); //Sinyal beklemede
//	Console.WriteLine("Thread 2");	
//});

//Thread thread3 = new Thread(() =>
//{
//	manualResetEvent.WaitOne();
//	Console.WriteLine("Thread 3");	
//});

//thread1.Start();
//thread2.Start();
//thread3.Start();
#endregion

#region EventWaitHandle
//EventWaitHandle eventWaitHandle = new EventWaitHandle(false,EventResetMode.ManualReset); //Sinyal gönderilmeye hazır
//Thread thread1 = new Thread(() =>
//{
//	Console.WriteLine("Thread 1");
//	eventWaitHandle.Set(); //Sinyal gönderildi	
//});

//Thread thread2 = new Thread(() =>
//{
//	eventWaitHandle.WaitOne(); //Sinyal beklemede
//	Console.WriteLine("Thread 2");
//});

//Thread thread3 = new Thread(() =>
//{
//	eventWaitHandle.WaitOne();
//	Console.WriteLine("Thread 3");
//});

//thread1.Start();
//thread2.Start();
//thread3.Start();
#endregion

#region CountdownEvent
CountdownEvent countdownEvent = new(2);

Thread thread1 = new Thread(() =>
{
	Console.WriteLine("Thread 1");
	countdownEvent.Signal();
});

Thread thread2 = new Thread(() =>
{
	Console.WriteLine("Thread 2");
	countdownEvent.Signal();
});

Thread thread3 = new Thread(() =>
{
	countdownEvent.Wait();
	Console.WriteLine("Thread 3");
});

Console.WriteLine("İşlemler tamamlandı");

thread1.Start();
thread2.Start();
thread3.Start();
#endregion
Console.Read();