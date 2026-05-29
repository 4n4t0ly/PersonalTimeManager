using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeManager.Core;
using TimeManager.App.ViewModels;
using System.Windows.Threading;

namespace TimeManager.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        DataContext = _viewModel;
        DispatcherTimer LiveTime = new DispatcherTimer();
        LiveTime.Interval = TimeSpan.FromSeconds(1);
        LiveTime.Tick += timer_Tick;
        LiveTime.Start();
    }
    void timer_Tick(object sender, EventArgs e)
    {
        CurrentTime.Content = DateTime.Now.ToString("HH:mm");
    }
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void SkipButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ShuffleButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new Views.AddTaskWindow();
        window.ShowDialog();
        if(window.CreatedTask != null)
        {
            _viewModel.AddTask(window.CreatedTask);
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}