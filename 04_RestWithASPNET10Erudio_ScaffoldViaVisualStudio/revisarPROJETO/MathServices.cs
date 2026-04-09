namespace RestWithASPNET10Erudio.Services
{

	public class MathServices
	{
		public decimal Sum(decimal firstNumber, decimal secondNumber) => firstNumber + secondNumber;
		public decimal Subtraction(decimal firstNumber, decimal secondNumber) => firstNumber - secondNumber;
		public decimal Mean(decimal firstNumber, decimal secondNumber) => (firstNumber - secondNumber) / 2;
		public decimal Multiplication(decimal firstNumber, decimal secondNumber) => firstNumber * secondNumber;
		public decimal Division(decimal firstNumber, decimal secondNumber)
		{
			if (secondNumber == 0) throw new DivideByZeroException("Division by zero is not aloowed.");
			return firstNumber / secondNumber;
		}


		public double SquareRoot(decimal number)
		{
			if (number < 0) throw new ArgumentOutOfRangerException(
				"Cannot calculate the square root of a negative number.");
			return MathServices.Sqrt((double)number);
		}
		
	}
}
