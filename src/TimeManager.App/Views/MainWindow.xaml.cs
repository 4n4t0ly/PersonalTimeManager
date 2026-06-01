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
    private bool _isTaskPaused = false;
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
        TimeSpan actualTimeSpent = _stopwatch.Elapsed;
        if (_isCountdownMode && actualTimeSpent > _plannedTimeToDo)
            actualTimeSpent = _plannedTimeToDo;
        TimerLabel.Content = FormatTime(actualTimeSpent);
        _isTaskRunning = false;
        _isTaskPaused = false;
        StartButton.Content = "Start";
        PauseButton.Content = "Pause";
        _viewModel.CompleteCurrentTask(actualTimeSpent);
    }
    private static string FormatTime(TimeSpan time)
    {
        return time.ToString(@"hh\:mm\:ss");
    }
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if(_isTaskRunning || _isTaskPaused)
            FinishTask();
        else
            StartTask();
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
        _isTaskPaused=false;
        PauseButton.Content = "Pause";
        StartButton.Content = "Done";
    }
    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.CurrentTask == null)
        {
            MessageBox.Show("No task selected.");
            return;
        }
        MessageBoxResult result = MessageBox.Show(
            "Delete selected task?",
            "Delete task",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes)
            return;
        ResetTimerState();
        _viewModel.DeleteCurrentTask();
    }
    private void ResetTimerState()
    {
        _stopwatch.Reset();
        _isTaskRunning = false;
        _isTaskPaused = false;
        _isCountdownMode = false;
        _plannedTimeToDo = TimeSpan.Zero;
        TimerLabel.Content = "00:00:00";
        StartButton.Content = "Start";
        PauseButton.Content = "Pause";
    }
    private void PauseButton_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.CurrentTask == null)
            return;
        if(_isTaskRunning)
            PauseTask();
        else if (_isTaskPaused)
            ResumeTask();
    }
    private void PauseTask()
    {
        _stopwatch.Stop();
        _isTaskRunning = false;
        _isTaskPaused = true;
        PauseButton.Content = "Resume";
    }
    private void ResumeTask()
    {
        _stopwatch.Start();
        _isTaskRunning = true;
        _isTaskPaused = false;
        PauseButton.Content = "Pause";
    }
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new Views.AddTaskWindow(_viewModel.Categories);
        window.TaskCreated += task =>
            _viewModel.AddTask(task);
        window.ShowDialog();
        _viewModel.AutoSortTasks();
    }

    private void CalendarButton_Click(object sender, RoutedEventArgs e)
    {

    }
}