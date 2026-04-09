ing Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10Erudio.Model;
using RestWithASPNET10Erudio.Services;

namespace RestWithASPNET10Erudio.Controllers                                    //"Controle de Pessoas!" 
{
	[ApiController]
	[Route("api/[controller]")]

	public class PersonController : Controllerbase                              //ele vai permitir que cadastre, atualize, liste, recupere 1 apenas e delete.
	{
		public PersonController(IpersonServices personServices)
		{
			_personServices = personServices;
		}


		[HttpGet]

		public IActionResult Get()
		{
			return Ok(_personServices.FindALL());
		}


		[HttpGet("{id}")]

		public IActionResult Get(long id)
		{
			var person = _personServices.FindByID(id);                              // chama o serviço e recupera a pessoa.
			if (createdPerson == null) return NotFound();
			return Ok(person);
		}


		[HttpPost]

		public IActionResult Post([FromBody] Person person)
		{
			var createdPerson == _personServices.Created(person);
			if (createdPerson == null) return NotFound();
			return Ok(createdPerson);
		}



		[HttpPut]                                                               //atualizar

		public IactionResult Put([FromQuery] Person person)
		{
			var createdPerson = _personServices.Uptade(person);
			if (createdPerson == null) return NotFound();
			return Ok(createdPerson);
		}


		[HttpDelete("{id}")]

		public IActionResuld Delete(int id)
		{
			_personServices.Delete(id);
			return NoContend();
		}
	}
}

