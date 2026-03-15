#pragma warning disable IDE0053, IDE0059

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// Lambda 表达式和匿名函数的区别。
/// </summary>
internal sealed class Lesson18_DifferenceBetweenAnonymousFunctionAndLambdaExpression : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 1. 弃元机制（Discard）
		// C# 2 匿名函数原生支持参数弃元机制，但是参数必须全部弃元才能使用省略。
		// 当且仅当参数全部都不使用的时候才可以省略。
		Func<int, int> function = delegate { return 42; };
		Func<int, int, int> function2 = delegate { return 42; };

		// C# 3 的 lambda 表达式原生支持弃元，并且需要在 C# 9 的时候才能使用，早期也是不支持的。
		// 由于下划线 _ 自身是一个合法的标识符，所以为了考虑对这部分用户的兼容，单参数使用下划线仍然是普通的变量。
		// 如果是多参数（至少两个）都声明为下划线 _ 时，这些参数都会被视为弃元。
		function = _ => { return _ + 1; };
		function2 = (_, _) => { return 42; };

		// 2. 表达式直接返回
		// C# 2 的匿名函数原生就不支持将大括号简写为表达式直接返回的形式。
		//function = delegate => 42; // 错误的语法。

		// C# 3 的 lambda 表达式原生就支持这种写法。
		function = _ => _ + 1;
		function2 = (_, _) => 42;

		// 但是带有复杂逻辑控制的语句（if、switch、do-while、foreach、try-catch 这些），就不能省略块了。
		Action action = () =>
		{
			if (true)
			{
				Console.WriteLine(42);
			}
		};
	}
}