//Modelo (pojo): define a estrutura dos dados.
//Representa o objeto que será transformado em JSON para o cliente.
//Contém apenas as propriedades (ex: Id e Content).



namespace RestWithASPNET10Erudio.Model
{
	public record Greeting(long Id, string content);
}
