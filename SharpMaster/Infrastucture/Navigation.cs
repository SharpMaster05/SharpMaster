using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SharpMaster.Infrastucture;

internal class Navigation
{
    public Frame Frame { get; set; }

    public Navigation()
    {
        Frame = new Frame();
    }

    public void ChangePage(Page page)
    {
        var time = TimeSpan.FromSeconds(0.5);
        
        DoubleAnimation hide = new DoubleAnimation(1, 0, time);
        hide.Completed += (s, e) =>
        {
            Frame.Content = page;

            DoubleAnimation show = new(0, 1, time);
            Frame.BeginAnimation(FrameworkElement.OpacityProperty, show);

        };
        Frame.BeginAnimation(FrameworkElement.OpacityProperty, hide);
    }
}
