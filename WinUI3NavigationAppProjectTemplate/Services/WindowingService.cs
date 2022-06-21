using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinUI3NavigationAppProjectTemplate.Helpers;
using WinUI3NavigationAppProjectTemplate.Interfaces;

namespace WinUI3NavigationAppProjectTemplate.Services;
public class WindowingService : IWindowingService
{
    private readonly ISettingsService _settingsService;
    private Window? _window;

    public WindowingService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        SettingsName = $"{GetType().Namespace}.{GetType().Name}";
    }

    public string SettingsName { get; set; }

    public string WindowSizeSettingsKey { get; set; } = "WindowSize";

    public string IsAlwaysOnTopSettingsKey { get; set; } = "IsAlwaysOnTop";

    public void Initialize(Window window)
    {
        _window = window;
    }

    public (int Width, int Height)? LoadWindowSizeSettings()
    {
        if (_settingsService.TryLoad(SettingsName, WindowSizeSettingsKey, out (int Width, int Height)? windowSize) is true &&
            windowSize.HasValue is true)
        {
            return (windowSize.Value.Width, windowSize.Value.Height);
        }

        return null;
    }

    public bool SaveWindowSizeSettings(int width, int height)
    {
        return _settingsService.TrySave(SettingsName, WindowSizeSettingsKey, (width, height));
    }

    public (int Width, int Height)? GetWindowSize()
    {
        if (_window is not null)
        {
            SizeInt32 currentSize = _window.GetAppWindow().Size;
            return (currentSize.Width, currentSize.Height);
        }

        return null;
    }

    public void SetWindowSize(int width, int height)
    {
        if (_window is not null && width > 0 && height > 0)
        {
            _window.GetAppWindow().Resize(new SizeInt32(width, height));
        }
    }

    public bool? LoadIsAlwaysOnTopSettings()
    {
        if (_settingsService.TryLoad(SettingsName, IsAlwaysOnTopSettingsKey, out bool isAlwaysOnTop) is true)
        {
            return isAlwaysOnTop;
        }

        return null;
    }

    public bool SaveIsAlwaysOnTopSettings(bool isAlwaysOnTop)
    {
        return _settingsService.TrySave(SettingsName, IsAlwaysOnTopSettingsKey, isAlwaysOnTop);
    }

    public bool? GetIsAlwaysOnTop()
    {
        return _window is not null ? _window.GetIsAlwaysOnTop() : null;
    }

    public void SetIsAlwaysOnTop(bool isAlwaysOnTop)
    {
        _window?.SetIsAlwaysOnTop(isAlwaysOnTop);
    }
}