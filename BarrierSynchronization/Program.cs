Barrier barrier = new(3,_=> Console.WriteLine("Barrier point"));

Thread thread1 = new Thread(() =>
{
	for (int i = 0; i < 5; i++)	
		Console.WriteLine($"Thread 1.1 is value : {i}");

	barrier.SignalAndWait();

	for (int i = 0;i < 2; i++)	
		Console.WriteLine($"Thread 1.2 is value : {i}");	
});

Thread thread2 = new Thread(() =>
{
	for (int i = 0; i < 4; i++)
		Console.WriteLine($"Thread 2.1 is value : {i}");

	barrier.SignalAndWait();

	for (int i = 0; i < 3; i++)
		Console.WriteLine($"Thread 2.2 is value : {i}");
});

Thread thread3 = new Thread(() =>
{
	for (int i = 0; i < 6; i++)
		Console.WriteLine($"Thread 3.1 is value : {i}");

	barrier.SignalAndWait();

	for (int i = 0; i < 7; i++)
		Console.WriteLine($"Thread 3.2 is value : {i}");
});

thread1.Start();
thread2.Start();
thread3.Start();

Console.Read();