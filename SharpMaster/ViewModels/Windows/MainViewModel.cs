using SharpMaster.Infrastucture;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharpMaster.ViewModels.Windows;

internal class MainViewModel : Notifier
{
    private readonly Animation _animation;
    private readonly AppResources _appResources;
    public Navigation Navigation { get; set; }
    public bool IsChecked { get; set; }

    private Page _personPage;
    private Page _buildPage;
    private Page _regionPage;

    public MainViewModel(Animation animation, Navigation navigation)
    {
        _animation = animation;
        Navigation = navigation;

        _personPage = new Views.Pages.PersonView();
        _buildPage = new Views.Pages.BuildView();
        _regionPage = new Views.Pages.RegionView();

        Navigation.Frame.Content = _personPage;

        _appResources = new AppResources();
    }
    public ICommand CloseWindowCommand => new Command(x =>
    {
        if(x is Border border) _animation.CloseAnimation(border, "MainWindow");
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
    public ICommand NavigateToRegionCommand => new Command(x => Navigation.ChangePage(_regionPage));
    public ICommand ChangeThemeCommand => new Command(x => 
    {
        if (!IsChecked)
            _appResources.ChangeTheme(_appResources.DarkThemeResources);
        else
            _appResources.ChangeTheme(_appResources.LightThemeResources);
    });
}
