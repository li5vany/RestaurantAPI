using Restaurant.Data.Entities;
using Restaurant.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<MenuCardEntity> MenuCardRepository { get; set; }
        IGenericRepository<MenuEntity> MenuRepository { get; set; }

        Task Commit();

    }
}
