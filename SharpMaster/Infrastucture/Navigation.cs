using System.Windows.Controls;

namespace SharpMaster.Infrastucture;

internal class Navigation
{
    private readonly Animation _animation;

    public Frame Frame { get; set; }

    public Navigation(Animation animation)
    {
        _animation = animation;
        Frame = new Frame();
    }

    public void ChangePage(Page page)
    {
        _animation.ChanegePageAnimation(Frame, page);
    }
}
