using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUI3NavigationAppProjectTemplate.ViewModels;

namespace WinUI3NavigationAppProjectTemplate.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<SettingsPageViewModel>();
    }

    public SettingsPageViewModel ViewModel { get; }

    private void LanguageGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LanguageButton.Flyout.Hide();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.InitializeCommand.Execute(this);
    }
}