namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

/// <summary>
/// 利用管道符 <c>|</c> 构造复合函数。
/// </summary>
internal sealed class Lesson47_PipelineFunctionCompositionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// sort -> arrayStringCreator -> Console.WriteLine
		int[] input = [3, 8, 1, 6, 5, 4, 7, 2, 9];

		////                           f          ( g  (  x  ))
		//string resultString = arrayStringCreator(sort(input));
		////                    ~~~~~~~~~~~~~~~~~~ ~~~~ ~~~~~
		//Console.WriteLine(resultString);

		// 管道符取消括号：
		// a(b(c(x))) 变为 x -> c -> b -> a
		// 假如我们定义 x | c 等效于 c(x)
		// 那么上面这个写法可以写成 x | c | b | a

		var f = arrayStringCreator;
		var g = sort;
		_ = input | g | f | Console.WriteLine;

		// Bash
		// < args.txt 这个表示将 args.txt 文件里的内容作为参数传入控制流处理；
		// > output.txt 这个表示将结果导出打印到 output.txt 文件里；
		// | 表示将操作串起来。在命令行里竖线 | 称为管道符（pipeline operator）。


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
	extension<TIn, TInterim, TOut>(Func<TIn, TInterim>)
	{
		public static Func<TIn, TOut> operator |(Func<TIn, TInterim> g, Func<TInterim, TOut> f)
		{
			return x => f(g(x));
		}
	}

	extension<TIn>(TIn)
	{
		public static bool operator |(TIn x, Action<TIn> f)
		{
			f(x);
			return true;
		}
	}

	extension<TIn, TOut>(TIn)
	{
		public static TOut operator |(TIn x, Func<TIn, TOut> f)
		{
			return f(x);
		}
	}
}
