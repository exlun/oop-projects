namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public class BasicRepository<T> where T : IIdentifiable
{
    private List<T> Database { get; init; } = [];

    public T? Get(Guid id)
    {
        foreach (T? item in Database.Where(item => item.Id == id))
        {
            return item;
        }

        return default;
    }

    public void Add(T item)
    {
        Database.Add(item);
    }

    public bool Remove(Guid id)
    {
        T? item = Get(id);

        return item != null && Database.Remove(item);
    }
}