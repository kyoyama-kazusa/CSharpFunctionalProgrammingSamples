namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 不建议使用的两种情况：
/// <list type="bullet">
/// <item>在 <see cref="Task(Action)"/> 传入 async lambda 表达式</item>
/// <item>在 <see cref="Task.Run(Func{Task?})"/> 传入使用非 CPU 密集型操作的 async lambda 表达式</item>
/// </list>
/// </summary>
internal sealed class Lesson24_AsyncLambdaAsArgumentInNewTaskSample : Sample
{
	/// <inheritdoc/>
	public override async void RunSample() => await RunSampleCoreAsync();


	private static async Task RunSampleCoreAsync()
	{
		// 1. new Task 构造器传参传入 async lambda，不太建议这么用。
		var task = new Task(
			// lambda 表达式在这里是异步的，但是 Task 的构造器参数均是用 Action 委托类型实例接收，
			// 这意味着我们使用的 task 是 async void 的（Action -> void 返回值）。
			async () =>
			{
				await Task.Delay(1000); // 等待 1 秒。
				Console.WriteLine("Hello, world!"); // 1 秒之后打印一行文字。
			}
		);
		task.Start(); // 启动 task。

		// 强行等待一下，但是毫无作用。
		// 因为内部的 async lambda 是 async void 效果的，我们无法知晓状态，所以外部的 task 实例启动了内部的 async void 的方法调用
		// （这个 async lambda 表达式）之后就会立刻完成（但是实际压根就没完成）。
		await task;


		// 2. Task.Run 传入 async lambda 在一些非绑定 CPU 操作（CPU 密集型操作）的时候也不建议。
		// Task 的底层是使用线程池线程资源来执行异步操作的。但是异步操作不等于说是多线程技术，换言之，异步操作有多种处理模式：
		//     * 使用线程池资源来执行异步操作；
		//     * I/O 操作（异步 I/O）操作，这种走的系统 API 执行的异步行为，跟线程池和多线程关系不大；
		//     * 定时器操作，这种也不是走的多线程技术，而是用的系统的定时器 API 绑定执行逻辑。
		var task2 = Task.Run(
			async () =>
			{
				await Task.Delay(1000); // 等待 1 秒。
				Console.WriteLine("Hello, world!"); // 1 秒之后打印一行文字。
			}
		);
		await task2;


		// 如果有混合使用的（非密集型和密集型搭配用的），这种包裹一层 Task.Run 就是可以的。
		// 因为在不包裹 Task.Run 的时候（在外面直接调用这个执行流程）可能仍然会阻塞主线程。
		var task3 = Task.Run(
			async () =>
			{
				DoHeavyOperation(); // 密集型操作执行。
				await Task.Delay(1000); // 等待 1 秒（非密集型操作在这里也 OK）。
				Console.WriteLine("执行完成"); // 1 秒之后打印一行文字。
			}
		);
		await task3;
	}

	private static void DoHeavyOperation()
	{
	}
}
