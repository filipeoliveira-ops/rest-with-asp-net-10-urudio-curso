using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RestWithASPNET10Erudio.Data.Converter.Impl;
using RestWithASPNET10Erudio.Data.DTO.V1;
using RestWithASPNET10Erudio.Model;
using RestWithASPNET10Erudio.Repositories;
using RestWithASPNET10Erudio.Services;

namespace RestWithASPNET10Erudio.Services.Impl
{
	public class PersonServicesImplV1 : IPersonServices
	{
		private IPersonRepository _repository;
		private readonly PersonConverter _converter;

		public PersonServicesImplV1(IPersonRepository repository)
		{
			_repository = repository;
			_converter = new PersonConverter();
		}

		public List<PersonDTO> FindALL()
		{
			return _converter.FindAll.Adapt<List<PersonDTO>>();
		}

		public PersonDTO FindByID(long id)
		{
			return _converter.Parse(_repository.FindByID(id));
		}

		public PersonDTO Create(PersonDTO person)
		{
			var entity = _converter.Parse(person);
			entity = _repository.Create(entity);
			return _converter.Parse(entity);
		}

		public PersonDTO Update(PersonDTO person)
		{
			var entity = _converter.Parse(person);
			entity = _repository.Update(entity); // Uptade -> Update
			return _converter.Parse(entity);
		}

		public void Delete(long id)
		{
			_repository.Delete(id);
		}

		public PersonDTO Disable(long id)
		{
			var entity = _repository.Disable(id);
			return entity.Adapt<PersonDTO>();
		}

		public List<PersonDTO> FindByName(string firstName, string lastName)
		{
			return _converter.ParseList(_repository.FindALL());
		}
	}
}