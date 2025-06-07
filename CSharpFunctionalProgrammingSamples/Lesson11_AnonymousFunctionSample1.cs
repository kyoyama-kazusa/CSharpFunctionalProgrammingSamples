namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 匿名函数例子 1 - 热水器的那个例子。
/// </summary>
internal sealed class Lesson11_AnonymousFunctionSample1 : Sample
{
	/// <inheritdoc/>
	/// <remarks><inheritdoc cref="Lesson03_HeaterEventHandlerSample.RunSample" path="/remarks"/></remarks>
	public override async void RunSample()
	{
		// 声明一个热水器的实例。
		var heater = new Heater(50);

		// 挂载上一个操作，这个操作会自动列到通知的操作列表里，一会儿水烧合适了就会自动调用这个方法。
		// 使用匿名函数可以减少完整函数声明的格式，避免代码臃肿。
		heater.HeaterFinished += delegate (Heater sender, HeaterFinishedEventArgs e)
		{
			Console.WriteLine("水烧好了，请慢用。");
		};

		// 模拟烧水。
		while (true)
		{
			// 等待一秒。
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
}