using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Core
{
    public class TaskCategory
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }

        public TaskCategory(string name)
        {
            Name = name;
        }
    }
}
