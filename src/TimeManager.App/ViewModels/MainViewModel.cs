using System.Collections.ObjectModel;
using TimeManager.Core;

namespace TimeManager.App.ViewModels
{
    public class MainViewModel
    {
        private readonly TaskManager _manager = new();
        public ObservableCollection<TaskViewModel> Tasks { get;}
        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskViewModel>();
            var task = new TaskItem("Work of the year", "Education", "");
            AddTask(task);
        }
        public void AddTask(TaskItem task)
        {
            _manager.AddTask(task);
            Tasks.Add(new TaskViewModel(task));
        }
    }
}