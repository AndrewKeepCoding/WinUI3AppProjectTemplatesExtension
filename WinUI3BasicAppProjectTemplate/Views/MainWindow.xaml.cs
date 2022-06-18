using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using WinUI3BasicAppProjectTemplate.ViewModels;

namespace WinUI3BasicAppProjectTemplate.Views;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        ViewModel = Ioc.Default.GetRequiredService<MainWindowViewModel>();
    }

    public AppTitleBar TitleBar { get => AppTitleBar; }

    public MainWindowViewModel ViewModel { get; }
}