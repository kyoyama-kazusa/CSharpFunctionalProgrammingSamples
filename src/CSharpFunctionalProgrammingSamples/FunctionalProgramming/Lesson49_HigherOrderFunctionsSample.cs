namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

/// <summary>
/// 高阶函数。
/// </summary>
internal sealed class Lesson49_HigherOrderFunctionsSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 求和 - 将每个元素加起来，操作清晰，所以它不是高阶函数。
		var values = new[] { 3, 8, 1, 6, 5, 4, 7, 2, 9 };
		var sum = values.Sum();
		Console.WriteLine(sum);

		// Aggregate 在这里就是一个高阶函数，因为它允许我们自定义数值聚合的行为。
		// 用法 1：求累乘结果。
		var product = values.Aggregate(1, static (interim, next) => interim * next);
		Console.WriteLine(product);

		// 用法 2：叠加枚举。
		var directions = (Direction[])[Direction.Up, Direction.Left, Direction.Right, Direction.RightUp, Direction.LeftDown];
		var directionValue = directions.Aggregate(static (interim, next) => interim | next);
		Console.WriteLine(directionValue.ToString());
	}
}

[Flags]
file enum Direction { Up = 1, Down = 2, Left = 4, Right = 8, LeftUp = 16, RightUp = 32, LeftDown = 64, RightDown = 128 }
