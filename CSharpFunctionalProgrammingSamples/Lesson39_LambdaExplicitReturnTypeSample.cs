#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// C# 10 的第二个 Lambda 设计更新：Lambda 显式指定返回值类型。
/// </summary>
internal sealed class Lesson39_LambdaExplicitReturnTypeSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 类型推断增强：支持返回值类型的显式指定。

		// 基础用法。
		var byteComparer = static long (byte left, byte right) => left.CompareTo(right);
		//var functionReturner = static () => Console.WriteLine; // 不指定会报错（因为方法是有重载的）。
		var functionReturner = static Action () => Console.WriteLine; // 这样就不会报错了。

		// 字面量一般会有一个自然类型，但自然类型不符合预期的时候也可以使用。
		var shortReturner = static short () => 42; // 整数字面量一般都是 int 类型，但是也可以用指定显式类型的方式来确定它的实际类型。

		// 一般会用在返回值类型和实际返回的类型不匹配，但支持隐式转换的时候。
		// 比如说 int -> long 有隐式转换机制，但有些时候的 lambda 表达式的返回值需要是 long，但自然类型推断出来是 int 作为返回值的时候。
		// 再比如不可空到可空类型。
		var nullableIntegerReturner = static int? (int value) => value;
		var nullableReferenceTypeReturner = static string? (string s) => s;

		// 或者是参数类型是没办法 Action/Func 表示的类型（如这里的指针类型）。
		unsafe
		{
			var unsafeType = static void* (int* original) => original;
			var functionPointerReturner = static delegate*<void> () => &Console.WriteLine;
		}

		// 要注意的是，带返回值的语法只支持带小括号的参数表列。如果单参数的时候是不允许的。
		//Func<int, short> method = short value => 42; // 报错。
		Func<int, short> method = short (value) => 42; // 可以。
	}
}
