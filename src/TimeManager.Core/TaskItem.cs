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
        public string? Description { get; private set; }
        public string? Category { get; private set; }
        //Have to add special structer, where 0 is None Category,
        //user will have acces to add new categories
        public DateTime? DeadLine { get; private set; }
        public TimeSpan? TimeToDo { get; private set; }

        public byte Priority { get; private set; } //1 - 10
        public byte Dificulty { get; private set; } //1 - 100

        public bool _isDone { get; private set; }
        public TaskItem(
            string name, string Description, string Category,
            DateTime DeadLine, TimeSpan TimeToDo,
            byte priority = 5, byte dificulty = 20)
        {
            Name = name;
            Description = Description;
            Category = Category;
            Priority = priority;
        }
        public void MarkDone()
        {
            _isDone = true;
        }
    }
}
