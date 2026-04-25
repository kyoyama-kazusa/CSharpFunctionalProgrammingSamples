namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

/// <summary>
/// Y-组合子。
/// </summary>
internal sealed class Lesson53_YCombinatorSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		var factorial = yCombinator(factorial => value => value == 1 ? 1 : factorial(value - 1) * value);
		var result = factorial(3);
		Console.WriteLine(result.ToString()); // 6


		static Func<long, long> yCombinator(Func<Func<long, long>, Func<long, long>> function)
			=> value => function(yCombinator(function))(value);
	}
}
