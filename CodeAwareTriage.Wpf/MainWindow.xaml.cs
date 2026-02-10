using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CodeAwareTriage.UI.Services;
using System.Net.Http;
using System.IO;

namespace CodeAwareTriage.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddBlazorWebViewDeveloperTools();
        
        // Configuration
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
            
        serviceCollection.AddSingleton<IConfiguration>(config);
        
        // Services
        serviceCollection.AddScoped<HttpClient>(); // For GeminiService
        serviceCollection.AddScoped<ExcelService>();
        serviceCollection.AddScoped<GeminiService>();

        Resources.Add("Services", serviceCollection.BuildServiceProvider());
    }
}
