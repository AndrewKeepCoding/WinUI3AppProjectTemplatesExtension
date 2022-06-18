using Microsoft.UI.Xaml;
using WinUI3BasicAppProjectTemplate.Interfaces;
using WinUI3BasicAppProjectTemplate.Views;

namespace WinUI3BasicAppProjectTemplate.Services;

public class AppActivationService : IAppActivationService
{
    private readonly MainWindow _mainWindow;
    private readonly IWindowingService _windowingService;
    private readonly IAppTitleBarService _appTitleBarService;
    private readonly IAppThemeService _appThemeService;
    private readonly ILocalizationService _localizationService;

    public AppActivationService(
        MainWindow mainWindow,
        IWindowingService windowingService,
        IAppTitleBarService appTitleBarService,
        ISettingsService settingsService,
        IAppThemeService appThemeService,
        ILocalizationService localizationService)
    {
        _mainWindow = mainWindow;
        _windowingService = windowingService;
        _appTitleBarService = appTitleBarService;
        _appThemeService = appThemeService;
        _localizationService = localizationService;

        settingsService.RemoveAllSettings();
    }

    public void Activate(object activationArgs)
    {
        InitializeServices();
        _mainWindow.Activate();
    }

    private void InitializeServices()
    {
        _windowingService.Initialize(_mainWindow);

        if (_windowingService.LoadWindowSizeSettings() is (int Width, int Height))

            if (Width is not 0 && Height is not 0)
            {
                _windowingService.SetWindowSize(Width, Height);
            }

        _appTitleBarService.Initialize(_mainWindow.TitleBar);

        if (_mainWindow.Content is FrameworkElement rootElement)
        {
            _appThemeService.Initialize(rootElement);
        }
    }
}