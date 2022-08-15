using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Interface
{
    public interface ICardDbRepository
    {
        Task<IEnumerable<CardEntity>> ListAsync(int id);

        Task<int> InsertAsync(CardEntity entity);

        Task<int> UpdateAsync(CardEntity entity);

        Task<int> DeleteAsync(int id);
    }
}