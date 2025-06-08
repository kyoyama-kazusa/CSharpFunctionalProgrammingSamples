namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 递归匿名函数。
/// </summary>
internal sealed class Lesson14_RecursionAnonymousFunctionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 青春版。
		Func<BigInteger, BigInteger> f1 = null!;
		f1 = delegate (BigInteger n) { return n == 1 ? 1 : f1(n - 1) * n; };
		Console.WriteLine(f1(5).ToString()); // 120

		// 专业版（柯里化实现）。
		Func<BigInteger, BigInteger> f2 = YCombinator(
			delegate (Func<BigInteger, BigInteger> g)
			{
				return delegate (BigInteger n) { return n == 1 ? 1 : g(n - 1) * n; };
			}
		);
		Console.WriteLine(f2(6).ToString()); // 720

		// C# 3 的 lambda 表达式可以将匿名函数进一步进行语法简化，但是声明等信息也不能丢失。
		Func<BigInteger, BigInteger> f3 = YCombinator(g => n => n == 1 ? 1 : g(n - 1) * n);
		Console.WriteLine(f3(6).ToString()); // 720
	}


	private static Func<BigInteger, BigInteger> YCombinator(Func<Func<BigInteger, BigInteger>, Func<BigInteger, BigInteger>> function)
	{
		// Y-组合子（Y-Combinator）。抽象的写法是这样的：f(Y(f))。
		// 就是为了解决递归匿名函数而产生的一个特殊用法。
		// 这个东西产生自一个学科，叫 lambda 演算（λ-Calculus）。f(x) 记作 λf.x
		return delegate (BigInteger n)
		{
			return function(YCombinator(function))(n);
		};
	}
}
