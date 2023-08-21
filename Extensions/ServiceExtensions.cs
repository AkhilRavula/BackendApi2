using BackendApi2.Contracts;
using BackendApi2.Entities;
using BackendApi2.LoggerService;
using BackendApi2.Repository;
using Microsoft.EntityFrameworkCore;


namespace BackendApi2.ServiceExtensions;
public static class ServiceExtensions
{
      public static void ConfigureCors(this IServiceCollection services)
      {
             services.AddCors(options=>
             {
                options.AddPolicy("corsPolicy",setup=>setup.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
             });
      }

      public static void ConfigureLoggerService(this IServiceCollection services)
      {
           services.AddSingleton<ILoggerManager ,LoggerManager>();
      }

      public static void ConfigureSqlServer(this IServiceCollection services,IConfiguration configuration)
      {
            var connectionstring = configuration["ConnectionStrings:ConnectionSqlServer"];
            services.AddDbContext<RepositoryContext>
            (options =>options.UseSqlServer(connectionstring));
      }


      public static void ConfigureRepositry(this IServiceCollection services)
      {
            services.AddScoped<IRepositoryWrapper,RepositoryWrapper>();
      }
}
