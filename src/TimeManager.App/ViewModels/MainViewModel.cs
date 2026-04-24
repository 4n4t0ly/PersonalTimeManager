using System.Collections.ObjectModel;
using TimeManager.Core;

namespace TimeManager.App.ViewModels
{
    public class MainViewModel
    {
        private readonly TaskManager _manager = new();
        public ObservableCollection<TaskItem> Tasks { get; }
        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            var task = new TaskItem("Work of the year", "Education", "");
            _manager.AddTask(task);
            Tasks.Add(task);
        }
    }
}