using Desktop.Repository;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktop
{
    public partial class Main : Window
    {
        private Border selectedTask = null;
        private string currentFilter = "ALL";

        private List<Border> allTasks = new List<Border>();
        private Dictionary<Border, TaskData> taskData = new Dictionary<Border, TaskData>();

        public Main()
        {
            InitializeComponent();
            if (UserRepository.CurrentUser != null)
            {
                UserNameText.Text = UserRepository.CurrentUser.Username;
            }
        }

        private class TaskData
        {
            public string Title { get; set; }
            public string Time { get; set; }
            public string Date { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public bool IsCompleted { get; set; }
        }

        private void Task_Click(object sender, MouseButtonEventArgs e)
        {
            var clickedTask = (Border)sender;

            if (selectedTask != null)
                selectedTask.Background = Brushes.White;

            selectedTask = clickedTask;
            selectedTask.Background = new SolidColorBrush(Color.FromRgb(230, 230, 255));

            if (taskData.ContainsKey(clickedTask))
            {
                var data = taskData[clickedTask];
                DetailTitle.Text = data.Title;
                DetailTime.Text = data.Time;
                DetailDate.Text = data.Date;
                DetailDescription.Text = string.IsNullOrEmpty(data.Description) ?
                    "Нет описания" : data.Description;

                CompleteButton.Visibility = (data.IsCompleted || currentFilter == "HISTORY")
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }

            TaskDetailsPanel.Visibility = Visibility.Visible;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var taskBorder = (Border)((StackPanel)checkBox.Parent).Parent;

            if (!taskData.ContainsKey(taskBorder)) return;

            var data = taskData[taskBorder];
            data.IsCompleted = checkBox.IsChecked == true;

            var textPanel = (StackPanel)((StackPanel)taskBorder.Child).Children[1];
            var titleText = (TextBlock)textPanel.Children[0];

            if (data.IsCompleted)
            {
                titleText.Foreground = Brushes.Gray;
                titleText.TextDecorations = TextDecorations.Strikethrough;

                if (selectedTask == taskBorder)
                {
                    CompleteButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                titleText.Foreground = Brushes.Black;
                titleText.TextDecorations = null;

                if (selectedTask == taskBorder && currentFilter != "HISTORY")
                {
                    CompleteButton.Visibility = Visibility.Visible;
                }
            }

            UpdateTaskVisibility(taskBorder);

            e.Handled = true;
        }

        private void UpdateTaskVisibility(Border taskBorder)
        {
            if (!taskData.ContainsKey(taskBorder)) return;

            var data = taskData[taskBorder];

            switch (currentFilter)
            {
                case "ALL":
                    taskBorder.Visibility = !data.IsCompleted ? Visibility.Visible : Visibility.Collapsed;
                    break;

                case "HISTORY":
                    taskBorder.Visibility = data.IsCompleted ? Visibility.Visible : Visibility.Collapsed;
                    break;

                default:
                    taskBorder.Visibility = (data.Category == currentFilter && !data.IsCompleted)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;
            }
        }

        private void ApplyFilter()
        {
            foreach (Border taskBorder in allTasks)
            {
                UpdateTaskVisibility(taskBorder);
            }

            if (selectedTask != null && taskData.ContainsKey(selectedTask))
            {
                var data = taskData[selectedTask];
                CompleteButton.Visibility = (data.IsCompleted || currentFilter == "HISTORY")
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask != null)
            {
                var checkBox = (CheckBox)((StackPanel)selectedTask.Child).Children[0];
                checkBox.IsChecked = true;
                CheckBox_Click(checkBox, e);
                TaskDetailsPanel.Visibility = Visibility.Collapsed;
                selectedTask = null;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask != null)
            {
                TasksPanel.Children.Remove(selectedTask);
                allTasks.Remove(selectedTask);
                taskData.Remove(selectedTask);
                TaskDetailsPanel.Visibility = Visibility.Collapsed;
                selectedTask = null;
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            if (addTaskWindow.ShowDialog() == true)
            {
                CreateTaskInUI(
                    addTaskWindow.TaskTitle,
                    addTaskWindow.TaskTime,
                    addTaskWindow.TaskDate,
                    addTaskWindow.TaskDescription,
                    addTaskWindow.TaskCategory);
            }
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            currentFilter = "HISTORY";
            ApplyFilter();
            TaskDetailsPanel.Visibility = Visibility.Collapsed;
            selectedTask = null;
        }

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            currentFilter = "ALL";
            ApplyFilter();
            TaskDetailsPanel.Visibility = Visibility.Collapsed;
            selectedTask = null;
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            currentFilter = button.Content.ToString();
            ApplyFilter();
            TaskDetailsPanel.Visibility = Visibility.Collapsed;
            selectedTask = null;
        }

        public void CreateTaskInUI(string title, string time, string date, string description = "", string category = "Дом")
        {
            Border taskBorder = new Border
            {
                BorderBrush = TaskTemplate.BorderBrush,
                BorderThickness = TaskTemplate.BorderThickness,
                CornerRadius = TaskTemplate.CornerRadius,
                Background = TaskTemplate.Background,
                Margin = TaskTemplate.Margin
            };

            StackPanel innerStack = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(15)
            };

            CheckBox checkBox = new CheckBox
            {
                Style = TemplateCheckBox.Style,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 15, 0)
            };
            checkBox.Click += CheckBox_Click;

            StackPanel textPanel = new StackPanel();

            TextBlock titleText = new TextBlock
            {
                Text = title,
                FontSize = TemplateTitle.FontSize,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 400
            };

            TextBlock timeText = new TextBlock
            {
                Text = time,
                FontSize = TemplateTime.FontSize,
                Foreground = TemplateTime.Foreground
            };

            textPanel.Children.Add(titleText);
            textPanel.Children.Add(timeText);

            innerStack.Children.Add(checkBox);
            innerStack.Children.Add(textPanel);
            taskBorder.Child = innerStack;
            taskBorder.MouseLeftButtonDown += Task_Click;

            TasksPanel.Children.Add(taskBorder);
            allTasks.Add(taskBorder);

            taskData[taskBorder] = new TaskData
            {
                Title = title,
                Time = time,
                Date = date,
                Description = description,
                Category = category,
                IsCompleted = false
            };

            UpdateTaskVisibility(taskBorder);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}