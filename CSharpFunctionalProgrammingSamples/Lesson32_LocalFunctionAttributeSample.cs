namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 本地函数允许标注特性。
/// </summary>
internal sealed class Lesson32_LocalFunctionAttributeSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		int a = 10, b = 42;
		Console.WriteLine($"{nameof(a)} = {a}, {nameof(b)} = {b}");
		(a, b) = (b, a);
		Console.WriteLine($"{nameof(a)} = {a}, {nameof(b)} = {b}");


#pragma warning disable CS8321
		[Obsolete("方法已经过时。因为你可以直接使用元组交换来完成相同功能，这个方法已经废弃。", false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static void swap<T>(ref T left, ref T right) where T : struct
		{
			var temp = left;
			left = right;
			right = temp;
		}
#pragma warning restore CS8321
	}
}
