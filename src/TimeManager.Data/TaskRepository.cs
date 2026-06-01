using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeManager.Core;

namespace TimeManager.Data
{
    public class TaskRepository
    {
        public void AddCategory(string name)
        {
            using var db = new TimeManagerDbContext();
            bool exists = db.Categories.Any(c => c.Name == name);
            if (exists)
                return;
            db.Categories.Add(new CategoryEntity
            {
                Name = name
            });
            db.SaveChanges();
        }
        public List<CategoryEntity> LoadCategories()
        {
            using var db = new TimeManagerDbContext();
            return db.Categories
                .OrderBy(c => c.Name)
                .ToList();
        }
        public TaskItem AddTask(TaskItem task)
        {
            using var db = new TimeManagerDbContext();
            CategoryEntity? category = null;
            if (!string.IsNullOrWhiteSpace(task.Category))
            {
                category = db.Categories
                    .FirstOrDefault(c => c.Name == task.Category);
                if (category == null)
                {
                    category = new CategoryEntity
                    {
                        Name = task.Category
                    };
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
            }
            var entity = new TaskEntity
            {
                Name = task.Name,
                Description = task.Description,
                CategoryId = category?.Id,
                DeadLine = task.DeadLine,
                TimeToDoSeconds = task.TimeToDo.HasValue
                    ? (int)task.TimeToDo.Value.TotalSeconds
                    : null,
                Priority = task.Priority,
                Difficulty = task.Difficulty,
                IsDone = task.IsDone,
                CompletedAt = task.CompletedAt,
                ActualTimeSpentSeconds = task.ActualTimeSpent.HasValue
                    ? (int)task.ActualTimeSpent.Value.TotalSeconds
                    : null
            };
            db.Tasks.Add(entity);
            db.SaveChanges();
            return ToTaskItem(entity, category);
        }
        public List<TaskItem> LoadActiveTasks()
        {
            using var db = new TimeManagerDbContext();
            return db.Tasks
                .Include(t => t.Category)
                .Where(t => !t.IsDone)
                .ToList()
                .Select(t => ToTaskItem(t, t.Category))
                .ToList();
        }
        public List<TaskItem> LoadCompletedTasks()
        {
            using var db = new TimeManagerDbContext();
            return db.Tasks
                .Include(t => t.Category)
                .Where(t => t.IsDone)
                .ToList()
                .Select(t => ToTaskItem(t, t.Category))
                .ToList();
        }
        private static TaskItem ToTaskItem(TaskEntity entity, CategoryEntity? category)
        {
            var task = new TaskItem(
                entity.Name,
                category?.Name ?? "",
                entity.Description ?? "",
                entity.Priority,
                entity.Difficulty,
                entity.Id);
            if (entity.DeadLine.HasValue)
                task.SetDeadLine(entity.DeadLine.Value);
            if(entity.TimeToDoSeconds.HasValue)
                task.SetTimeToDo(TimeSpan.FromSeconds(entity.TimeToDoSeconds.Value));
            if(entity.IsDone && entity.CompletedAt.HasValue &&
                entity.ActualTimeSpentSeconds.HasValue)
            {
                task.Complete(
                    entity.CompletedAt.Value,
                    TimeSpan.FromSeconds(entity.ActualTimeSpentSeconds.Value));
            }
            return task;
        }
        public void CompleteTask(TaskItem task)
        {
            using var db = new TimeManagerDbContext();
            TaskEntity? entity = db.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (entity == null)
                return;
            entity.IsDone = true;
            entity.CompletedAt = task.CompletedAt;
            entity.ActualTimeSpentSeconds = task.ActualTimeSpent.HasValue
                ? (int)task.ActualTimeSpent.Value.TotalSeconds
                : null;
            db.SaveChanges();
        }
        public void DeleteTask(TaskItem task)
        {
            using var db = new TimeManagerDbContext();
            TaskEntity? entity = db.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (entity == null)
                return;
            db.Tasks.Remove(entity);
            db.SaveChanges();
        }
    }
}
