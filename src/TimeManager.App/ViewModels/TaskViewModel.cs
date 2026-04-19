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

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}