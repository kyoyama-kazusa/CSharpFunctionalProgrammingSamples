#pragma warning disable

namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

/// <summary>
/// 惰性求值。
/// </summary>
internal sealed class Lesson50_LazyEvaluationSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		//// 即时求值：调用函数的时候会立马运算和返回结果。
		//var values = GetEvenNumbers();
		//foreach (var value in values)
		//{
		//	Console.WriteLine(value);
		//}

		//// 惰性求值：当你不使用 Lazy 的迭代逻辑的时候（这里的 foreach），它都不会开始迭代。
		//var sequence = GetEvenNumbers_Lazy();
		//foreach (var value in sequence)
		//{
		//	Console.WriteLine(value);
		//}

		// 无穷迭代多使用惰性求值，避免内存爆炸（即时存储会占用内存大小），而且还一直算不完。
		// 所以这个时候惰性求值也一般会搭配其他的函数辅助参与，达成一些额外操作，例如这里只取前 100 个质数。
		var primes = GetPrimeSequence();
		foreach (var prime in primes.Skip(25).Take(100))
		{
			Console.WriteLine(prime);
		}
	}


	private static IEnumerable<int> GetEvenNumbers()
	{
		var list = new List<int>();
		for (var i = 2; i <= 100; i += 2)
		{
			list.Add(i);
		}
		return list;
	}

	private static IEnumerable<int> GetEvenNumbers_Lazy()
	{
		for (var i = 2; i <= 100; i += 2)
		{
			yield return i;
		}
	}

	private static IEnumerable<int> GetPrimeSequence()
	{
		var number = 2;
		while (true)
		{
			if (isPrime(number))
			{
				yield return number;
			}
			number += number == 2 ? 1 : 2;
		}


		static bool isPrime(int number)
		{
			for (var i = 2; i * i <= number; i += i == 2 ? 1 : 2)
			{
				if (number % i == 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}
