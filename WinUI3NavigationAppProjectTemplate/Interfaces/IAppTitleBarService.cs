using Windows.UI;
using WinUI3NavigationAppProjectTemplate.Views;

namespace WinUI3NavigationAppProjectTemplate.Interfaces;
public interface IAppTitleBarService
{
    void Initialize(AppTitleBar appTitleBar);

    string? LoadTextSettings();

    bool SaveTextSettings(string text);

    string? GetText();

    void SetText(string text);

    Color? LoadBackgroundSettings();

    bool SaveBackgroundSettings(Color color);

    Color? GetBackground();

    void SetBackground(Color color);

    Color? LoadForegroundSettings();

    bool SaveForegroundSettings(Color color);

    Color? GetForeground();

    void SetForeground(Color color);
}