using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.Core;
namespace TimeManager.Tests
{
    [TestClass]
    public class TaskItemTests
    {
        [TestMethod]
        public void Constructor_WithValidValues_CreatesTask()
        {
            TaskItem task = new TaskItem(
                "Study",
                "Education",
                "Finish coursework",
                priority: 8,
                difficulty: 40);
            Assert.AreEqual("Study", task.Name);
            Assert.AreEqual("Education", task.Category);
            Assert.AreEqual("Finish coursework", task.Description);
            Assert.AreEqual(8, task.Priority);
            Assert.AreEqual(40, task.Difficulty);
            Assert.IsFalse(task.IsDone);
        }
        [TestMethod]
        public void Constructor_WithPriorityOutOfRange_ThrowsExeption()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
            {
                new TaskItem(
                    "Study",
                    "Education",
                    "Text",
                    priority: 11,
                    difficulty: 40);
            });
        }
        [TestMethod]
        public void MarkDone_SetsIsDoneToTrue()
        {
            TaskItem task = new TaskItem("Study", "Education", "Text");
            task.MarkDone();
            Assert.IsTrue(task.IsDone);
        }
    }
}