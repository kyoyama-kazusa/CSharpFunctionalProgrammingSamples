#pragma warning disable

using System.Diagnostics.CodeAnalysis;

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// Lambda 表达式在 C# 10 特性优化的时候，对单参数 lambda 表达式的支持的情况需要特殊注意。
/// </summary>
internal sealed class Lesson41_LambdaSingleParameterSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 单参数 lambda 表达式。
		// 如果 lambda 表达式是单参数的，并且类型可以被推断出来不用显式指定，就可以不写小括号直接写参数，
		// 即 (param) => {} 可以直接写成 param => {}。
		Func<string?, bool> lambdaOriginal = name =>
		{
			if (name is null)
			{
				return false;
			}

			Console.WriteLine($"Hello, {name}!");
			return true;
		};

		// 当如果是单参数 lambda 表达式的时候，lambda 的任何地方有特性标记，或者是 lambda 显式指定了返回值类型，
		// 这个时候参数的小括号不可省略。
		Func<string?, bool> lambda = ([NotNullWhen(true)] name) =>
		{
			if (name is null)
			{
				return false;
			}

			Console.WriteLine($"Hello, {name}!");
			return true;
		};
	}
}
