#pragma warning disable IDE0001

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 在方法组上指定泛型参数。尽管这很少使用。
/// </summary>
internal sealed class Lesson08_GenericMethodGroupConversionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 求一个锯齿数组每一个元素（一维数组）的元素个数的和。
		int[] a = [1, 2, 3];
		int[] b = [4, 5, 6, 7];
		int[] c = [8, 9];
		int[] d = [];
		int[][] arrays = [a, b, c, d];

		// 在极少数情况下（确实不多，很罕见的用法），在指定方法组的时候泛型参数无法被编译器正常推断出来，
		// 这个时候可能会需要你手动为方法组指定实际的数据类型，比如这里的 Method<T> 的语法。
		// 这种写法在 C# 里是允许的，它表示给泛型方法作为委托方法组转换语法作了一个特殊支持格式。
		// 但是要注意，如果泛型参数有多个的话，是需要全部泛型参数都指定出类型的，即使其中确实有一部分不需要指定（可以被推断），
		// 但是 C# 在设计多泛型参数的推断的时候一直需要要么全都不指定，要么就必须全都指定。
		int sumResult = GetSum(arrays, ArrayConverter<int>);
		Console.WriteLine(sumResult);
	}

	/// <summary>
	/// 计算一个泛型类型的数组的和，指定计算求和的元素过程。
	/// </summary>
	/// <typeparam name="T">表示数组的元素的类型。</typeparam>
	/// <param name="array">数组。</param>
	/// <param name="converter">转换器方法，将每一个元素映射为一个合适的、用于求和的 <see cref="int"/> 数值。</param>
	/// <returns>将映射后的每一个 <see cref="int"/> 结果求和得到的结果。</returns>
	public static int GetSum<T>(T[] array, Func<T, int> converter)
	{
		var result = 0;
		foreach (var element in array)
		{
			result += converter(element);
		}
		return result;
	}

	private static int ArrayConverter<T>(T[] values)
	{
		return values.Length;
	}
}
