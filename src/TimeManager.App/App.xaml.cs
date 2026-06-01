using System.Windows;
namespace TimeManager.App;
using TimeManager.Data;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DatabaseInitializer.Initialize();
    }
}