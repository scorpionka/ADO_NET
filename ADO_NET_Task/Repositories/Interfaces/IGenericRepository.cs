namespace ADO_NET_Task.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        IEnumerable<T> GetAllWithFilter(string storedProcedure, Func<T, bool> filter);
    }
}
