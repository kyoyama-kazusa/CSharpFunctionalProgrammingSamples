#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// C# 10 的第一个 Lambda 设计更新：Lambda 表达式的类型推断机制。
/// </summary>
internal sealed class Lesson38_LambdaNaturalTypeSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// C# 10 类型推断（自然类型）：
		// 在明确知晓参数和返回值类型的时候，编译器会自动推断 lambda 表达式的类型，
		// 进而省略声明委托类型实例赋值的写法，可改用 var 关键字描述。

		// 标准用例——省略声明变量的实际类型，改用 var 修饰。
		var integerComparer = static (int left, int right) => left.CompareTo(right);

		// 带 ref 参数的例子。
		var swapper = static (ref int left, ref int right) =>
		{
			var temp = left;
			left = right;
			right = temp;
		};

		// 带 out 参数的例子。
		var integerParser = static (string? s, out int result) => int.TryParse(s, out result);

		// 带重载的方法不能使用这个机制的，因为编译器无法预先推断出这个方法组具体调用的是哪一个方法。
		//var method = int.Parse; // int.Parse 有重载版本，所以不能这么用（无法精确定位到固定的某一个方法调用）。
	}
}
