#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// C# 13 方法组绑定优化 - 主要针对的是 <c>实例.Method</c> 语法在绑定方法组上的优化。
/// </summary>
internal sealed class Lesson44_MethodGroupImprovementsSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 实例方法组。

		//
		// 情况 1
		//
		// 早期（C# 13 之前），编译器是会报错的 - 因为实例方法和扩展方法的语法调用上是一样的写法。
		// C# 13 起规避了这个问题 - 它会先检查实例方法后扩展方法，有个优先级次序的解析，所以 Method1 优先绑定实例方法。
		var method1 = new C().Method1;

		//
		// 情况 2
		//
		// 早期（C# 13 之前），编译器是会报错的 - 因为无泛型参数和有泛型参数的实例方法，在方法组声明语法上来说是一样的写法
		// （除非你给泛型参数显式指定参数）。
		// C# 13 起规避了这个问题 - 因为带有泛型参数的方法不指定参数时作为方法组绑定的行为是不受 C# 类型生态允许的，会被编译器直接丢掉。
		var method2 = new C().Method2;

		//
		// 情况 3
		//
		// 早期（C# 13 之前），编译器是会报错的 - 带有泛型参数约束的方法和普通的方法混在一起的时候，编译器无法区分调用哪一个。
		// C# 13 起规避了这个问题 - 因为泛型约束编译期间可以判定是否类型成立，所以会立马丢掉不能成立的方法作为匹配，
		// 所以绑定范围会变小，也就规避了歧义。
		var method3 = new C().Method3; // 稳定调用实例方法（因为带泛型参数约束的 Method3 扩展方法不满足，泛型约束不满足）
		//var method3_2 = 42.Method3; // 报错，42（int 类型）没实现 Dispose 方法，也不具备 Method3 实例方法成员
	}
}

file sealed class C
{
	public void Method1() => Console.WriteLine("Instance method");
	public void Method2() => Console.WriteLine("Instance method");
	public void Method2<T>() => Console.WriteLine($"Generic method of type '{typeof(T).Name}'");
	public void Method3() => Console.WriteLine("Instance method");
}

file static class Extensions
{
	public static void Method1(this C @this) => Console.WriteLine("Extension method");
	public static void Method3<T>(this T @this) where T : IDisposable
	{
		Console.WriteLine("Extension method");
		@this.Dispose();
	}
}
