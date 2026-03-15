#pragma warning disable IDE0051

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 匿名类型和 <see langword="let"/> 查询表达式从句。
/// </summary>
internal sealed class Lesson20_LetClauseAndAnonymousTypeSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 取出里面的质数（素数）。
		int[] array = [2, 3, 9, 16, 91, 97];

		// 查询表达式实现。
		//var primes =
		//	from element in array
		//	let factors = GetFactors(element)
		//	where factors.All(factor => element % factor != 0)
		//	select element;

		// 完整写法。
		var primes = array
			// 语法为 new { } 的格式的内容我们称为匿名类型（Anonymous Type）。
			// 匿名类型是 C# 3 的，它专门用于辅助产生临时变量表达（尤其是在查询表达式里使用最多）。
			// let 语句由于不支持一个特殊方法的执行等价，所以被翻译为了 Select 方法的调用，并使用存储到匿名类型的方式来表达临时变量的过程。
			// 但是在 C# 7 开始就有值元组（Value Tuple）了，语法变得更为轻便：(element, factors: ...)，
			// 于是匿名类型就只会出现在查询表达式里了。
			.Select(element => new { element, factors = GetFactors(element) })
			.Where(pair => pair.factors.All(factor => pair.element % factor != 0))
			.Select(pair => pair.element);

		foreach (var element in primes)
		{
			Console.WriteLine(element);
		}
	}

	private static IEnumerable<int> GetFactors(int n)
	{
		if (n < 2)
		{
			throw new NotSupportedException("因为是质数判断，所以 n 必须至少为 2。");
		}

		yield return 2;
		if (n == 2)
		{
			yield break;
		}

		for (var i = 3; i <= Math.Sqrt(n); i += 2)
		{
			yield return i;
		}
	}

	/// <summary>
	/// 纯循环实现计算质数。
	/// </summary>
	/// <param name="elements">等待找质数的集合。</param>
	/// <returns>参数里是质数的所有元素构成的集合。</returns>
	private static IEnumerable<int> GetPrimes(IEnumerable<int> elements)
	{
		// 纯循环的实现。
		foreach (var element in elements)
		{
			// 迭代每一个从 2 到 根号 n 的正整数，然后和原始数字 element 进行除法取模运算。
			// 如果能被整除（余数为 0），说明这个数一定可以作为它的其中一种拆分形式的其中一个数字（m * n 的 m 或者 n），此时它就不是质数。
			var isPrime = true;
			for (var factor = 2; factor <= Math.Sqrt(element); factor += factor == 2 ? 1 : 2)
			{
				if (element % factor == 0)
				{
					isPrime = false;
					break;
				}
			}
			if (isPrime)
			{
				yield return element;
			}
		}
	}
}
