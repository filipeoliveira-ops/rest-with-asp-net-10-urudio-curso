namespace RestWithASPNET10Erudio.Utils
{

	public class NumberHelper
	{

		public static decimal ConvertToDecimal(string strNumber)                                                //método auxiliar para converter string em decimal com segurança.
		{
			decimal decimalValue;
			if (decimal.TryParse(                                                           //tenta convertar considerando diferentes formatods globais (ponto ou virgula)
				strNumber,
				System.Globalization.NumberStyles.Any,
				System.Globalization.NumberFormatInfo.InvariantInfo,
				out decimalValue))
			{
				return decimalValue
		}
			return 0;																		//retonra 0 caso a conversão falhe(embora o IsNumeric previna isso)
		}

		public static bool IsNumeric(string strNumber)
		{
			decimal decimalValue;														//método auxiliar que apenas checa se a string pode ser tratada como número.
			bool isNumber = decimal.TryParse(											//o tryparse tenta converter: se conseguir, retorna true; se nao, false.
				strNumber,
				System.Globalization.NumberStyles.Any,
				System.Globalization.NumberFormatInfo.InariantInfo,
				out decimalValue
				);
		}
	}
}