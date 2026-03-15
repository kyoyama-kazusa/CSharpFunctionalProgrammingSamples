#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 托管函数指针。
/// </summary>
internal sealed class Lesson33_ManagedFunctionPointerSample : Sample
{
	/// <inheritdoc/>
	public override unsafe void RunSample()
	{
		// 函数指针：存函数的地址。
		// 函数指针为了更加快捷地进行间接访问函数的机制，避免委托产生不必要的开销。
		var arr = new[] { 3, 8, 1, 6, 5, 4, 7, 2, 9 };
		Console.WriteLine($"[{string.Join(',', arr)}]");
		//bubbleSort(arr, static (left, right) => left - right);
		bubbleSort2(arr, &cmp);
		Console.WriteLine($"[{string.Join(',', arr)}]");


		static int cmp(int left, int right) => left - right;

		// 使用委托类型实例来参与比较。
		static void bubbleSort(int[] arr, ValueComparer comparer)
		{
			for (var i = 0; i < arr.Length - 1; i++)
			{
				for (var j = 0; j < arr.Length - 1 - i; j++)
				{
					if (comparer(arr[j], arr[j + 1]) >= 0)
					{
						(arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
					}
				}
			}
		}

		// 使用函数指针来参与比较。
		// managed 关键字可以省略（即 delegate*<int, int, int>）。
		static unsafe void bubbleSort2(int[] arr, delegate* managed<int, int, int> comparer)
		{
			for (var i = 0; i < arr.Length - 1; i++)
			{
				for (var j = 0; j < arr.Length - 1 - i; j++)
				{
					if (comparer(arr[j], arr[j + 1]) >= 0)
					{
						(arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
					}
				}
			}
		}
	}
}

internal delegate int ValueComparer(int left, int right);
