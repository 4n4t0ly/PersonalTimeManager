using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Core
{
    public static class TaskAutoScheduler
    {
        private static readonly TimeSpan DefaultTaskTime = TimeSpan.FromMinutes(25);
        private static readonly TimeSpan CategoryBlockTime = TimeSpan.FromMinutes(100);
        private static readonly TimeSpan DeadlineUrgencyWindow = TimeSpan.FromHours(24);
        public static List<TaskItem> BuildSchedule(IEnumerable<TaskItem> tasks)
        {
            DateTime now = DateTime.Now;
            List<TaskItem> activeTasks = tasks
                .Where(t => !t.IsDone)
                .ToList();
            List<TaskItem> urgentTasks = activeTasks
                .Where(t => IsDeadlineUrgent(t, now))
                .OrderBy(t => t.DeadLine)
                .ThenByDescending(t => t.Priority)
                .ThenBy(t => t.Difficulty)
                .ToList();
            List<TaskItem> regularTasks = activeTasks
                .Where(t => !urgentTasks.Contains(t))
                .ToList();
            List<TaskItem> result = new();
            result.AddRange(urgentTasks);
            result.AddRange(BuildCategoryBlocks(regularTasks));
            return result;
        }
        private static bool IsDeadlineUrgent(TaskItem task, DateTime now)
        {
            if(task.DeadLine == null)
                return false;
            return task.DeadLine.Value <= now + DeadlineUrgencyWindow;
        }
        private static List<TaskItem> BuildCategoryBlocks(List<TaskItem> tasks)
        {
            List<TaskItem> result = new();

            var categoryQueues = tasks
                .GroupBy(t => string.IsNullOrWhiteSpace(t.Category) ? "None" : t.Category)
                .Select(g => new Queue<TaskItem>(
                    g.OrderByDescending(t => t.Priority)
                     .ThenBy(t => t.Difficulty)
                     .ThenBy(t => t.DeadLine ?? DateTime.MaxValue)
                ))
                .ToList();

            while (categoryQueues.Any(q => q.Count > 0))
            {
                foreach (Queue<TaskItem> queue in categoryQueues.Where(q => q.Count > 0))
                {
                    TimeSpan blockTime = TimeSpan.Zero;
                    bool canAddMoreTasks = true;

                    while (queue.Count > 0 && canAddMoreTasks)
                    {
                        TaskItem nextTask = queue.Peek();
                        TimeSpan taskTime = GetTaskTime(nextTask);
                        bool taskFitsBlock = blockTime + taskTime <= CategoryBlockTime;
                        bool blockIsEmpty = blockTime == TimeSpan.Zero;
                        if (taskFitsBlock || blockIsEmpty)
                        {
                            result.Add(queue.Dequeue());
                            blockTime += taskTime;
                        }
                        else
                            canAddMoreTasks = false;
                    }
                }
            }
            return result;
        }
        private static TimeSpan GetTaskTime(TaskItem task)
        {
            return task.TimeToDo ?? DefaultTaskTime;
        }

    }
}