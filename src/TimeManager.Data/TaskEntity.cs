using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Data
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
        public DateTime? DeadLine { get; set; }
        public int? TimeToDoSeconds { get; set; }
        public byte Priority { get; set; }
        public byte Difficulty { get; set; }
        public bool IsDone { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? ActualTimeSpentSeconds { get; set; }
    }
}
