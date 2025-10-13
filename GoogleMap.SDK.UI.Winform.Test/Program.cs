using GoogleMap.SDK.Core.Component.AutoComplete;
using GoogleMap.SDK.UI.Winform.Components.AutoComplete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;
using ServiceCollection = IOCServiceCollection.ServiceCollection;
using GoogleMap.SDK.API;
using GoogleMap.SDK.Core;
namespace GoogleMap.SDK.UI.Winform.Test
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        /// 
        public static IServiceProvider provider = null;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var collection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            collection.Add(new ServiceDescriptor(typeof(IConfiguration), configuration));
            collection.AddSingleton<Form, Form1>();
            collection.AddGoogleMapCoreRegistration(configuration);
            collection.AddGoogleMapWinformRegistration(configuration);
            provider = collection.BuildServiceProvider();

            Form form = provider.GetService<Form>();
            Application.Run(form);
        }
    }
}
