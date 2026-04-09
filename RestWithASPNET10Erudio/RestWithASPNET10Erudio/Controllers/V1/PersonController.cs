//  using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10Erudio.Data.DTO.V1;
using RestWithASPNET10Erudio.Services;
using RestWithASPNET10Erudio.Services.Impl;

namespace RestWithASPNET10Erudio.Controllers.V1									//"Controle de Pessoas!
{
	[ApiController]
	[Route("api/[controller]/v1")]
	//[EnableCors("LocalPolicy")]
	public class PersonController : ControllerBase									//ele vai permitir que cadastre, atualize, liste, recupere 1 apenas e delete
	{
	
		private IPersonServices _personService;
		private readonly ILogger<PersonController> _logger;
		public PersonController(IPersonServices personServices,
			ILogger<PersonController> logger)
		{
			_personService = personServices;
			_logger = logger; 
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<PersonDTO>))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Get() 
		{
			_logger.LogInformation("Fetching all persons");												// "buscando todas as pessoas
			return Ok(_personService.FindALL());		
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(PersonDTO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		// [EnableCors("LocalPolicy")]
		public IActionResult Get(long id)
		{
			_logger.LogInformation("Fetching person with ID {id}", id);			//Buscar por ID (parametro id: {id}) e quando for imprimir, substitiu o {id} pelo ID.
			var person = _personService.FindByID(id);                       // chama o serviço e recupera a pessoa
			if (person == null)
			{
				_logger.LogWarning("Person with ID {id} not found", id);		//vai ser o log de aviso: "Pessoa nao encontrada ID (parametro id: {id}) e o 'id' para aparecer o ID.
				return NotFound();
			}
			return Ok(person);
					
		}

		[HttpPost]                                                              //criar
		[ProducesResponseType(200, Type = typeof(PersonDTO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		// [EnableCors("MultipleOriginPolicy")]
		public IActionResult Post([FromBody] PersonDTO person)
		{
			_logger.LogInformation("Creating new Person: {firstname}", person.FirstName);					//Criar uma nossa pessoa com o 1ºNome e o comando para setar a pessoa pelo 1º nome.
			var createdPerson = _personService.Create(person);
			if (createdPerson == null)
			{
				_logger.LogError("Failed to create person with name {firstname}", person.FirstName);        //"Falha ao criar pessoa com nome (1º nome)" e o comando para setar p 1º nome.
				return NotFound();
			}
			return Ok(createdPerson);
		}
		
		
		[HttpPut]                                                           //atualizar
		[ProducesResponseType(200, Type = typeof(PersonDTO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Put([FromBody] PersonDTO person)
		{
			_logger.LogInformation("Uptating person with ID {id}", person.Id);				//"atualizando pessoa pelo ID" = parametro id {id} e o comando para buscar ela pelo id 'person.Id'
			var createdPerson = _personService.Update(person);
			if (createdPerson == null)
			{
				_logger.LogError("Failed to uptade person with ID {id}", person.Id);		//"Falha ao criar pessoa pelo ID" = parametro id {id} e o comando para buscar pelo id 'person.Id'		
				return NotFound();
			}
			_logger.LogDebug("Person updated successfully: {firstname}", createdPerson.FirstName);         //"Pessoa criada com suacesso; 1º nome e o comando para setar o 1º nome.
			return Ok(createdPerson);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204, Type = typeof(PersonDTO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Delete(long id)
		{
			_logger.LogInformation("Deleting person with ID {id}", id);                //"Deletando pessoa pelo ID" = parametro {id} e o 'id' para aparecer o ID.
			_personService.Delete(id);
			_logger.LogDebug("Person with ID {id} deleted successfully", id);           //"Pessoa com ID excluída com sucesso" = param. {id} e o 'id' para setar o ID da pessoa.
			return NoContent();
		}

		[HttpPatch("{id}")]                                                          //atualilzar parcial
		[ProducesResponseType(200, Type = typeof(PersonDTO))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public IActionResult Disable(long id)
		{
			_logger.LogInformation("Disabling person with ID {id}", id);				//"desabilitando pessoa pelo ID" = parametro id {id} e o comando para buscar ela pelo id 'person.Id'
			var disabledPerson = _personService.Disable(id);
			if (disabledPerson == null)
			{
				_logger.LogError("Failed to disable person with ID {id}", id);		//"Falha ao desabilitar pessoa pelo ID" = parametro id {id} e o comando para buscar pelo id 'person.Id'
				return NotFound();
			}
			_logger.LogDebug("Person with ID {id} disabled successfully: {firstname}", id, disabledPerson.FirstName);         //"Pessoa desabilitada com suacesso; 1º nome e o comando para setar o 1º nome.
			return Ok(disabledPerson);
		}

	}
}
