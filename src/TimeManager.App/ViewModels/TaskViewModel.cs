using System.ComponentModel;
using System.Runtime.CompilerServices;
using TimeManager.Core;

namespace TimeManager.App.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private readonly TaskItem _task;
        public TaskViewModel(TaskItem task)
        {
            _task = task;
        }
        public string Name => _task.Name;
        public string? Category => _task.Category;
        public string? Description => _task.Description;
        public byte Priority => _task.Priority;
        public byte Difficulty => _task.Difficulty;
        public DateTime? DeadLine => _task.DeadLine;
        public TimeSpan? TimeToDo => _task.TimeToDo;
        public DateTime? CompletedAt => _task.CompletedAt;
        public TimeSpan? ActualTimeSpent => _task.ActualTimeSpent;
        public string ActualTimeSpentText => _task.ActualTimeSpent?.ToString(@"hh\:mm\:ss") ?? "";
        public bool IsDone => _task.IsDone;
        public string DisplayText =>
            $"Task name: {Name}\n" +
            $"Category: {Category}\n" +
            $"P: {Priority} | D: {Difficulty}\n" +
            $"{Description}";
        public event PropertyChangedEventHandler? PropertyChanged;
        public void MarkDone()
        {
            _task.MarkDone();
            OnPropertyChanged(nameof(IsDone));
        }
        public void Complete (DateTime completedAt, TimeSpan actualTimeSpent)
        {
            _task.Complete (completedAt, actualTimeSpent);
            OnPropertyChanged (nameof(IsDone));
            OnPropertyChanged(nameof(ActualTimeSpent));
            OnPropertyChanged (nameof(ActualTimeSpentText));
            OnPropertyChanged(nameof(CompletedAt));
        }
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}