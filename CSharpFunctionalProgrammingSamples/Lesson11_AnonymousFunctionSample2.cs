namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 匿名函数例子 2 - 热水器的那个例子。
/// </summary>
internal sealed class Lesson11_AnonymousFunctionSample2 : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		int[] array = [3, 8, 1, 6, 5, 4, 7, 2, 9];
		Sort(
			array,

			// 匿名函数声明：无需声明访问修饰符、返回值类型和函数名，只保留剩余的部分。
			delegate (int left, int right)
			{
				return left - right;
			}
		);
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
