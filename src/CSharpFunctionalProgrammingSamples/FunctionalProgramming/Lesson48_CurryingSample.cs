#pragma warning disable

namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

/// <summary>
/// 柯里化：<c>f(a, b, c)</c> 转化为 <c>f(a)(b)(c)</c>。
/// </summary>
internal sealed class Lesson48_CurryingSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 两层的柯里化
		{
			Func<int, int, int> originalAdder = (a, b) => a + b;
			Func<int, Func<int, int>> curryingAdder = (a) => (b) => a + b;

			var f = originalAdder;
			var g = curryingAdder;
			var result1 = f(1, 2); // 普通委托调用：多参数函数。
			Console.WriteLine(result1);

			var result2 = g(1)(2); // 柯里化函数调用：将多个参数改成内外层 lambda 表达式的迭代传参的函数调用。
			Console.WriteLine(result2);
		}

		// 三层的柯里化
		{
			Func<int, int, int, int> complexAdder = (a, b, c) => a + b + c;
			Func<int, Func<int, Func<int, int>>> complexCurryingAdder = (a) => (b) => (c) => a + b + c;
			var f = complexCurryingAdder;
			var result = f(1)(2)(3);
			Console.WriteLine(result);
		}
	}
}
