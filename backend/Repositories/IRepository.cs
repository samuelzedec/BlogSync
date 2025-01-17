namespace backend.Interface;

public interface IRepository<T>
{
    public Task<List<T>> ReadAll();
    public Task<T> Read(int modelId);
    public Task<T> Create(T model);
    public Task<T> Update(int modelId, T model);
    public Task<T> Delete(int modelId);
}
