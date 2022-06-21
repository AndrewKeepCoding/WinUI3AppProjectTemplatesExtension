using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using WinUI3BasicAppProjectTemplate.ViewModels;

namespace WinUI3BasicAppProjectTemplate.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<MainPageViewModel>();
    }

    public MainPageViewModel ViewModel { get; }
}