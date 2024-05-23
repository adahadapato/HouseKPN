

namespace HouseKPN.Infrastructures.DI;

using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


public static class ServiceDependencies
{
    public static void RegisterServices(this IServiceCollection services,
       IConfiguration configuration)
    {
        //services.AddDbContext<ToDoDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("SimpleTodoApp")));
        //services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton(typeof(CandidateService));
        services.AddSingleton(typeof(CandidateService));
        services.AddScoped(typeof(RegistryService));
        services.AddScoped<Resources.Interfaces.IResourceService, ResourceService>();  
        services.AddScoped<IDataService, DataService>();
        services.AddScoped<ITokenContainer, TokenContainer>();

       
    }
}
