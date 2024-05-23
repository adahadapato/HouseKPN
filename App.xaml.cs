using HouseKPN.Infrastructures.DI;
using HouseKPN.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text;
using System.Windows;

namespace HouseKPN
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string access_token;
        public static bool Authenticated = false;
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, true);



            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }


        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.RegisterViewModels();
            serviceCollection.RegisterServices(Configuration);
            serviceCollection.RegisterHttpClients(Configuration);


            serviceCollection.AddSingleton(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

        }
    }

}
