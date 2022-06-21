using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using WinUI3NavigationAppProjectTemplate.Interfaces;

namespace WinUI3NavigationAppProjectTemplate.Services;
public class AppThemeService : IAppThemeService
{
    private readonly ISettingsService _settingsService;
    private FrameworkElement? _rootElement;

    public AppThemeService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        SettingsName = $"{GetType().Namespace}.{GetType().Name}";
    }

    public string SettingsName { get; set; }

    public string ThemeNameSettingsKey { get; set; } = "ThemeName";

    public IEnumerable<ElementTheme> AvailableThemes { get => Enum.GetValues<ElementTheme>(); }

    public void Initialize(FrameworkElement rootElement)
    {
        _rootElement = rootElement;
    }

    public ElementTheme? LoadThemeSettings()
    {
        if (_settingsService.TryLoad(SettingsName, ThemeNameSettingsKey, out string? themeName) is true)
        {
            if (Enum.TryParse(themeName, out ElementTheme theme) is true)
            {
                return theme;
            }
        }

        return null;
    }

    public bool SaveThemeSettings(ElementTheme theme)
    {
        return _settingsService.TrySave(SettingsName, ThemeNameSettingsKey, theme.ToString());
    }

    public ElementTheme? GetTheme()
    {
        return (_rootElement is not null) ? _rootElement.RequestedTheme : null;
    }

    public void SetTheme(ElementTheme theme)
    {
        if (_rootElement is not null)
        {
            _rootElement.RequestedTheme = theme;
        }
    }
}