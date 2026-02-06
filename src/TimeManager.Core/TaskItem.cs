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

        public bool IsDone { get; private set; }
        public TaskItem(
            string name, string description, string category,
            byte priority = 5, byte dificulty = 20)
        {
            Name = name;
            Description = description;
            Category = category;
            Priority = priority;
        }
        public void MarkDone()
        {
            IsDone = true;
        }
        public void SetPriority(byte priority)
        {

        }
    }
}
