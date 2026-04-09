using RestWithASPNET10Erudio.Model;

namespace RestWithASPNET10Erudio.Services.Implementations
{
	private class PersonServicesimpl : IpersonServices

		public Person FindByID(long id)                                                 //vai retornar uma pessoa mocada
	{
		var person = MockPerson((int)id);
		return person;
	}


	public List<Person> FindALL()                                                   //vai retornar uma lista de pessoas mocadas
	{
		List<Person> persons = new List<Person>();
		for (int i = 0; i < 8; i++)                                                 //8 mocks: (Vai listar o nome(firstname), endereço(address, segundo nome(last name),Genero(gender)
		{
			persons.Add(MockPerson(i));
		}
		return persons;
	}


	public Person Create(Person person)                                                 //chama o Create, passando a pessoa, o sistema vai ate a base de dados, grava essa pessoa no banco e vai retornar a pessoa atualizada com o ID; simula o cadastro no banco.
	{
		person.Id = new Random().Next(1, 1000);                                         //Simula a  atribuição de ID. 
		return person;
	}


	public Person Uptade(Person person)                                             //Ja vem com o ID, nao precisa setar; simula o uptade.
	{
		return person;
	}


	public void Delete(long id)                                                     //Recebe o id, vai até o banco, deleta a pessoa e n retorna nada(por isso é um 'void).



	private Person MockPerson(int i)
	{
		var person = new Person
		{
			Id = new Random().Next(1, 1000),
			FirstName = "Filipe " + i,
			LastName = "Oliveira " + i,
			Address = "123 Main Street " + i,
			Gender = "Male"
		};
		return person;
	}

}
