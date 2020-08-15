using Restaurant.Data.Entities;
using Restaurant.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _context;

        private bool disposed = false;

        public UnitOfWork(RestaurantDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            MenuCardRepository = MenuCardRepository ?? new GenericRepository<MenuCardEntity>(_context);
            MenuRepository = MenuRepository ?? new GenericRepository<MenuEntity>(_context);

        }

        public IGenericRepository<MenuCardEntity> MenuCardRepository { get; set; }
        public IGenericRepository<MenuEntity> MenuRepository { get; set; }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
    }
}
