#pragma warning disable

using System.Net.Http;

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 异步 lambda 表达式。
/// </summary>
internal sealed class Lesson22_AsyncLambdaExpressionSample : Sample
{
	/// <inheritdoc/>
	public override async void RunSample() => await RunSampleCoreAsync();


	private static async Task RunSampleCoreAsync()
	{
		// 异步 lambda 表达式。
		Func<Task> fetcher = async () =>
		{
			using var httpClient = new HttpClient();
			try
			{
				var html = await httpClient.GetStringAsync("https://example.com/");
				Console.WriteLine(html);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"错误信息：{ex.Message}");
			}
		};

		// 执行异步 lambda 表达式 - 这个时候才会执行异步 lambda 表达式。
		await fetcher();
	}
}
