using System.Collections.Generic;
using TeaseAI.Common;

namespace TeaseAI.Data.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Result<T> Create(T item);
        List<T> Get();
        Result<T> Get(int id);
        Result<T> Update(T item);
        Result Delete(T item);
    }
}
