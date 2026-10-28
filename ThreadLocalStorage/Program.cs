
#region ThreadStatic Attribute

//internal class Program
//{
//	[ThreadStatic]
//	static int x = 0;
//	private static void Main(string[] args)
//	{
//		Thread thread1 = new Thread(() =>
//		{
//			while (x<=10)
//			{
//				Console.WriteLine($"Thread1 x: {++x}");
//			}			
//		});

//		Thread thread2 = new Thread(() =>
//		{
//			while (x <= 10)
//			{
//				Console.WriteLine($"Thread2 x: {++x}");
//			}
//		});

//		Thread thread3 = new Thread(() =>
//		{
//			while (x <= 10)
//			{
//				Console.WriteLine($"Thread3 x: {++x}");
//			}
//		});

//		thread1.Start();
//		thread2.Start();
//		thread3.Start();

//		Console.Read();
//	}
//}

#endregion

#region ThreadLocal<T> Class

//   ThreadLocal<int> x = new(() => 0);


//Thread thread1 = new Thread(() =>
//{
//	while (x.Value <= 10)
//	{
//		Console.WriteLine($"Thread1 x: {++x.Value}");
//	}
//});

//Thread thread2 = new Thread(() =>
//{
//	while (x.Value <= 10)
//	{
//		Console.WriteLine($"Thread2 x: {++x.Value}");
//	}
//});

//Thread thread3 = new Thread(() =>
//{
//	while (x.Value <= 10)
//	{
//		Console.WriteLine($"Thread3 x: {++x.Value}");
//	}
//});

//thread1.Start();
//thread2.Start();
//thread3.Start();
//Console.Read();
#endregion

#region GetData & SetData
internal class Program
{
	static LocalDataStoreSlot localDataStoreSlot = Thread.GetNamedDataSlot("x");
	static int x { 
		get {
			var data = (int?)Thread.GetData(localDataStoreSlot);
			return data is null ? 0 : data.Value;
		} 		
		set {
			Thread.SetData(localDataStoreSlot, value);
		} 
	
	
	}
	private static void Main(string[] args)
	{		
		Thread thread1 = new Thread(() =>
		{
			while (x <= 10)
			{
				Console.WriteLine($"Thread1 x: {++x}");
			}
		});

		Thread thread2 = new Thread(() =>
		{
			while (x <= 10)
			{
				Console.WriteLine($"Thread2 x: {++x}");
			}
		});

		Thread thread3 = new Thread(() =>
		{
			while (x <= 10)
			{
				Console.WriteLine($"Thread3 x: {++x}");
			}
		});

		thread1.Start();
		thread2.Start();
		thread3.Start();


		Console.Read();
	}
}

#endregion
