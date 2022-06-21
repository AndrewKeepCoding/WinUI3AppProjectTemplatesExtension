using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Globalization;
using WinUI3BasicAppProjectTemplate.Interfaces;

namespace WinUI3BasicAppProjectTemplate.Services;

public class LocalizationService : ILocalizationService
{
    private readonly ResourceManager _resourceManager;
    private readonly ResourceContext _resourceContext;

    public LocalizationService()
    {
        _resourceManager = new();
        _resourceContext = _resourceManager.CreateResourceContext();
        AvailableLanguages = ApplicationLanguages.ManifestLanguages;
        SettingsName = $"{GetType().Namespace}.{GetType().Name}";
    }

    public string SettingsName { get; set; }

    public string LanguageSettingsKey { get; set; } = "Language";

    public IEnumerable<string> AvailableLanguages { get; }

    public string GetLanguage()
    {
        return ApplicationLanguages.PrimaryLanguageOverride;
    }

    /// <summary>
    /// </summary>
    /// <param name="language"></param>
    /// <remarks>Restart the app to apply this change.</remarks>
    /// <exception cref="InvalidLanguageException"></exception>
    public void SetLanguage(string language)
    {
        if (AvailableLanguages.Contains(language) is true)
        {
            ApplicationLanguages.PrimaryLanguageOverride = language;
            _resourceContext.QualifierValues["Language"] = language;
            return;
        }

        throw new InvalidLanguageException(language, AvailableLanguages);
    }

    public class InvalidLanguageException : Exception
    {
        public InvalidLanguageException(string? language, IEnumerable<string> availableLanguages)
        {
            Language = language;
            AvailableLanguages = availableLanguages;
        }

        public string? Language { get; }

        public IEnumerable<string> AvailableLanguages { get; }
    }
}