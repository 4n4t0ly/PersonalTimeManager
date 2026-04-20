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
        //Have to add special structer, where 0 is None Category,
        //user will have acces to add new categories
        public DateTime? DeadLine { get; private set; }
        public TimeSpan? TimeToDo { get; private set; }

        public byte Priority { get; private set; } //1 - 10
        public byte Dificulty { get; private set; } //1 - 100

        public bool IsDone { get; private set; }
        public TaskItem(
            string name,  string category, string description,
            byte priority = 5, byte dificulty = 20)
        {
            if (priority < 1 || priority > 10)
                throw new ArgumentOutOfRangeException(nameof(priority));
            if (dificulty < 1 || dificulty > 100)
                throw new ArgumentOutOfRangeException(nameof(dificulty));
            Name = name;
            Description = description;
            Category = category;
            Priority = priority;
            Dificulty = dificulty;
        }
        public void MarkDone()
        {
            IsDone = true;
        }
        public void MarkUndone()
        {
            IsDone = false;
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
