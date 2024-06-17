using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SharpMaster.Infrastucture;

internal class Animation
{
    public void CloseAnimation(Border border, string windowTitle)
    {
        var width = (int)border.ActualWidth;
        var time = TimeSpan.FromSeconds(0.7);

        DoubleAnimation animation = new(width, 0, time)
        {
            EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 }
        };

        animation.Completed += (obj, e) => 
        {
            foreach (Window i in App.Current.Windows)
            {
                if(i.Title == windowTitle)
                    i.Close();
            }
        };

        border.BeginAnimation(FrameworkElement.WidthProperty , animation);
    }
    
    public void ChanegePageAnimation(Frame frame, Page page)
    {
        var time = TimeSpan.FromSeconds(0.7);

        DoubleAnimation hideAnimation = new(1, 0, time) { EasingFunction = new PowerEase { Power = 3, EasingMode = EasingMode.EaseInOut } };

        hideAnimation.Completed += (obj, e) => 
        {
            frame.Content = page;
            DoubleAnimation fadeAnimation = new(0, 1, time) { EasingFunction = new PowerEase { Power = 3 , EasingMode = EasingMode.EaseInOut } };
            frame.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
        };

        frame.BeginAnimation(UIElement.OpacityProperty, hideAnimation);
    }

    public void MaximizeAnimation(Window window)
    {
        var time = TimeSpan.FromSeconds(0.3);

        DoubleAnimation animation = new(1, 0, time);

        animation.Completed += (sender, e) =>
        {
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

            DoubleAnimation showAnimation = new DoubleAnimation(0, 1, time);
            window.BeginAnimation(UIElement.OpacityProperty , showAnimation);
        };

        window.BeginAnimation(UIElement.OpacityProperty, animation);
    }

    public void MinimizeAnimation(Window window)
    {
        var time = TimeSpan.FromSeconds(0.3);

        DoubleAnimation animation = new(1, 0, time);
        animation.Completed += (s, e) =>
        {
            window.WindowState = WindowState.Minimized;
            DoubleAnimation showAnimation = new DoubleAnimation(0, 1, time);
            window.BeginAnimation(UIElement.OpacityProperty, showAnimation);
        };
        
        window.BeginAnimation(UIElement.OpacityProperty , animation);
    }
}
