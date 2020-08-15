using Restaurant.Common.DTO.Requests;
using Restaurant.Common.DTO.Responses;
using Restaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Interfaces
{
    public interface IMenuCardService
    {
        Task<MenuCardResponseDTO> Get(int id);

        Task<MenuCardResponseDTO> Get(string name);
        
        Task<MenuCardWithMenuResponseDTO> GetWithMenu(int id);

        Task<MenuCardWithMenuResponseDTO> GetWithMenu(string name);

        Task<IEnumerable<MenuCardResponseDTO>> GetAll(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true);

        Task<int> Count(string filter);
        
        Task<IEnumerable<MenuCardWithMenuResponseDTO>> GetAllWithMenus(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true);

        Task Add(MenuCardRequestDTO dto);

        Task Update(MenuCardRequestDTO dto, int id);

        Task ChangeActive(int id, bool isActive);

        Task Delete(int id);
    }
}
