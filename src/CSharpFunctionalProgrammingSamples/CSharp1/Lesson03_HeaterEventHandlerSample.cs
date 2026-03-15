namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 热水器委托事件例子。
/// </summary>
internal sealed class Lesson03_HeaterEventHandlerSample : Sample
{
	/// <inheritdoc/>
	/// <remarks>
	/// 注意这个函数带有异步操作，但不返回值。如果要看这个实例的效果，
	/// 请在调用方强行插入 <see cref="Console.ReadKey()"/> 之类的函数避免主线程直接退出。
	/// </remarks>
	public override async void RunSample()
	{
		// 声明一个热水器的实例。
		var heater = new Heater(50);

		// 挂载上一个操作，这个操作会自动列到通知的操作列表里，
		// 一会儿水烧合适了就会自动调用这个方法。
		// 这个方法现在不会马上调用。
		// C# 1 的写法：
		//heater.HeaterFinished += new HeaterFinishedEventHandler(Heater_HeaterFinished);
		// C# 2+ 的写法（委托方法组转换：允许省略 new 委托的声明部分）：
		heater.HeaterFinished += Heater_HeaterFinished;

		// 模拟烧水。
		while (true)
		{
			// 等待一秒。
			// 这个是 C# 5 的异步函数设计，不属于这里介绍的内容。
			// 你就理解为它会等待一秒，在一秒期间程序会一直卡在这个函数这里不进行后面的处理过程。
			await Task.Delay(1000);

			// 升温。
			heater.IncreaseTemperature();

			// 打印当前温度。
			Console.WriteLine(heater.ToString());

			if (heater.Temperature >= heater.DesiredTemperature)
			{
				// 如果超过了热水器设定的烧开的温度，我们就关闭电源（跳出 while (true) 循环）。
				break;
			}
		}

		Console.WriteLine("关闭电源。");
	}


	/// <summary>
	/// 用于热水器烧开后执行的绑定函数操作。
	/// </summary>
	/// <param name="sender">热水器实例（哪一个热水器烧开了）。</param>
	/// <param name="e">事件参数。</param>
	private void Heater_HeaterFinished(Heater sender, HeaterFinishedEventArgs e)
	{
		Console.WriteLine("水烧好了，请慢用。");
	}
}
