using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Core
{
    public class TaskItem
    {
        private string _name;
        private string? _description;
        private TaskCategory? _category;
        //Have to add special structer, where 0 is None Category,
        //user will have acces to add new categories
        private DateTime? _deadLine;
        private TimeSpan _timeToDo;
        private byte _priority; //1 - 10
        private byte _dificulty; //1 - 100

        private bool _isItDone;
        public TaskItem(
            string Name, string Description,  Category,
            DateTime DeadLine, TimeSpan TimeToDo,
            byte priority = 5, byte dificulty = 20)
        {
            _name = Name;
            _description = Description;
            _category = Category;
            _priority = priority;
            _isItDone = IsItDone;
            _deadLine = DeadLine;
            _timeToDo = TimeToDo;
        }
        public void MarkDone()
        {
            _isItDone = true;
        }
    }
}
