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
        public bool IsDone => _task.IsDone;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}