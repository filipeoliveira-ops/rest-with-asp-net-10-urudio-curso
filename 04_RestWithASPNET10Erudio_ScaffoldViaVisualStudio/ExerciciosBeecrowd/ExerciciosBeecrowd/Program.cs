using System;

namespace ExerciciosBeecrowd
{
	class Program
	{
		static void Main(string[] args)
		{

			int a = int.Parse(Console.ReadLine());
			int b = int.Parse(Console.ReadLine());
			int x = (a + b);
			

			a = int.Parse(Console.ReadLine());
			b = int.Parse(Console.ReadLine());

			Console.WriteLine(a + b, "=", x );

			Console.ReadLine();
		}
	}
}