namespace TimeManager.Core
{
    public class TaskManager
    {
        private readonly List<TaskItem> _tasks = new();
        public IReadOnlyList<TaskItem> Task => _tasks;
        public void AddTask(TaskItem task)
        {
            _tasks.Add(task);
        }
        public void RemoveTask(TaskItem task)
        {
            _tasks.Remove(task);
        }
        public void MarkDone(TaskItem task)
        {
            task.MarkDone();
        }
    }
}