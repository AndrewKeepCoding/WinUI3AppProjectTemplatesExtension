using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using WinUI3NavigationAppProjectTemplate.Interfaces;
using WinUI3NavigationAppProjectTemplate.Services;
using WinUI3NavigationAppProjectTemplate.ViewModels;
using WinUI3NavigationAppProjectTemplate.Views;

namespace WinUI3NavigationAppProjectTemplate;
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        InitializeComponent();
        UnhandledException += App_UnhandledException;
        _host = BuildHost();
        Ioc.Default.ConfigureServices(_host.Services);
    }

    private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        IAppActivationService appWindowService = Ioc.Default.GetRequiredService<IAppActivationService>();
        appWindowService.Activate(args);
    }

    private static IHost BuildHost() => Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            _ = services
                .AddSingleton<ISettingsService, LocalSettingsService>()
                .AddSingleton<IAppThemeService, AppThemeService>()
                .AddSingleton<ILocalizationService, LocalizationService>()
                .AddSingleton<IAppTitleBarService, AppTitleBarService>()
                .AddSingleton<IWindowingService, WindowingService>()
                .AddSingleton<IAppActivationService, AppActivationService>()
                .AddSingleton<MainWindowViewModel>()
                .AddSingleton<MainPageViewModel>()
                .AddSingleton<MainWindow>();
        })
        .Build();
}