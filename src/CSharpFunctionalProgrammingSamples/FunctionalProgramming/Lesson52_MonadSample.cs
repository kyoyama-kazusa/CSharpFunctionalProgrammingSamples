namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

/// <summary>
/// Monad 机制。
/// </summary>
internal sealed class Lesson52_MonadSample : Sample
{
	/// <inheritdoc/>
	public override async void RunSample()
	{
		// Monad：将数据包裹一层（“上下文”），然后只用操作这个上下文进行统一语法层面的处理，最终拆解上下文拿取里面的数据作为结果。
		// 一般的函数处理流程是 T -> U -> V -> W
		// monad 的处理流程是 T -> Box(U) -> Box(V) -> Box(W) -> W

		// 1. LINQ（Enumerable 类型里提供的那些方法 / 查询表达式）。
		var sequence = new List<int> { 1, 2, 8, 14, 22, 23, 79 };
		var oddNumbersGreaterThanForty = sequence
			.Where(static value => value % 2 == 1)
			.Where(static value => value > 40)
			.ToArray(); // List<int> -> Enumerable(List<int>) -> Enumerable(集合<int>) -> int[]

		// 2. await 语句（搭配 Task<T>、ValueTask<T> 的时候）。
		var task = new Task<int>(() => 42);
		task.Start(); // 仅为示意（这里只是想体现 await 语句拆解上下文获取里面的结果的效果，是 monad 的一种体现）。
		var resultValueOfTask = await task; // Func<T> -> Task(Func<T>) -> T

		// 3. Nullable<T>（可空值类型 T?）。
		int? nullableInt32 = 42;
		var strOfNullableInt32 = nullableInt32?.ToString() ?? "<null>"; // int -> int? -> string? -> string
		Console.WriteLine(strOfNullableInt32);
	}
}
