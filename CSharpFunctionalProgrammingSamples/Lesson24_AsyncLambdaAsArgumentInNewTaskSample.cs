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
	public override async void RunSample()
	{
		// 

		// 1. 作为 new Task 参数。
		// async lambda 表达式应该使用 Func<Task> 委托类型的实例（变量）接收。可 new Task 的构造器参数全都是 Action 系列的。
		// 这意味着 Action 没有返回值，也就意味着 async lambda 被“改写成” async void 组合在使用。
		var task = new Task(
			async () =>
			{
				await Task.Delay(1000);
				Console.WriteLine("Hello, world!");
			}
		);
		task.Start();

		// 2. Task.Run 传入 async lambda 表达式。
		// Task.Run 会默认开启一个线程池线程（或者说分配一个线程池线程资源单位）去执行你内部包裹的 lambda 表达式（或者 async lambda 表达式）。
		// 但是如果你内部的 async lambda 表达式用到的资源跟线程池没有任何关系（比如 Task.Delay 会启动定时器；再比如 I/O 异步操作读写文件），
		// 这个时候，外部 Task.Run 启动的线程池线程资源就会被浪费掉（或者说空转）。
		await Task.Run(
			async () =>
			{
				await Task.Delay(1000);
				Console.WriteLine("Hello, world!");
			}
		);
	}
}
