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
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;


        public MenuService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MenuEntity> Get(int id)
        {
            return await _uow.MenuRepository.GetAsync(id);
        }
        
        public async Task<MenuEntity> Get(string name)
        {
            return await _uow.MenuRepository.GetAll().Where(w => w.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MenuResponseDTO>> GetAll(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            var result = _uow.MenuRepository.GetAll().Skip(skip).Take(limit);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(w => w.Name.ToLower().Contains(filter.ToLower()));
            }

            if (!string.IsNullOrEmpty(columnSort))
            {

                Expression<Func<MenuEntity, string>> orderingFunction = i =>
                                    columnSort == "Type" ? i.Type :
                                    columnSort == "Price" ? i.Price.ToString() :
                                    columnSort == "Name" ? i.Name : i.Type + i.Price + i.Name;

                if (descending)
                {
                    result = result.OrderByDescending(orderingFunction);
                } else
                {
                    result = result.OrderBy(orderingFunction);
                }
            }

            return _mapper.Map<IEnumerable<MenuResponseDTO>>(await result.ToListAsync());
        }
        
        public async Task<int> Count(string filter)
        {
            var result = _uow.MenuRepository.GetAll();

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(w => w.Name.ToLower().Contains(filter.ToLower()));
            }

            return await result.CountAsync();
        }
        
        public async Task<IEnumerable<MenuResponseDTO>> GetAllByCard(int id, int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            var result = _uow.MenuRepository.GetAll().Where(w => w.MenuCardId == id).Skip(skip).Take(limit);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(w => w.Name.ToLower().Contains(filter.ToLower()));
            }

            if (!string.IsNullOrEmpty(columnSort))
            {

                Expression<Func<MenuEntity, string>> orderingFunction = i =>
                                    columnSort == "Type" ? i.Type :
                                    columnSort == "Price" ? i.Price.ToString() :
                                    columnSort == "Name" ? i.Name : i.Type + i.Price + i.Name;

                if (descending)
                {
                    result = result.OrderByDescending(orderingFunction);
                } else
                {
                    result = result.OrderBy(orderingFunction);
                }
            }

            return _mapper.Map<IEnumerable<MenuResponseDTO>>(await result.ToListAsync());
        }

        public async Task<int> CountByCard(int id, string filter)
        {
            var result = _uow.MenuRepository.GetAll().Where(w => w.MenuCardId == id);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(w => w.Name.ToLower().Contains(filter.ToLower()));
            }

            return await result.CountAsync();
        }
        
        public async Task Add(MenuRequestDTO dto)
        {
            var model = new MenuEntity
            {
                Name = dto.Name,
                Type = dto.Type,
                Price = dto.Price,
                Active = true,
                CreateDate = DateTime.Now,
                LastModification = DateTime.Now,
                MenuCardId = dto.MenuCardId
            };

            await _uow.MenuRepository.AddAsync(model);

            await _uow.Commit();
        }
        
        public async Task Update(MenuRequestDTO dto, int id)
        {
            var model = new MenuEntity
            {
                Id = id,
                Name = dto.Name,
                Type = dto.Type,
                Price = dto.Price,
                LastModification = DateTime.Now,
                MenuCardId = dto.MenuCardId
            };

            await _uow.MenuRepository.UpdateAsync(model, id);

            await _uow.Commit();
        }
        
        public async Task ChangeActive(int id, bool isActive)
        {
            var model = new MenuEntity
            {
                Id = id,
                Active = isActive,
                LastModification = DateTime.Now
            };

            await _uow.MenuRepository.UpdateAsync(model, id);

            await _uow.Commit();
        }
        
        public async Task Delete(int id)
        {            
            await _uow.MenuRepository.Delete(d => d.Id == id);

            await _uow.Commit();
        }

    }
}
