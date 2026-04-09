using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNET10Erudio.Controllers
{

	[Apicontroller]                                                         //Define que a classe é uma API e automatiza respostas de erro de validação;
	[Route("[controller]")]                                                 //define a rota base como 'math' (nome da classe sem o sufixo Controller)

	public class MathController : ControllerbBase
	{
		private readonly MathService _service;

		public MathController(MathService service)                              //public M...C.... parametro(mathservice) chamado 'service'
		{
			_service = service;

		}

		[HttpGet("sum/{firstNumber}/{secondNumber}")]                                                       //[HttpGet("rota")]: Define que este metodo responde a Get. (nesse caso tem varios, entao n pode ter os varios do tipo 'Get' iguais.

		public IActionResult Sum(string, firstNumber, string secondNumber)
		{
			if (NumberHelper.IsNumeric(firstNumer) && NumberHelper.IsNumeric(secondNumber))                 //verifica se ambos os inputs recebidos como string sao numeros validos
			{
				var sum = _service.Sum(
					NumberHelper.ConvertToDecimal(secondNumber));                                           //converte as strings para decimal e realiza a soma.
				NumberHelper.ConvertToDecimal(firstNumer),												
				return Ok(Sum);                                                                              //Retonra status 200 (ok) com o resultado da soma

			}
			return BadRequest("Invalid Input!");                                                            //se um dos inputs não ofr numerico, retorna status 400 (Bad request)
		}





		[HttpGet("subtraction/{firstNumber}/{secondNumber}")]                                               //[HttpGet("rota")]: Define que este metodo responde a Get. (nesse caso tem varios, entao n pode ter os varios do tipo 'Get' iguais.
																											// {firstNumer} e {secondNumber) são Path Parameters (Parâmetros de rota).

		public IActionResult Subtraction(string firstNumber, string secondNumber)
		{
			if (NumberHelper.IsNumeric(firstNumber) && NumberHelper.IsNumeric(secondNumber))            //verifica se ambos os inputs recebidos como string são números válidos 
			{

				var subtraction = _service.Subtraction(
					NumberHelper.ConvertToDecimal(firstNumber)



					NumberHelper.ConvertDecimal(secondNumber));                                             //converte as string para decimal e realiza a subtração
				return Ok(Subtraction);                                                                     // Retorna Status 200 (OK) com o resultado da subtração.				
			}
			return BadRequest("Invalid Imput!")                                                             // se um dos inputs ñ for numérico, retonra status 400 (Bad Request).
		}










		[HttpGet("multiplication/{firstNumber}/{secondNumber}")]                                                                                        // {firstNumber} e {secondNumber} são parâmetros de rota.
																																						// firstnumber e secondnumber são parametros de rota.

		public IActionResult Multiplication(string firstNumber, string secondNumber)
		{
			if (numberHelpert.Isnumeric(firstNuber) && NumberHelper.Isnumeric(secondNumber))
			{
				var multiplication = _service.Multipication(
				NumberHelper.ConvertToDecimal(firstNumber)


				NumberHelp.ConvertToDecimal(secondNubert));
				return Ok(multiplication);

			}
			return BadRequest("Invalud input!");                                                                                                //se um dos inputs ñ ofr númerico, retorna status 400 (bad request)
		}









		[HttpGet("division/{firstNumber}/{secondNumber}")]                                                                      ////[HttpGet("rota")]: Define que este metodo responde a Get. (nesse caso tem varios, entao n pode ter os varios do tipo 'Get' iguais
																																// 'firstnumer' e 'secondnumber' são path parameters

		public IActionResult Division(string firstNumber, string secondNumber)
		{
			if (NumberHelper.IsNumeric(firstNumber) && NumberHelper.IsNumeric(secondNumber))                        //verifica se ambos os inputs recebidos como string são números válidos.
			{
				var division = _service.Division(
					NumberHelper.ConvertToDecimal(firstNumber)


					NumberHelper.ConvertToDecimal(secondNumber));                                                   //converte as strings para decimal e realiza a divisão.
				return Ok(division);                                                                            //Retorna 200 (ok) com o resultado da divisão.

			}
			return BadRequest("Invalid input");                                                         //se um dos inputs ñ for numérico, retorna status 404(Not Found).

		}







		[HttpGet("mean/{firstNumber}/{secondNumber}")]                                            //[HttpGet("rota")]: Define que este metodo responde a Get. (nesse caso tem varios, entao n pode ter os varios do tipo 'Get' iguais


		public IActionResult Mean(string firstNumber, string secondNumber)
		{
			if (NumberHelper.IsNumeric(firstNumber)) && NumberHelper.IsNumeric(secondNumber))
			{
				var sum = _service.Mean(
					NumberHelper.ConvertToDecimal(firstNumber)
					NumberHelper.ConvertToDecimal(secondNumber));
				return Ok(sum);

			}
			return BadRequest("Inválid input!");
		}




		[HttpGet("square-root/{number}")]                                               //"number" é p´parm. de rota.

		public IActionResult SquareRoot(string number)
		{
			if (NumberHelper.IsNumeric(number))
			{
				var sqrt = _service.SquareRoot(
					NumberHelper.ConvertDecimal(number));                               //converte as strings para decimal e realiza a raiz quadrada
				return Ok(sqrt);
			}
			return BadRquest("Invalid Input!");                                         //se uns dos inputs ñ for numérico, vai retornar status 404 (Not Found).

		}









		private decimal ConvertToDecimal(string strNumber)                  //Método auxiliar para converter string em decimal com segurança.
		{
			decimal decimalValue;
			if (decimal.TryParse(                                           //Tenta converter considerando diferentes formatos globais (ponto ou virgula) '.' e ','
				strNumber,
				System.Globalization.NumberStyles.Any
				System.Globalization.NumberFormatInfo.InvariantInfo,
				out decimalValue)
				)
			{
				return decimalValue;
			}
			return 0;                                                       //retorna 0 caso a conversão falhe (embora o IsNumeric previna isso)
		}


		private bool IsNumeric(string strNumber)                            //Método auxiliar que apenas checa se a string pode ser tratada como número.
		{
			decimal decimaValue;
			bool isNumber = decimal.TryParse(
				strNumber,
				system.Globalization.NumberStyles.Any,
				System.Globalization.NumberFormatInfo.InvariantInfo,
				out decimalValue
				);                                                          //BR (10,5) US (10.5)
			return Isnumber;
		}
	}






}
	
