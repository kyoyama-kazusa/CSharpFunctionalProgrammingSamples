namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 静态 lambda 表达式。
/// </summary>
internal sealed class Lesson29_StaticLambdaExpressionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 静态 lambda 表达式：不捕获任何外部变量的 lambda 表达式。
		//Func<int, bool> isOdd = static value => value % 2 == 1;

		// 有 static 修饰符的 lambda 表达式会有特殊处理，在 JIT 层会有特殊优化，性能会比一般委托要高很多。
		// 举个例子：
		var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		var oddNumbers = array.Where(static value => value % 2 == 1); // 建议用这个 LINQ 调用格式来使用 static lambda 表达式达到优化。
		var oddNumbers2 = from value in array where value % 2 == 1 select value; // 不建议 - 因为没有（没办法）进行 static lambda 优化。
		foreach (var element in oddNumbers2)
		{
			Console.WriteLine(element);
		}

		// 匿名函数支持 static 修饰。
		var oddNumbers3 = array.Where(
			static delegate (int value)
			{
				return value % 2 == 1;
			}
		);

		// 本地函数也支持 static 修饰。
		var oddNumbers4 = array.Where(localOddFilter);


		static bool localOddFilter(int value) => value % 2 == 1;
	}
}
