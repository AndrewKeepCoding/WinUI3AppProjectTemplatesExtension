using System.Collections.Generic;

namespace WinUI3NavigationAppProjectTemplate.Interfaces;
public interface ILocalizationService
{
    IEnumerable<string> AvailableLanguages { get; }

    string LanguageSettingsKey { get; set; }

    string? GetLanguage();

    void SetLanguage(string language);
}