#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 不要混用 lambda 表达式和本地函数。
/// </summary>
internal sealed class Lesson28_DoNotMixLambdaAndLocalFunctionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 捕获变量 - 在本地函数里使用本地函数的函数体之外的变量信息的行为。
		//int i = 42;
		//Console.WriteLine(i);
		//Action action = () => i *= 3; // sealed class 闭包
		//action();
		//localFunction(); // struct 闭包
		//Console.WriteLine(i);


		//void localFunction() => i *= 2;

		// 当同时定义了 lambda 表达式和本地函数且存在闭包的时候，C# 会优先使用 sealed class 闭包的版本，
		// 并会将本地函数的相关代码照抄到 sealed class 的闭包之中去，而不是声明同级别的 ref 修饰符 + struct 实例的静态函数。
		var closure = new Closure();
		closure.i = 42;
		Console.WriteLine(closure.i);
		Action action = closure.action;
		action(); // 调用原先的 lambda 表达式。
		closure.localFunction(); // 调用原先的本地函数。
		Console.WriteLine(closure.i);
	}


	/// <summary>
	/// 当同时在一个方法体里存在 lambda 表达式和本地函数时，会将本地函数降级为和 lambda 表达式一起存入 sealed class 版本的闭包里。
	/// </summary>
	private sealed class Closure
	{
		/// <summary>
		/// 捕获的变量会提升为类的字段。
		/// </summary>
		public int i;


		public void action()
		{
			i *= 3;
		}

		public void localFunction()
		{
			i *= 2;
		}
	}
}
