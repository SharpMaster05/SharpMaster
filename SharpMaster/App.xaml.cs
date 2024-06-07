using SharpMaster.Infrastucture;
using System.Windows;

namespace SharpMaster;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        DI.Init();
        base.OnStartup(e);
    }
}
