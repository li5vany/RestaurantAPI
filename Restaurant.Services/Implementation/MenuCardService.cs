using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Common.DTO.Requests;
using Restaurant.Common.DTO.Responses;
using Restaurant.Data.Entities;
using Restaurant.Data.UOW;
using Restaurant.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Implementation
{
    public class MenuCardService : IMenuCardService
    {
        private readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;


        public MenuCardService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MenuCardResponseDTO> Get(int id)
        {
            return _mapper.Map<MenuCardResponseDTO>(await _uow.MenuCardRepository.GetAsync(id));
        }
        
        public async Task<MenuCardResponseDTO> Get(string name)
        {
            return _mapper.Map<MenuCardResponseDTO>(await _uow.MenuCardRepository.GetAll().Where(w => w.Name == name).FirstOrDefaultAsync());
        }
        
        public async Task<MenuCardWithMenuResponseDTO> GetWithMenu(int id)
        {
            return _mapper.Map<MenuCardWithMenuResponseDTO>(await _uow.MenuCardRepository.GetAll().Include(i => i.Menus).Where(w => w.Id == id).FirstOrDefaultAsync());
        }
        
        public async Task<MenuCardWithMenuResponseDTO> GetWithMenu(string name)
        {
            return _mapper.Map<MenuCardWithMenuResponseDTO>(await _uow.MenuCardRepository.GetAll().Include(i => i.Menus).Where(w => w.Name == name).FirstOrDefaultAsync());
        }

        public async Task<IEnumerable<MenuCardResponseDTO>> GetAll(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            var result = _uow.MenuCardRepository.GetAll().Skip(skip).Take(limit);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(w => w.Name.ToLower().Contains(filter.ToLower()));
            }

            if (!string.IsNullOrEmpty(columnSort))
            {

                Expression<Func<MenuCardEntity, string>> orderingFunction = i => columnSort == "Name" ? i.Name : i.Name;

                if (descending)
                {
                    result = result.OrderByDescending(orderingFunction);
                } else
                {
                    result = result.OrderBy(orderingFunction);
                }
            }

            return _mapper.Map<IEnumerable<MenuCardResponseDTO>>(await result.ToListAsync());
        }
        
        public async Task<int> Count(string filter)
        {
            var result = _uow.MenuCardRepository.GetAll();

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(w => w.Name.ToLower().Contains(filter.ToLower()));
            }

            return await result.CountAsync();
        }
        
        public async Task<IEnumerable<MenuCardWithMenuResponseDTO>> GetAllWithMenus(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            var result = _uow.MenuCardRepository.GetAll().Include(i => i.Menus).Skip(skip).Take(limit);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(w => w.Name.ToLower().Contains(filter.ToLower()));
            }

            if (!string.IsNullOrEmpty(columnSort))
            {

                Expression<Func<MenuCardEntity, string>> orderingFunction = i => columnSort == "Name" ? i.Name : i.Name;

                if (descending)
                {
                    result = result.OrderByDescending(orderingFunction);
                } else
                {
                    result = result.OrderBy(orderingFunction);
                }
            }

            return _mapper.Map<IEnumerable<MenuCardWithMenuResponseDTO>>(await result.ToListAsync());
        }

        public async Task Add(MenuCardRequestDTO dto)
        {
            var model = new MenuCardEntity
            {
                Name = dto.Name,
                Active = true,
                CreateDate = DateTime.Now,
                LastModification = DateTime.Now
            };

            await _uow.MenuCardRepository.AddAsync(model);

            await _uow.Commit();
        }
        
        public async Task Update(MenuCardRequestDTO dto, int id)
        {
            var model = new MenuCardEntity
            {
                Id = id,
                Name = dto.Name,
                LastModification = DateTime.Now
            };

            await _uow.MenuCardRepository.UpdateAsync(model, id);

            await _uow.Commit();
        }
        
        public async Task ChangeActive(int id, bool isActive)
        {
            var model = new MenuCardEntity
            {
                Id = id,
                Active = isActive,
                LastModification = DateTime.Now
            };

            await _uow.MenuCardRepository.UpdateAsync(model, id);

            await _uow.Commit();
        }
        
        public async Task Delete(int id)
        {            
            await _uow.MenuCardRepository.Delete(d => d.Id == id);

            await _uow.Commit();
        }

    }
}
