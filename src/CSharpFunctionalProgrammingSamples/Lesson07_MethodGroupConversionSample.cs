namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 委托方法组转换的例子。
/// </summary>
internal sealed class Lesson07_MethodGroupConversionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 方法组（Method Group）：表示在使用 new 委托(函数名) 语法的时候的那个函数名。
		// 它往往在 API 里有重载函数（同名但参数类型、参数个数有些许不同的情况），
		// 所以只凭借一个函数名（方法名）我们无法辨识它的具体类型，所以它代指的是一个方法组，所以叫方法组。
		Action writeLineCallback = Console.WriteLine;
		Console.Write("a");
		writeLineCallback(); // 委托推断，可以省略 .Invoke。
		Console.Write("b");
	}
}
