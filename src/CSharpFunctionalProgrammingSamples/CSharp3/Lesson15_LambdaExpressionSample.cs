#pragma warning disable IDE0039, IDE0053, IDE0200, IDE0350

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// Lambda 表达式。
/// </summary>
internal sealed class Lesson15_LambdaExpressionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 胖箭头 =>
		// 瘦箭头 ->（用于 Java 的 lambda 表达式，C# 里的指针对象读取成员，等价于 (*p).Property）
		// C# 里 lambda 表达式用的是胖箭头。

		// lambda 表达式是允许类型推断的，参数的类型是可以省略的（但是带 ref、out 修饰符之后就不能省略了）：
		//     (int value) 可以变为 (value)
		// lambda 表达式如果是单参数的话，小括号也可以不写：
		//     (value) 可以变为 value
		// lambda 表达式如果在执行块里，如果只有一个执行逻辑（一个函数调用等等，不带 if、for 这些复杂处理的），
		// 或 return 语句，还可以进一步简写：
		//     { return ... } 可以变为 => ...
		//     { DoSomething(); } 可以变为 => DoSomething()
		Action defaultAction = () => Console.WriteLine("Hello, world");
		Action<int> lambda1_NoReturn = value => Console.WriteLine(value);
		Func<int, int> lambda2_ReturnInt32 = value => value + 42;
		LambdaExpression3_WithModifier lambda3_ComplexDeclaration = (ref int value1, out int value2) =>
		{
			value1 += 42;
			value2 = value1;
		};

		// 使用前面样例的三个 lambda 表达式。
		int a = 10, b;
		defaultAction();
		lambda1_NoReturn(42);
		lambda2_ReturnInt32(42);
		lambda3_ComplexDeclaration(ref a, out b);
		Console.WriteLine(a);
		Console.WriteLine(b);

		// 函数调用也可以使用 lambda 表达式。
		int[] array = [3, 8, 1, 6, 5, 4, 7, 2, 9];
		Sort(array, (x, y) => x - y);
		Console.WriteLine($"[{string.Join(", ", array)}]");
	}


	private static void Sort<T>(T[] array, Func<T, T, int> comparison)
	{
		for (var i = 0; i < array.Length - 1; i++)
		{
			for (var j = 0; j < array.Length - 1 - i; j++)
			{
				if (comparison(array[j], array[j + 1]) >= 0)
				{
					(array[j], array[j + 1]) = (array[j + 1], array[j]);
				}
			}
		}
	}
}

file delegate void LambdaExpression3_WithModifier(ref int value1, out int value2);