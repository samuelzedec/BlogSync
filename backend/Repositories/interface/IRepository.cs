namespace backend.Repositories.@interface;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetAsync(int modelId);
    public Task<T> CreateAsync(T model);
    public Task<T> UpdateAsync(int modelId, T model);
    public Task<T> DeleteAsync(int modelId);
}