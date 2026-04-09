using RestWithASPNET10Erudio.Model;

namespace RestWithASPNET10Erudio.Services
{

	public interface IpersonServices
	{

		Person Create(Person person);           //método chama 'create' e a assinatura 'person'

		Person FindByID(ling id);               //long = nao tera problema no banco de daids se estiver com muitas pessoas cadastradas. Agora se for o Int(no lugar de 'long'), pode ser que tenha problemas no banco de dados.

		List<Person> FindALL();
		Person Uptade(Person person);

		void Delete(long id);
	}
	
}