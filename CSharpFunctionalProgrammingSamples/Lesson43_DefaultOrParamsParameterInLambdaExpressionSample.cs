#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// C# 12 里允许 lambda 表达式定义带 <see langword="params"/> 修饰符的参数和带默认赋值的可选参数。
/// </summary>
internal sealed class Lesson43_DefaultOrParamsParameterInLambdaExpressionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// C# 12 允许 lambda 声明的时候带有可选参数和 params 修饰过的参数。
		// 凡是带有这两种情况的 lambda 表达式都不能被传统的 Action 系列和 Func 系列委托所覆盖，
		// 所以它会在底层声明一个匿名委托类型。
		// 但是你也可以赋值给一个兼容的 Action 或 Func 系列的委托作为实际接收的临时变量，但是这样语法就退化了，
		// 也就是说，这样就不能使用默认赋值和 params 变长参数赋值的相关语法。

		//
		// 1. 带可选参数的 lambda 表达式。
		//
		var defaultParameterLambda = static (int i = 42) => Console.WriteLine(i);
		defaultParameterLambda(100); // 显式指定参数值（100）。
		defaultParameterLambda(); // 使用默认赋值（42）。

		//
		// 2. 带 params 修饰符的参数的 lambda 表达式。
		//
		var paramsParameterLambda = static (Func<int, int, int> comparer, params int[] array) =>
		{
			// 打印初始序列。
			Console.WriteLine($"[{string.Join(',', array)}]");

			// 进行排序。
			for (var i = 0; i < array.Length - 1; i++)
			{
				for (var j = 0; j < array.Length - 1 - i; j++)
				{
					if (comparer(array[j], array[j + 1]) >= 0)
					{
						(array[j], array[j + 1]) = (array[j + 1], array[j]);
					}
				}
			}

			// 打印结果。
			Console.WriteLine($"[{string.Join(',', array)}]");
		};
		paramsParameterLambda(static (left, right) => left.CompareTo(right)); // 传入了一个空数组。
		paramsParameterLambda(static (left, right) => left.CompareTo(right), 3, 8, 1, 6, 5, 4, 7, 2, 9); // 传入了一个数组。
	}
}
