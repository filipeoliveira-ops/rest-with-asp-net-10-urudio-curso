using Microsoft.EntityFrameworkCore;
using RestWithASPNET10Erudio.Model.Base;
using RestWithASPNET10Erudio.Model.Context;
using RestWithASPNET10Erudio.Repositories;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
	protected MSSQLContext _context;
	private DbSet<T> _dataset;

	public GenericRepository(MSSQLContext context)
	{
		_context = context;
		_dataset = context.Set<T>();
	}

	public List<T> FindALL() => _dataset.ToList();

	public T FindByID(long id) => _dataset.Find(id);

	public T Create(T item)
	{
		_context.Add(item);
		_context.SaveChanges();
		return item;
	}

	public T Update(T item)
	{
		var existingItem = _dataset.Find(item.Id);
		if (existingItem == null) return null;
		_context.Entry(existingItem).CurrentValues.SetValues(item);
		_context.SaveChanges();
		return item;
	}

	public void Delete(long id)
	{
		var existingItem = _dataset.Find(id);
		if (existingItem == null) return;
		_context.Remove(existingItem);
		_context.SaveChanges();
	}

	public bool Exists(long id) => _dataset.Any(e => e.Id == id);
}