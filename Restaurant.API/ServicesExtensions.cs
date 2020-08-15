using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Data;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.UOW;
using Restaurant.Data.Repositories;
using Restaurant.Services.Interfaces;
using AutoMapper;
using Restaurant.Services.Implementation;
using Restaurant.Common.Mapping;

namespace Restaurant.API
{
    public static class ServicesExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RestaurantDbContext>(options =>
                       options.UseSqlServer(config.GetConnectionString("RestaurantConnection")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IMenuCardService, MenuCardService>();

            services.AddCors();

            services.AddCors(o => o.AddPolicy("EnableAnyRequest", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

    }
}
