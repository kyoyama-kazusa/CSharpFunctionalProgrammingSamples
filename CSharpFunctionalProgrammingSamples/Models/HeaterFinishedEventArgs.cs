#pragma warning disable IDE0290

namespace CSharpFunctionalProgrammingSamples.Models;

/// <summary>
/// 封装一个类型，表示热水器完成烧水的时候的核心状态信息都有什么数据，都用这个类型包装
/// （规范实现需要用 <see cref="EventArgs"/> 完成继承）。
/// </summary>
internal sealed class HeaterFinishedEventArgs : EventArgs
{
	/// <summary>
	/// 实例化一个类型，因为这个类型是不包裹热水器实例本身的，
	/// 所以一般需要传入热水器的相关数据，一般是不传入热水器实例（实例走刚才委托的第一个参数传入过了）。
	/// </summary>
	/// <param name="currentTemperature">当前温度。</param>
	public HeaterFinishedEventArgs(int currentTemperature)
	{
		CurrentTemperature = currentTemperature;
	}


	/// <summary>
	/// 表示当前温度。
	/// </summary>
	public int CurrentTemperature { get; }
}