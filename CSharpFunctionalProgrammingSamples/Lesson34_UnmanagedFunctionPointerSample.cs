#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 非托管函数指针。
/// </summary>
internal sealed unsafe class Lesson34_UnmanagedFunctionPointerSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		var arr = new[] { 3, 8, 1, 6, 5, 4, 7, 2, 9 };
		Console.WriteLine($"[{string.Join(',', arr)}]");
		fixed (int* ptr = arr)
		{
			//BubbleSort(ptr, arr.Length, static (left, right) => left - right);
			BubbleSortUnmanaged(ptr, arr.Length, &cmp);
		}
		Console.WriteLine($"[{string.Join(',', arr)}]");


		[UnmanagedCallersOnly]
		static int cmp(int left, int right) => left - right;
	}


	[DllImport("CSharpFunctionalProgrammingSamples.Lesson34.dll", EntryPoint = "bubble_sort")]
	private static extern void BubbleSort(int* arr, int length, UnmanagedValueComparer comparer);

	[DllImport("CSharpFunctionalProgrammingSamples.Lesson34.dll", EntryPoint = "bubble_sort")]
	private static extern void BubbleSortUnmanaged(int* arr, int length, delegate* unmanaged<int, int, int> comparer);
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate int UnmanagedValueComparer(int left, int right);
