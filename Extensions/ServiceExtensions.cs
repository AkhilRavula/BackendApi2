using System.Net;
using BackendApi2.Contracts;
using BackendApi2.CustomExceptions;
using BackendApi2.Entities;
using BackendApi2.LoggerService;
using BackendApi2.Repository;
using Microsoft.AspNetCore.Diagnostics;
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


       public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                   
                    if(contextFeature != null)
                    { 
                    context.Response.StatusCode = contextFeature.Error switch
                       {
                        BadHttpRequestException => StatusCodes.Status400BadRequest,
                        EmployeeNotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {

                            StatusCode = context.Response.StatusCode,

                            Message = contextFeature.Error.Message

                        }.ToString());
                    }
                });
            });
        }
}
