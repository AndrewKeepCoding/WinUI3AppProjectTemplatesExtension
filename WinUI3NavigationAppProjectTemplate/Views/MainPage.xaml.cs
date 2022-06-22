using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using WinUI3NavigationAppProjectTemplate.ViewModels;

namespace WinUI3NavigationAppProjectTemplate.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<MainPageViewModel>();
    }

    public MainPageViewModel ViewModel { get; }
}