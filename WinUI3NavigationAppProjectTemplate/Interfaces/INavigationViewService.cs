using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace WinUI3NavigationAppProjectTemplate.Interfaces;

public interface INavigationViewService
{
    void Initialize(NavigationView navigationView, Frame contentFrame);

    void NavigateTo(Type pageType, NavigationTransitionInfo navigationTransitionInfo);
}