using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace HouseKPN.Infrastructures.DI;

public static class HttpClientDependencies
{
    public static void RegisterHttpClients(this IServiceCollection services,
           IConfiguration configuration)
    {
       

        services.AddHttpClient<IResourceService, ResourceService>((httpClient) =>
        {
            httpClient.BaseAddress = new Uri("http://10.0.1.31/ManagerApi/api/");
        })
      .ConfigurePrimaryHttpMessageHandler(() =>
      {
          return new SocketsHttpHandler
          {
              PooledConnectionLifetime = TimeSpan.FromMinutes(5),
          };
      }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);



        services.AddHttpClient<IDataService, DataService>((httpClient) =>
        {
            httpClient.BaseAddress = new Uri("http://10.0.1.31:9302/api/");
        })
     .ConfigurePrimaryHttpMessageHandler(() =>
     {
         return new SocketsHttpHandler
         {
             PooledConnectionLifetime = TimeSpan.FromMinutes(5),
         };
     }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

    }
}
