#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// Lambda 表达式的底层运作。
/// </summary>
internal sealed class Lesson16_BackingImplOfLambdaExpressionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		//
		// 有捕获。
		//

		// 捕获变量 - 在 lambda 表达式里使用 lambda 执行块之外的变量信息的行为。
		//int i = 42;
		//Action action = () => i *= 2;

		//Console.WriteLine(i);
		//action();
		//Console.WriteLine(i);

		// 前文捕获机制的完整底层实现。
		var instance = new LambdaClosure();
		instance.i = 42;
		Action action = instance.action; // 实例方法组。

		Console.WriteLine(instance.i);
		action();
		Console.WriteLine(instance.i);


		//
		// 没有捕获。
		//

		//Action printer = () => Console.WriteLine("Hello");
		//printer();

		// 没有捕获的 lambda 表达式会翻译成这样：
		Action printer = LambdaNoCapture.printer; // 静态方法组。
		printer();
	}


	/// <summary>
	/// lambda 表达式没有捕获的时候的样子。
	/// </summary>
	private sealed class LambdaNoCapture
	{
		/// <summary>
		/// 底层会生成的实际函数调用，但是没有捕获的时候方法则是静态的。
		/// </summary>
		public static void printer()
		{
			Console.WriteLine("Hello");
		}
	}


	/// <summary>
	/// lambda 表达式底层所产生的实体类型。这个类型我们叫闭包（Closure）。
	/// </summary>
	private sealed class LambdaClosure
	{
		/// <summary>
		/// 字段。所有 lambda 表达式捕获的变量全部会复制（照抄一份）到这里。
		/// </summary>
		public int i;


		/// <summary>
		/// lambda 表达式的执行语句。
		/// </summary>
		public void action()
		{
			i *= 2;
		}
	}
}
