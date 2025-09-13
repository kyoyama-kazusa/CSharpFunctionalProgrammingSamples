namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 本地函数。
/// </summary>
internal sealed class Lesson26_LocalFunctionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		int[] array = [3, 8, 1, 6, 5, 4, 7, 2, 9];
		sort(array, compare);
		Console.WriteLine($"[{string.Join(", ", array)}]");


		// 声明一个本地函数。
		// 本地函数建议使用驼峰命名法（用帕斯卡命名法也可以，不过尽量保证项目内命名规则统一）。
		// 并且永远定义到和执行逻辑的下方，且中间有隔断（用空行隔断）。
		// 一般 1 个空行就行，我个人建议用 2 个空行，因为 1 个空行也可能只是普通的分隔分步骤执行逻辑的过程。
		// 在 JetBrains Rider 里会显示让用户定义本地函数时用 return 和 continue 语句隔断，这也是出于这种考虑。
		void sort<T>(T[] array, Func<T, T, int> comparison)
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

		int compare(int left, int right) => left - right;
	}
}
