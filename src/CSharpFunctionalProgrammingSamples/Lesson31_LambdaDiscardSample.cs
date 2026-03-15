using System.Timers;

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// lambda 表达式弃元。
/// </summary>
internal sealed class Lesson31_LambdaDiscardSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// C# 2 匿名函数是允许不写参数表列的。
		// 不写参数的时候，我们把这个行为称为弃元（Discard）。

		// C# 9 开始支持 lambda 表达式弃元。
		// 当且仅当参数至少有两个都是弃元的时候，下划线才是真正的弃元效果；否则下划线就是普通的变量，可以被使用。
		//Action<int, int, int> action = (_, b, _) => Console.WriteLine(b);

		var timer = new Timer(1000);
		timer.Elapsed += static (_, _) => Console.WriteLine(DateTime.Now);
		timer.Start();
		Console.ReadKey();
	}
}
