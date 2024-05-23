


namespace HouseKPN.Infrastructures.DI;
using HouseKPN.ViewModels;
using Microsoft.Extensions.DependencyInjection;
public static class ViewModelDependencies
{
    public static void RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<FilesViewModel>();
        services.AddSingleton<EventsViewModel>();
        services.AddSingleton<DashbordViewModel>();


        services.AddSingleton<Func<Type, ViewModel>>(servireProvider => 
                                viewmodelType => 
                                (ViewModel)servireProvider.GetRequiredService(viewmodelType));
    }
}
