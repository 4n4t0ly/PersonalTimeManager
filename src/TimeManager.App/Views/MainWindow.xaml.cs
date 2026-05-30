using System.Text;
using System;
using System.Diagnostics;
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
    private readonly DispatcherTimer _uiTimer;
    private readonly Stopwatch _stopwatch;
    private bool _isTaskRunning = false;
    private bool _isCountdownMode = false;
    private TimeSpan _plannedTimeToDo = TimeSpan.Zero;
    private readonly MainViewModel _viewModel;
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        DataContext = _viewModel;
        _stopwatch = new Stopwatch();
        _uiTimer = new DispatcherTimer();
        _uiTimer.Interval = TimeSpan.FromSeconds(1);
        _uiTimer.Tick += UiTimer_Tick;
        _uiTimer.Start();
        CurrentTime.Content = DateTime.Now.ToString("HH:mm");
        TimerLabel.Content = "00:00:00";
    }
    void UiTimer_Tick(object? sender, EventArgs e)
    {
        CurrentTime.Content = DateTime.Now.ToString("HH:mm");
        if (!_isTaskRunning)
            return;
        if(_isCountdownMode)
        {
            TimeSpan remainingTime = _plannedTimeToDo - _stopwatch.Elapsed;
            if(remainingTime <= TimeSpan.Zero)
            {
                TimerLabel.Content = "00:00:00";
                FinishTask();
                return;
            }
            TimerLabel.Content = FormatTime(remainingTime);
        }
        else
        {
            TimeSpan elapsedTime = _stopwatch.Elapsed;
            TimerLabel.Content = FormatTime(elapsedTime);
        }
    }
    private void FinishTask()
    {
        if (_viewModel.CurrentTask == null)
            return;
        _stopwatch.Stop();
        TimeSpan finalTime;
        if (_isCountdownMode)
            finalTime = _plannedTimeToDo;
        else
            finalTime = _stopwatch.Elapsed;
        TimerLabel.Content = FormatTime(finalTime);
        _isTaskRunning = false;
        StartButton.Content = "Start";
        _viewModel.CompleteCurrentTask(finalTime);
    }
    private static string FormatTime(TimeSpan time)
    {
        return time.ToString(@"hh\:mm\:ss");
    }
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if(!_isTaskRunning)
            StartTask();
        else
            FinishTask();
    }
    private void StartTask()
    {
        if (_viewModel.CurrentTask == null)
        {
            MessageBox.Show("No task selected.");
            return;
        }
        _plannedTimeToDo = _viewModel.CurrentTask.TimeToDo ?? TimeSpan.Zero;
        _isCountdownMode = _plannedTimeToDo > TimeSpan.Zero;
        _stopwatch.Restart();
        _isTaskRunning=true;
        StartButton.Content = "Done";
    }
    private void SkipButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void PauseButton_Click(object sender, RoutedEventArgs e)
    {

    }
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new Views.AddTaskWindow();
        window.TaskCreated += task =>
            _viewModel.AddTask(task);
        window.ShowDialog();
    }

    private void CalendarButton_Click(object sender, RoutedEventArgs e)
    {

    }
}