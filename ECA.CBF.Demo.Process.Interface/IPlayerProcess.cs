using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface IPlayerProcess
    {
        Task<IEnumerable<PlayerEntity>> ListPlayerAsync();

        Task<PlayerEntity> GetPlayerAsync(int teamId);

        Task<int> InsertPlayerAsync(PlayerEntity team);

        Task UpdatePlayerAsync(PlayerEntity team);

        Task DeletePlayerAsync(int teamId);
    }
}