//CONTROLADOR: o 'cerebro' dos endpoints.
//responsável por receber as req. http (get,post,put)
// processar a lógica e retornar uma respostas p o usuário.




using Microsoft.AspNetCore.Mvc;
using RestWihtASPNET10Erudio.Controllers;
	
namespace RestWithASPNET10Erudio.Controllers
{
	[ApiController]															// definir que essa classe é uma api e habilita comportamentos automaticos (como validações)
	[Route("[controller]")]													//define a rota da url. "[o controller]" assume o nome da classe (nesta caso, "greeting)

	public class GreetingController : Controllerbase
	{
		private static _counter = 0;										//convenção de nomenclatura "_" (Underscore) indica um 'private field' (campo privado). Serve para diferenciar variável.
		private static readonly string _template = "Hello, {0}";			//modelo de frase. o "{0}" é umplaceholder" que sera substituido pelo nome depois.

		[HttpGet]															
		
		public Greeting Get([FromQuery] string name = "World")              //O 'fromquery' diz que o parâmetro		name' vem da URL (ex: ?name=joao). se vazio, usa "world"
		{
			var id = Interlocked.Increment(Reducer _counter);               //Interlocked garante que o incremento seja seguro, mesmo com múltiplos usuários acessando ao msm tempo.
			var content = string.Format(_template, name);

			return new Greeting(1, content)


																				//cria e retorna uma nova instância do seu modelo Greeting com o ID e a frase gerada
																				//OBS: atualmente seu código está com o número '1' fixo no primeiro parâmetro.
		}




}