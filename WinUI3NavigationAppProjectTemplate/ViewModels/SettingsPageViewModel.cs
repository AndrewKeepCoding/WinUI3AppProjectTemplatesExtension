using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using WinUI3NavigationAppProjectTemplate.Interfaces;

namespace WinUI3NavigationAppProjectTemplate.ViewModels;

[ObservableObject]
public partial class SettingsPageViewModel
{
    private readonly IWindowingService _windowingService;
    private readonly IAppTitleBarService _appTitleBarService;
    private readonly IAppThemeService _appThemeService;
    private readonly ILocalizationService _localizationService;

    [ObservableProperty]
    private int _windowWidth;

    [ObservableProperty]
    private int _windowHeight;

    [ObservableProperty]
    private Color _titleBarBackground;

    [ObservableProperty]
    private Color _titleBarForeground;

    [ObservableProperty]
    private IEnumerable<ThemeViewModel> _availableAppThemes;

    [ObservableProperty]
    private ThemeViewModel? _appTheme;

    [ObservableProperty]
    private IEnumerable<LanguageViewModel> _availableLanguages;

    [ObservableProperty]
    private LanguageViewModel? _language;

    [ObservableProperty]
    private bool _isLocalizationChanged = false;

    public SettingsPageViewModel(
        IWindowingService windowingService,
        IAppTitleBarService appTitleBarService,
        IAppThemeService appThemeService,
        ILocalizationService localizationService)
    {
        _windowingService = windowingService;
        _appTitleBarService = appTitleBarService;
        _appThemeService = appThemeService;
        _localizationService = localizationService;

        _availableAppThemes = _appThemeService.AvailableThemes
            .Select(theme => new ThemeViewModel(theme, theme.ToString().GetLocalized("Resources")));

        _availableLanguages = _localizationService.AvailableLanguages
            .Select(language => new LanguageViewModel(language, language.GetLocalized("Resources")));

        string? language = _localizationService.GetLanguage();

        if (language is not null && _availableLanguages.Count(i => i.Language == language) > 0)
        {
            _language = new LanguageViewModel(language, language.GetLocalized("Resources"));
        }
    }

    partial void OnTitleBarBackgroundChanged(Color value)
    {
        _appTitleBarService.SetBackground(value);
        _ = _appTitleBarService.SaveBackgroundSettings(value);
    }

    partial void OnTitleBarForegroundChanged(Color value)
    {
        _appTitleBarService.SetForeground(value);
        _ = _appTitleBarService.SaveForegroundSettings(value);
    }

    partial void OnAppThemeChanged(ThemeViewModel? value)
    {
        if (value is not null)
        {
            _appThemeService.SetTheme(value.Theme);
        }
    }

    partial void OnLanguageChanged(LanguageViewModel? value)
    {
        if (value is not null && value.Language != _localizationService.GetLanguage())
        {
            _localizationService.SetLanguage(value.Language);
            IsLocalizationChanged = true;
        }
    }

    [RelayCommand]
    private void Initialize()
    {
        (int Width, int Height)? windowSize = _windowingService.LoadWindowSizeSettings();
        windowSize ??= _windowingService.GetWindowSize();

        if (windowSize is not null)
        {
            WindowWidth = windowSize.Value.Width;
            WindowHeight = windowSize.Value.Height;
        }

        if (_appTitleBarService.LoadBackgroundSettings() is Color background)
        {
            TitleBarBackground = background;
        }

        if (_appTitleBarService.LoadForegroundSettings() is Color foreground)
        {
            TitleBarForeground = foreground;
        }

        ElementTheme? theme = _appThemeService.LoadThemeSettings();
        theme ??= _appThemeService.GetTheme();

        if (_availableAppThemes.FirstOrDefault(item => item.Theme.Equals(theme)) is ThemeViewModel themeItemViewModel)
        {
            AppTheme = themeItemViewModel;
        }
    }

    [RelayCommand]
    private void UpdateWindowSize()
    {
        _windowingService.SetWindowSize(WindowWidth, WindowHeight);
        _ = _windowingService.SaveWindowSizeSettings(WindowWidth, WindowHeight);
    }

    [RelayCommand]
    private void ChangeTheme(string themeString)
    {
        if (Enum.TryParse(themeString, out ElementTheme theme) is true)
        {
            _appThemeService.SetTheme(theme);
            _ = _appThemeService.SaveThemeSettings(theme);
        }
    }
}