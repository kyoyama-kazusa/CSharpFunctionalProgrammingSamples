using System.Threading;

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 使用委托类型的 <c>BeginInvoke</c> 和 <c>EndInvoke</c> 的操作。
/// </summary>
/// <remarks>
/// 在 .NET 5+ 的版本下，委托已经移除对 <c>BeginInvoke</c> 和 <c>EndInvoke</c> 方法的支持，
/// 它会直接抛异常，所以这段代码是无法运行的。你只能把他粘到你的目标平台低于这个版本的环境上才能使用。
/// 代码仅供参考。
/// </remarks>
internal sealed class Lesson23_DelegateBeginInvokeAndEndInvokeSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 定义一个求和操作的委托实例，这里模拟一下长时间操作，用 Thread.Sleep 来模拟。
		AddValueHandler handler = (a, b) =>
		{
			Thread.Sleep(3000);
			return a + b;
		};

		// 使用 BeginInvoke 开启异步操作执行。
		var asyncResult = handler.BeginInvoke(3, 4, null, null);

		// 开始异步执行，并且等待结果返回到主线程。
		var result = handler.EndInvoke(asyncResult);

		// 打印结果。
		Console.WriteLine(result);
	}
}

file delegate int AddValueHandler(int a, int b);