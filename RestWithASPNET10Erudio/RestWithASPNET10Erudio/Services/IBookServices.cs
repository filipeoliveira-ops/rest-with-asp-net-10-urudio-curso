using RestWithASPNET10Erudio.Data.DTO.V1;
using SeuProjeto.Model;

namespace RestWithASPNET10Erudio.Services
{
	public interface IBookServices
	{

		BookDTO Create(BookDTO book);
		BookDTO FindByID(long id);
		List<BookDTO> FindAll();
		BookDTO Update(BookDTO book);
		void Delete(long id);
	}
}


