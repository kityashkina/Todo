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
        private string currentCategory = null; // null = показывать все задачи

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
            }

            TaskDetailsPanel.Visibility = Visibility.Visible;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var taskBorder = (Border)((StackPanel)checkBox.Parent).Parent;

            if (checkBox.IsChecked == true)
            {
                if (taskData.ContainsKey(taskBorder))
                {
                    taskData[taskBorder].IsCompleted = true;
                }

                var textPanel = (StackPanel)((StackPanel)taskBorder.Child).Children[1];
                ((TextBlock)textPanel.Children[0]).Foreground = Brushes.Gray;
                ((TextBlock)textPanel.Children[0]).TextDecorations = TextDecorations.Strikethrough;

                taskBorder.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (taskData.ContainsKey(taskBorder))
                {
                    taskData[taskBorder].IsCompleted = false;
                }

                var textPanel = (StackPanel)((StackPanel)taskBorder.Child).Children[1];
                ((TextBlock)textPanel.Children[0]).Foreground = Brushes.Black;
                ((TextBlock)textPanel.Children[0]).TextDecorations = null;

                // ВОТ ТУТ ИСПРАВЛЕНИЕ:
                if (currentCategory == null || taskData[taskBorder].Category == currentCategory)
                {
                    taskBorder.Visibility = Visibility.Visible;
                }
            }

            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click1(object sender, RoutedEventArgs e)
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
                CreateTaskInUI(addTaskWindow.TaskTitle, addTaskWindow.TaskTime,
                    addTaskWindow.TaskDate, addTaskWindow.TaskDescription,
                    addTaskWindow.TaskCategory);
            }
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Border taskBorder in allTasks)
            {
                var checkBox = (CheckBox)((StackPanel)taskBorder.Child).Children[0];
                taskBorder.Visibility = checkBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            currentCategory = null;
            ShowAllTasks();
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            currentCategory = button.Content.ToString();
            FilterTasksByCategory();
        }

        private void ShowAllTasks()
        {
            foreach (Border taskBorder in allTasks)
            {
                if (taskData.ContainsKey(taskBorder))
                {
                    var data = taskData[taskBorder];
                    taskBorder.Visibility = !data.IsCompleted ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void FilterTasksByCategory()
        {
            if (string.IsNullOrEmpty(currentCategory))
            {
                ShowAllTasks();
                return;
            }

            foreach (Border taskBorder in allTasks)
            {
                if (taskData.ContainsKey(taskBorder))
                {
                    var data = taskData[taskBorder];

                    if (data.Category == currentCategory && !data.IsCompleted)
                    {
                        taskBorder.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        taskBorder.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        public void CreateTaskInUI(string title, string time, string date, string description = "", string category = "Дом")
        {
            Border taskBorder = new Border
            {
                BorderBrush = TaskTemplate.BorderBrush,
                BorderThickness = TaskTemplate.BorderThickness,
                CornerRadius = TaskTemplate.CornerRadius,
                Background = TaskTemplate.Background,
                Margin = TaskTemplate.Margin,
                Tag = Tuple.Create(date, description, category)
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

            // ВОТ ТУТ ВАЖНОЕ ИСПРАВЛЕНИЕ:
            if (currentCategory == null || currentCategory == category)
            {
                taskBorder.Visibility = Visibility.Visible;
            }
            else
            {
                taskBorder.Visibility = Visibility.Collapsed;
            }
        }
    }
}