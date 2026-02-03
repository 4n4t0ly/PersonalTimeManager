using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Core
{
    public class TaskItem
    {
        private string _name
        {
            get; set;
        }
        private string _description
        {
            get; set;
        }
        private byte _category
        {
            get; set;
        }
        //Have to add special structer, where 0 is None Category,
        //user will have acces to add new categories
        private DateTime _deadLine
        {
            get; set;
        }
        private TimeSpan _timeToDo
        {
            get; set;
        }
        private byte _priority //1 - 10
        {
            get; set;
        }
        private byte _dificulty //1 - 100
        {
            get; set;
        }

        private bool _isItDone;
        public TaskItem(
            string Name, string Description, byte Category,
            DateTime DeadLine, TimeSpan TimeToDo,
            byte priority = 5, byte dificulty = 20,
            bool IsItDone = false)
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
