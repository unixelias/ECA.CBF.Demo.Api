using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Interface
{
    public interface IPlayerDbRepository
    {
        Task<IEnumerable<PlayerEntity>> ListPlayerAsync();

        Task<PlayerEntity> GetPlayerAsync(int playerId);

        Task<int> InsertPlayerAsync(PlayerEntity player);

        Task UpdatePlayerAsync(PlayerEntity player);

        Task DeletePlayerAsync(int playerId);
    }
}