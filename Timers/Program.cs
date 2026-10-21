#region System.Threading
//Timer timer = new(state =>
//{
//	Console.WriteLine(state);
//}, "Tick", 1000, Timeout.Infinite);

//timer.Change(0, 1500);
#endregion

#region System.Timers
using System.Timers.Timer timer = new System.Timers.Timer();

timer.Elapsed += (sender, e) =>
{
	Console.WriteLine("Ticked!");
};
timer.Interval = 1000;
timer.Start();
Thread.Sleep(10000);
#endregion

Console.Read(); 