namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// LINQ 表达式。
/// </summary>
internal sealed class Lesson19_LinqExpressionsSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		int[] array = [1, 2, 3, 4, 5];

		// LINQ 表达式 / 查询表达式（Query Expression）。
		// 它可以允许我们使用类似于 SQL 查询语句的方式来查询 C# 里定义的集合数据里的元素，并进行筛选。
		//var evenSequence =
		//	from element in array
		//	where element % 2 == 0
		//	select element;

		// 上面查询表达式的等价的实现。
		var evenSequence = array
			// Where 方法需要传入一个 Func<int, bool> 委托，我们把返回 bool 值的委托类型称为谓词（Predicate）。
			// 这个词来自于英语的表语（Predicative），指的是放在 be 动词（“是”的意思）或系动词（可以跟形容词的动词）之后的语法成分。
			// 在英语里，be 动词往往都表示“是”的意思，所以在这里，放在“是”之后的部件，我们都可以认定它具备一个布尔值的结果，
			// 换言之就是说这个说法最终肯定只有“对”和“错”两个结果，只不过你现在可能不知道它具体是哪一个结果而已。
			// 这个说法很像是下面这个 where 所传入的 lambda 表达式想表达的感觉，所以我们把他称为谓词。
			.Where(element => element % 2 == 0)

			// Select 方法将前面执行的返回结果（序列）一个一个映射为新的结果值。所以 Select 体现的效果是映射，把一个数值加以处理改成另外一个数值。
			// 但是这里我们只需要将刚才筛选出来的偶数直接返回，所以对于每一个偶数而言，我们都只会直接取出作为结果，所以这里的映射等于说是不用映射，
			// 直接传入的参数直接作为返回值即可，才有了 a => a 的语法形式。
			.Select(element => element);

		// 所有是偶数的部分。
		foreach (var element in evenSequence)
		{
			Console.WriteLine(element);
		}
	}
}
