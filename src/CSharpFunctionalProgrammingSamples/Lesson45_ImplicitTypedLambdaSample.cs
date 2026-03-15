#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// C# 14 带修饰符的参数的 lambda 可以省略类型。
/// </summary>
internal sealed class Lesson45_ImplicitTypedLambdaSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// swapper 委托实例使用 lambda 表达式的时候，因为参数带有 ref 修饰符，所以不允许省略参数类型 int。
		// C# 14 开始允许所有带有修饰符的 lambda 表达式省略参数类型。
		// 要注意的是，如果 lambda 表达式省略了参数类型，那么参数类型必须要么同时省略，要么就都不省略。
		Swapper<int> swapper = static (ref left, ref right) =>
		{
			var temp = left;
			left = right;
			right = temp;
		};

		int a = 42, b = 100;
		swapper(ref a, ref b);

		// 罗列一些情况。
		var integer = 42;
		int variable;
		InParam inParam = static (in value) => Console.WriteLine(value);
		inParam(integer);

		OutParam outParam = static (out value) => value = 42;
		outParam(out variable);

		RefParam refParam = static (ref value) => value += 42;
		refParam(ref integer);

		RefReadOnlyParam refReadOnlyParam = static (ref readonly value) => Console.WriteLine(value);
		refReadOnlyParam(in variable);

		ScopedParam scopedParam = static (scoped values) => Console.WriteLine(values.Length);
		scopedParam([1, 2, 3]);

		// 具有 params 修饰符的 lambda 表达式是直接不写 params 修饰符的。
		ParamsParam paramsParam = static values => Console.WriteLine(values.Length);
		paramsParam(1, 2, 3);
	}
}

file delegate void Swapper<T>(ref T left, ref T right);
file delegate void InParam(in int value);
file delegate void OutParam(out int value);
file delegate void RefParam(ref int value);
file delegate void RefReadOnlyParam(ref readonly int value);
file delegate void ScopedParam(scoped ReadOnlySpan<int> values);
file delegate void ParamsParam(params ReadOnlySpan<int> values);
