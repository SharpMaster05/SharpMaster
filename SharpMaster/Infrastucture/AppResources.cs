using System.Windows;

namespace SharpMaster.Infrastucture;

internal class AppResources
{
    public List<Uri> DarkThemeResources { get; private set; }
    public List<Uri> LightThemeResources { get; private set; }

    public AppResources()
    {
        SetDarkThemeResources();
        SetLightThemeResources();
    }

    public void ChangeTheme(List<Uri> themeResources)
    {
        Application.Current.Resources.Clear();

        foreach(var i in themeResources)
        {
            var resource = new ResourceDictionary() { Source = i };
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
    }

    private void SetDarkThemeResources()
    {
        DarkThemeResources = new List<Uri>()
        {
            new Uri("Styles/DarkStyles/UIElements/BordersStyle.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/DarkStyles/UIElements/ButtonsStyle.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/DarkStyles/UIElements/TextBlockStyles.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/DarkStyles/UIElements/TextBoxStyles.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/DarkStyles/AppStyles.xaml", UriKind.RelativeOrAbsolute)
        };
    }
    private void SetLightThemeResources()
    {
        LightThemeResources = new List<Uri>()
        {
            new Uri("Styles/LightStyles/UIElements/BordersStyle.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/LightStyles/UIElements/ButtonsStyle.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/LightStyles/UIElements/TextBlockStyles.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/LightStyles/UIElements/TextBoxStyles.xaml", UriKind.RelativeOrAbsolute),
            new Uri("Styles/LightStyles/AppStyles.xaml", UriKind.RelativeOrAbsolute)
        };
    }

}
