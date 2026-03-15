#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 本地函数的捕获。
/// </summary>
internal sealed class Lesson27_LocalFunctionCaptureSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 捕获变量 - 在本地函数里使用本地函数的函数体之外的变量信息的行为。
		//int i = 42;
		//Console.WriteLine(i);
		//localFunction();
		//Console.WriteLine(i);


		//void localFunction() => i *= 2;

		// 下面这段代码是上面代码的底层翻译后的结果。
		// 捕获变量在本地函数里会被翻译为使用结构操作的闭包（struct 版本闭包）。
		// 这种闭包在正确使用的时候肯定是会在栈内存分配的，它不走 GC，所以不需要引用类型那种复杂的内存分配机制。
		// 从性能上来说，struct 闭包肯定比 sealed class 闭包（lambda 表达式的底层闭包）性能要好。
		var closure = new Closure();
		closure.i = 42;
		Console.WriteLine(closure.i);
		LocalFunctionClosure(ref closure);
		Console.WriteLine(closure.i);
	}


	/// <summary>
	/// 这个是本地函数在带有闭包的时候的翻译（实体方法，带 <see langword="ref"/> 修饰符的静态函数）。
	/// </summary>
	/// <param name="closure">闭包，这里要传 <see langword="ref"/> 来保证和原始的值类型实例是同一个。</param>
	private static void LocalFunctionClosure(ref Closure closure)
	{
		closure.i *= 2;
	}


	/// <summary>
	/// 本地函数的闭包。是一个结构。
	/// </summary>
	private struct Closure
	{
		/// <summary>
		/// 捕获的变量会放在闭包里，仍然是一个 <see langword="public"/> 的字段。
		/// </summary>
		public int i;
	}
}
