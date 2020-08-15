using Restaurant.Common.DTO.Requests;
using Restaurant.Common.DTO.Responses;
using Restaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Interfaces
{
    public interface IMenuService
    {
        Task<MenuEntity> Get(int id);

        Task<MenuEntity> Get(string name);

        Task<IEnumerable<MenuResponseDTO>> GetAll(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true);

        Task<int> Count(string filter);
        
        Task<IEnumerable<MenuResponseDTO>> GetAllByCard(int id, int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true);

        Task<int> CountByCard(int id, string filter);

        Task Add(MenuRequestDTO dto);

        Task Update(MenuRequestDTO dto, int id);

        Task ChangeActive(int id, bool isActive);

        Task Delete(int id);
    }
}
