using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TimeManager.Core;

namespace TimeManager.App.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly TaskManager _manager = new();
        public ObservableCollection<TaskViewModel> Tasks { get; } = new();
        public ObservableCollection<TaskViewModel> CompletedTasks { get; } = new();
        private TaskViewModel? _currentTask;
        public TaskViewModel? CurrentTask
        {
            get => _currentTask;
            set
            {
                _currentTask = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentTaskText));
            }
        }
        public string CurrentTaskText =>
            CurrentTask?.DisplayText ?? "No current task";
        public void AddTask(TaskItem task)
        {
            _manager.AddTask(task);
            var taskViewModel = new TaskViewModel(task);
            Tasks.Add(taskViewModel);
            CurrentTask ??= taskViewModel;
        }
        public void CompleteCurrentTask(TimeSpan actualTimeSpent)
        {
            if (CurrentTask == null)
                return;
            var completedTask = CurrentTask;
            completedTask.Complete(DateTime.Now, actualTimeSpent);
            CompletedTasks.Add(completedTask);
            Tasks.Remove(completedTask);
            CurrentTask = Tasks.FirstOrDefault();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}