#pragma warning disable IDE0031, IDE0032, IDE0290, IDE1005

namespace CSharpFunctionalProgrammingSamples.Models;

/// <summary>
/// 热水器。
/// </summary>
/// <remarks>
/// 为了演示清楚，这里尽量采用最原始的 C# 语法呈现。我显然知道他们的简写语法糖，但是这不便于学习内容，不知道它省略了什么东西。这也不符合我讲解的习惯。
/// </remarks>
internal sealed class Heater
{
	/// <summary>
	/// 表示预期烧开的热水温度。
	/// </summary>
	private readonly int _desiredTemperature;

	/// <summary>
	/// 表示当前热水温度。
	/// </summary>
	private int _temperature = 30;

	/// <summary>
	/// 表示一个操作，当水烧好了的时候回调使用的操作。
	/// </summary>
	private HeaterFinishedEventHandler? _heaterFinishedEventHandler;


	/// <summary>
	/// 实例化一个热水器对象，传入的参数表示这个热水器应该烧到多少温度就通知水温合适的信息。
	/// </summary>
	/// <param name="desiredTemperature">温度。</param>
	public Heater(int desiredTemperature)
	{
		_desiredTemperature = desiredTemperature;
	}


	/// <summary>
	/// 表示当前温度。
	/// </summary>
	public int Temperature
	{
		get { return _temperature; }
	}

	/// <summary>
	/// 表示预期的水温。
	/// </summary>
	public int DesiredTemperature
	{
		get { return _desiredTemperature; }
	}


	/// <summary>
	/// 表示一个事件，当且仅当温度达标的时候通知用户水烧好了。
	/// </summary>
	/// <value>表示为底层的委托字段增加和删除的绑定方法操作。</value>
	public event HeaterFinishedEventHandler? HeaterFinished
	{
		add { _heaterFinishedEventHandler += value; }

		remove { _heaterFinishedEventHandler -= value; }
	}


	/// <inheritdoc/>
	public override string ToString()
	{
		return $"当前温度：{_temperature}";
	}

	/// <summary>
	/// 这里是一个举例，执行升温逻辑用，随机升温 1 - 2 摄氏度。
	/// </summary>
	internal void IncreaseTemperature()
	{
		var delta = Random.Shared.NextDouble() >= .5 ? 2 : 1;
		_temperature += delta;

		// 通知水温合适的代码部分。
		if (_temperature >= _desiredTemperature)
		{
			// 如果水温合适了就通知。
			if (_heaterFinishedEventHandler != null)
			{
				// 查看绑定的回调操作是否挂载了任意的方法（函数）。
				// 如果有的话就调用它。

				// C# 1 写法：
				//_heaterFinishedEventHandler.Invoke(this, new HeaterFinishedEventArgs(_temperature));
				// C# 2 开始的写法（委托推断：允许省略 .Invoke，和原始效果完全一样，只是简写）：
				_heaterFinishedEventHandler(this, new HeaterFinishedEventArgs(_temperature));
			}
		}
	}
}
