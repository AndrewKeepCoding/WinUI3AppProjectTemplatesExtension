using System.Collections.Generic;

namespace WinUI3BasicAppProjectTemplate.Interfaces;

public interface ILocalizationService
{
    IEnumerable<string> AvailableLanguages { get; }

    string LanguageSettingsKey { get; set; }

    string? GetLanguage();

    void SetLanguage(string language);
}