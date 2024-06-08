using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharpMaster.ViewModels.Windows;

internal class MainViewModel : Notifier
{
    private readonly Animation _animation;
    public Navigation Navigation { get; set; }

    private Page _personPage;
    private Page _buildPage;

    public MainViewModel(Animation animation, Navigation navigation)
    {
        _animation = animation;
        Navigation = navigation;

        _personPage = new Views.Pages.PersonView();
        _buildPage = new Views.Pages.BuildView();
    }
    public ICommand CloseWindowCommand => new Command(x =>
    {
        if(x is Border border) _animation.CloseAnimation(border);
    });

    public ICommand MaximizeWindowCommand => new Command(x =>
    {
        if(x is Window window) _animation.MaximizeAnimation(window);
    });

    public ICommand MinimizeWindowCommand => new Command(x =>
    {
        if (x is Window window) _animation.MinimizeAnimation(window);
    });

    public ICommand NavigateToPersonCommand => new Command(x => Navigation.ChangePage(_personPage));
    public ICommand NavigateToBuildCommand => new Command(x => Navigation.ChangePage(_buildPage));
}
