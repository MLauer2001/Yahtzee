using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Yahztee.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceProvider serviceProvider;
        public static IConfiguration Configuration;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private static void ConfigureServices(ServiceCollection services)
        {
            var configSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Configuration = configSettings;

            Log.Logger = new LoggerConfiguration()
                             .ReadFrom.Configuration(configSettings)
                             .CreateLogger();

            services.AddSingleton<YahtzeeCard>().AddLogging(configure => configure.AddConsole())
                                               .AddLogging(configure => configure.AddEventLog())
                                               .AddLogging(configure => configure.AddSerilog())
                                               .AddLogging(configure => configure.AddDebug());
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var Scorecard = serviceProvider.GetService<YahtzeeCard>();
        }
    }
}
