using Desktop.Repository;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktop
{
	public partial class Main : Window
	{
		private Border selectedTask = null;

		public Main()
		{
			InitializeComponent();
			if (UserRepository.CurrentUser != null)
			{
				UserNameText.Text = UserRepository.CurrentUser.Username;
			}
		}

		//клик по задаче
		private void Task_Click(object sender, MouseButtonEventArgs e)
		{
			var clickedTask = (Border)sender;

			if (selectedTask != null) selectedTask.Background = Brushes.White;
			selectedTask = clickedTask;
			selectedTask.Background = new SolidColorBrush(Color.FromRgb(230, 230, 255));

			var innerStack = (StackPanel)clickedTask.Child;
			var textPanel = (StackPanel)innerStack.Children[1];
			var titleText = (TextBlock)textPanel.Children[0];
			var timeText = (TextBlock)textPanel.Children[1];

			DetailTitle.Content = titleText.Text;
			DetailTime.Content = timeText.Text;

			var tagData = clickedTask.Tag as Tuple<string, string>;
			if (tagData != null)
			{
				DetailDate.Content = tagData.Item1; // Настоящая дата из окна
				DetailDescription.Content = string.IsNullOrEmpty(tagData.Item2) ? "Нет описания" : tagData.Item2;
			}

			TaskDetailsPanel.Visibility = Visibility.Visible;
		}

		//клик по CheckBox
		private void CheckBox_Click(object sender, RoutedEventArgs e)
		{
			var checkBox = (CheckBox)sender;
			var taskBorder = (Border)((StackPanel)checkBox.Parent).Parent;

			if (checkBox.IsChecked == true)
			{
				var textPanel = (StackPanel)((StackPanel)taskBorder.Child).Children[1];
				((TextBlock)textPanel.Children[0]).Foreground = Brushes.Gray;
				((TextBlock)textPanel.Children[0]).TextDecorations = TextDecorations.Strikethrough;
				taskBorder.Visibility = Visibility.Collapsed;
			}
			else
			{
				var textPanel = (StackPanel)((StackPanel)taskBorder.Child).Children[1];
				((TextBlock)textPanel.Children[0]).Foreground = Brushes.Black;
				((TextBlock)textPanel.Children[0]).TextDecorations = null;
				taskBorder.Visibility = Visibility.Visible;
			}
		}

		//кнопка Готово
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

		//кнопка Удалить
		private void Button_Click1(object sender, RoutedEventArgs e)
		{
			if (selectedTask != null)
			{
				TasksPanel.Children.Remove(selectedTask);
				TaskDetailsPanel.Visibility = Visibility.Collapsed;
				selectedTask = null;
			}
		}

		//кнопка "+"
		private void AddTaskButton_Click(object sender, RoutedEventArgs e)
		{
			AddTaskWindow addTaskWindow = new AddTaskWindow();
			if (addTaskWindow.ShowDialog() == true)
			{
				CreateTaskInUI(addTaskWindow.TaskTitle, addTaskWindow.TaskTime,
					addTaskWindow.TaskDate, addTaskWindow.TaskDescription);
			}
		}

		//нажать на кнопку "история"
		private void HistoryButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (Border taskBorder in TasksPanel.Children)
			{
				var checkBox = (CheckBox)((StackPanel)taskBorder.Child).Children[0];
				taskBorder.Visibility = checkBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		//нажать на кнопку "задачи"
		private void TasksButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (Border taskBorder in TasksPanel.Children)
			{
				var checkBox = (CheckBox)((StackPanel)taskBorder.Child).Children[0];
				taskBorder.Visibility = checkBox.IsChecked == false ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		//создание новой задачи
		public void CreateTaskInUI(string title, string time, string date, string description = "")
		{
			Border taskBorder = new Border
			{
				BorderBrush = Brushes.Black,
				BorderThickness = new Thickness(1),
				CornerRadius = new CornerRadius(10),
				Background = Brushes.White,
				Margin = new Thickness(0, 0, 0, 10),
				Tag = Tuple.Create(date, description)
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

			//текст задачи
			StackPanel textPanel = new StackPanel();
			textPanel.Children.Add(new TextBlock { Text = title, FontSize = 16 });
			textPanel.Children.Add(new TextBlock
			{
				Text = time,
				FontSize = 14,
				Foreground = Brushes.Gray
			});

			innerStack.Children.Add(checkBox);
			innerStack.Children.Add(textPanel);
			taskBorder.Child = innerStack;
			taskBorder.MouseLeftButtonDown += Task_Click;

			TasksPanel.Children.Add(taskBorder);
		}
	}
}