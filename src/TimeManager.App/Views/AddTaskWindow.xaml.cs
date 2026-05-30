using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeManager.Core;

namespace TimeManager.App.Views
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public AddTaskWindow()
        {
            InitializeComponent();
        }
        public event Action<TaskItem>? TaskCreated;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            if(string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Task name is required.");
                return;
            }
            string category = CategoryComboBox.Text.Trim();
            string description = new TextRange(
                DescriptionBox.Document.ContentStart,
                DescriptionBox.Document.ContentEnd
            ).Text.Trim();
            byte priority = TaskItem.DefaultPriority;
            if (!string.IsNullOrWhiteSpace(PriorityBox.Text))
            {
                if(!byte.TryParse(PriorityBox.Text, out priority))
                {
                    MessageBox.Show("Priority must be a number");
                    return;
                }
            }
            byte difficulty = TaskItem.DefaultDifficulty;
            if (!string.IsNullOrWhiteSpace(DiffBox.Text))
            {
                if (!byte.TryParse(DiffBox.Text, out difficulty))
                {
                    MessageBox.Show("Difficulty must be a number");
                    return;
                }
            }
            if (priority < TaskItem.MinPriority || priority > TaskItem.MaxPriority)
            {
                MessageBox.Show($"Priority must be from {TaskItem.MinPriority} to {TaskItem.MaxPriority}.");
                return;
            }
            if (difficulty < TaskItem.MinDifficulty || difficulty > TaskItem.MaxDifficulty)
            {
                MessageBox.Show($"Difficulty must be from {TaskItem.MinDifficulty} to {TaskItem.MaxDifficulty}.");
                return;
            }
            var task = new TaskItem(
                name,
                category,
                description,
                priority,
                difficulty
            );
            if (DeadLinePick.SelectedDate.HasValue)
            {
                task.SetDeadLine(DeadLinePick.SelectedDate.Value);
            }
            if (!string.IsNullOrWhiteSpace(TimeToDoBox.Text))
            {
                if (TimeSpan.TryParseExact(TimeToDoBox.Text, @"hh\:mm",null, out TimeSpan timeToDo))
                {
                    task.SetTimeToDo(timeToDo);
                }
                else
                {
                    MessageBox.Show("Time to do must be in format like [01:30].");
                    return;
                }
            }
            TaskCreated?.Invoke(task);
            ClearFields();
        }
        private void ClearFields()
        {
            NameBox.Clear();
            PriorityBox.Clear();
            DiffBox.Clear();
            DeadLinePick.SelectedDate = null;
            TimeToDoBox.Clear();
            DescriptionBox.Document.Blocks.Clear();

            NameBox.Focus();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = true;
                Close();
            }
            if (e.Key == Key.Enter)
            {
                AddButton_Click(sender, e);
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
