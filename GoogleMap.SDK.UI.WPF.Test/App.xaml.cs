using IOCServiceCollection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GoogleMap.SDK.Core;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollection = IOCServiceCollection.ServiceCollection;

namespace GoogleMap.SDK.UI.WPF.Test
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider provider = null;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<Window, MainWindow>();
            collection.AddGoogleMapCoreRegistration();
            collection.AddGoogleMapWPFRegistration();
            provider = collection.BuildServiceProvider();

            Window window = provider.GetService<Window>();
            window.Show();
        }
    }
}
