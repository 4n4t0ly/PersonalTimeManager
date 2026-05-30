using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Core
{
    public class TaskItem
    {
        public string Name { get; private set; }
        public string? Category { get; private set; }
        public string? Description { get; private set; }
        public DateTime? DeadLine { get; private set; }
        public TimeSpan? TimeToDo { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public TimeSpan? ActualTimeSpent { get; private set; }
        public byte Priority { get; private set; }
        public byte Difficulty { get; private set; }

        public bool IsDone { get; private set; }

        public const byte MinPriority = 1;
        public const byte MaxPriority = 10;
        public const byte DefaultPriority = 5;

        public const byte MinDifficulty = 1;
        public const byte MaxDifficulty = 100;
        public const byte DefaultDifficulty = 20;
        public TaskItem(
            string name,  string category, string description,
            byte priority = DefaultPriority, byte difficulty = DefaultDifficulty)
        {
            if (priority < MinPriority || priority > MaxPriority)
                throw new ArgumentOutOfRangeException(nameof(priority));
            if (difficulty < MinDifficulty || difficulty > MaxDifficulty)
                throw new ArgumentOutOfRangeException(nameof(difficulty));
            Name = name;
            Description = description;
            Category = category;
            Priority = priority;
            Difficulty = difficulty;
            IsDone = false;
        }
        public void Complete(DateTime completedAt, TimeSpan actualTimeSpent)
        {
            MarkDone();
            CompletedAt = completedAt;
            ActualTimeSpent = actualTimeSpent;
        }
        public void MarkDone()
        {
            IsDone = true;
        }
        public void MarkUndone()
        {
            IsDone = false;
            CompletedAt = null;
            ActualTimeSpent = null;
        }
        public void SetDeadLine(DateTime deadLine)
        {
            DeadLine = deadLine;
        }
        public void SetTimeToDo(TimeSpan timeToDo)
        {
            TimeToDo = timeToDo;
        }
    }
}
