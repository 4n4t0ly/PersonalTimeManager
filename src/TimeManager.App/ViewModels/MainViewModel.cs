using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TimeManager.Core;
using TimeManager.Data;

namespace TimeManager.App.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly TaskManager _manager = new();
        public ObservableCollection<TaskViewModel> Tasks { get; } = new();
        public ObservableCollection<TaskViewModel> CompletedTasks { get; } = new();
        public ObservableCollection<string> Categories { get; } = new();
        private TaskViewModel? _currentTask;
        private readonly TaskRepository _repository = new();
        public MainViewModel()
        {
            LoadCategoriesFromDatabase();
            LoadTasksFromDatabase();
        }
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
            TaskItem savedTask = _repository.AddTask(task);
            _manager.AddTask(savedTask);
            var taskViewModel = new TaskViewModel(savedTask);
            Tasks.Add(taskViewModel);
            if (!string.IsNullOrWhiteSpace(savedTask.Category) &&
                !Categories.Contains(savedTask.Category))
                Categories.Add(savedTask.Category);
            CurrentTask ??= taskViewModel;
        }
        public void CompleteCurrentTask(TimeSpan actualTimeSpent)
        {
            if (CurrentTask == null)
                return;
            var completedTask = CurrentTask;
            completedTask.Complete(DateTime.Now, actualTimeSpent);
            _repository.CompleteTask(completedTask.Model);
            CompletedTasks.Add(completedTask);
            Tasks.Remove(completedTask);
            CurrentTask = Tasks.FirstOrDefault();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void AutoSortTasks()
        {
            List<TaskItem> sortedTasks = TaskAutoScheduler
                .BuildSchedule(Tasks.Select(t => t.Model));
            Tasks.Clear();
            foreach (TaskItem task in sortedTasks)
                Tasks.Add(new TaskViewModel(task));
            CurrentTask = Tasks.FirstOrDefault();
        }
        public void DeleteCurrentTask()
        {
            if (CurrentTask == null)
                return;
            var taskToDelete = CurrentTask;
            _repository.DeleteTask(taskToDelete.Model);
            _manager.RemoveTask(taskToDelete.Model);
            Tasks.Remove(taskToDelete);
            CurrentTask = Tasks.FirstOrDefault();
        }
        public void ClearCompletedTasks()
        {
            if (CompletedTasks == null)
                return;
            foreach (var completedTask in CompletedTasks)
            {
                _repository.DeleteTask(completedTask.Model);
                _manager.RemoveTask(completedTask.Model);
            }
        }
        private void LoadTasksFromDatabase()
        {
            foreach (TaskItem task in _repository.LoadActiveTasks())
            {
                var taskViewModel = new TaskViewModel(task);
                Tasks.Add(taskViewModel);
            }
            foreach (TaskItem task in _repository.LoadCompletedTasks())
            {
                var taskViewModel = new TaskViewModel(task);
                CompletedTasks.Add(taskViewModel);
            }
            CurrentTask = Tasks.FirstOrDefault();
        }
        private void LoadCategoriesFromDatabase()
        {
            Categories.Clear();
            foreach (string category in _repository.LoadCategoryNames())
                Categories.Add(category);
        }
    }
}