#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 泛型参数的协变性和逆变性。
/// </summary>
internal sealed class Lesson21_GenericTypeParameterVarianceSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 泛型参数允许协变性。
		IEnumerable<Dog> dogs = new List<Dog> { new Dog() };
		IEnumerable<Animal> animals = dogs; // 利用了泛型参数的协变性。

		foreach (var animal in animals)
		{
			animal.Speak();
		}

		// 泛型参数允许逆变性。
		Predicate<Animal> animalPredicate = animal => animal is Dog;
		Predicate<Dog> dogPredicate = animalPredicate; // 利用了泛型参数的逆变性。

		var dogInstance = new Dog();
		Console.WriteLine(dogPredicate(dogInstance));
	}
}

/// <summary>
/// 动物类型。
/// </summary>
file abstract class Animal
{
	/// <summary>
	/// 发出声音。
	/// </summary>
	public abstract void Speak();
}

/// <summary>
/// 狗的类型。
/// </summary>
file sealed class Dog : Animal
{
	/// <inheritdoc/>
	public override void Speak() => Console.WriteLine("狗叫");
}