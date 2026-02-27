#pragma warning disable

using System.Diagnostics.CodeAnalysis;

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// C# 10 的第三个 Lambda 设计更新：允许给 lambda 的任何部件追加特性标记。
/// </summary>
internal sealed class Lesson40_LambdaAttributeSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 给 lambda 表达式添加特性。一般会用在这几个地方：返回值、参数和 lambda 代替的那个实际方法本体上。

		// 特性加在返回值上。
		var name = "Kazusa";
		var targetStringReturner =
			[return: NotNullIfNotNull(nameof(name))] static (string? name) => name is null ? null : $"Hello, {name}!";
		var resultString = targetStringReturner(name);
		Console.WriteLine(resultString);

		// 标注在参数上。
		var rawArray = new string[42];
		rawArray.InitializeArray(static ([NotNull] ref string? element) => element = string.Empty);

		// 标注在 lambda 本体上。
		var lambdaExpression = [AnonymousMethod] () => { };

		// 匿名类型只在 C# 10 里同步更新了第一个设计（允许 var 接收 - 自然类型/类型推断，它是支持的），
		// 而后面两种语法设计（返回值类型指定和特性指定），匿名函数就不再支持了。
		//var result = delegate (ref int left, ref int right)
		//{
		//	var temp = left;
		//	left = right;
		//	right = temp;
		//};
	}
}

file static class Extensions
{
	public static void InitializeArray<T>(this T[] array, ArrayElementInitializer<T> initializer)
	{
		foreach (ref var element in array.AsSpan())
		{
			initializer(ref element);
		}
	}
}

file delegate void ArrayElementInitializer<T>([NotNull] ref T? element);

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
file sealed class AnonymousMethodAttribute : Attribute;
