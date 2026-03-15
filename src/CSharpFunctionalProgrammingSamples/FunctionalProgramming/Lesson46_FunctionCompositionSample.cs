#pragma warning disable

namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

using static Extensions;

/// <summary>
/// 复合函数。
/// </summary>
internal sealed class Lesson46_FunctionCompositionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 复合函数：f(g(x))。

		// sort -> arrayStringCreator -> Console.WriteLine
		int[] input = [3, 8, 1, 6, 5, 4, 7, 2, 9];

		//                           f          ( g  (  x  ))
		string resultString = arrayStringCreator(sort(input));
		//                    ~~~~~~~~~~~~~~~~~~ ~~~~ ~~~~~
		Console.WriteLine(resultString);

		// 复合函数的构造。
		var f = arrayStringCreator;
		var g = sort;
		var h = Compose(f, g);
		var output = h(input); // 结果类型一定是 string
		Console.WriteLine(output);


		static int[] sort(int[] original)
		{
			var result = original[..];
			Array.Sort(result);
			return result;
		}

		static string arrayStringCreator(int[] original) => $"[{string.Join(',', original)}]";
	}
}

file static class Extensions
{
	public static Func<TSource, TResult> Compose<TSource, TInterim, TResult>(Func<TInterim, TResult> f, Func<TSource, TInterim> g)
	{
		// 函数进行复合。
		return x => f(g(x));
	}
}
