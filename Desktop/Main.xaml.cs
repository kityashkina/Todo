using Desktop.Repository;
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

namespace Desktop
{
	/// <summary>
	/// Логика взаимодействия для Main.xaml
	/// </summary>
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

		// Клик по задаче
		private void Task_Click(object sender, MouseButtonEventArgs e)
		{
			if (selectedTask == sender)
			{
				selectedTask.Background = Brushes.White;
				selectedTask = null;
				TaskDetailsPanel.Visibility = Visibility.Collapsed;
				return;
			}

			if (selectedTask != null)
			{
				selectedTask.Background = Brushes.White;
			}

			selectedTask = (Border)sender;
			selectedTask.Background = new SolidColorBrush(Color.FromRgb(230, 230, 255));
			TaskDetailsPanel.Visibility = Visibility.Visible;
		}

		// Клик по CheckBox
		private void CheckBox_Click(object sender, RoutedEventArgs e)
		{
			var checkBox = (CheckBox)sender;
			var parentStackPanel = (StackPanel)checkBox.Parent;
			var taskBorder = (Border)parentStackPanel.Parent;

			if (checkBox.IsChecked == true)
			{
				MarkTaskAsCompleted(taskBorder);
			}
			else
			{
				MarkTaskAsIncomplete(taskBorder);
			}
		}

		// Отметить задачу выполненной
		private void MarkTaskAsCompleted(Border taskBorder)
		{
			var stackPanel = (StackPanel)taskBorder.Child;
			var textStackPanel = (StackPanel)stackPanel.Children[1];
			var titleText = (TextBlock)textStackPanel.Children[0];
			var timeText = (TextBlock)textStackPanel.Children[1];

			taskBorder.Background = new SolidColorBrush(Color.FromRgb(200, 255, 200));

			titleText.Foreground = Brushes.Gray;
			titleText.TextDecorations = TextDecorations.Strikethrough;
			timeText.Foreground = Brushes.Gray;
		}

		// Вернуть задачу в невыполненную
		private void MarkTaskAsIncomplete(Border taskBorder)
		{

			var stackPanel = (StackPanel)taskBorder.Child;
			var textStackPanel = (StackPanel)stackPanel.Children[1];
			var titleText = (TextBlock)textStackPanel.Children[0];
			var timeText = (TextBlock)textStackPanel.Children[1];

			taskBorder.Background = Brushes.White;

			titleText.Foreground = Brushes.Black;
			titleText.TextDecorations = null;
			timeText.Foreground = Brushes.Gray;
		}

		// Кнопка Готово
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (selectedTask != null)
			{
				var stackPanel = (StackPanel)selectedTask.Child;
				var checkBox = (CheckBox)stackPanel.Children[0];

				checkBox.IsChecked = true;
				MarkTaskAsCompleted(selectedTask);
			}
		}

		// Кнопка Удалить
		private void Button_Click1(object sender, RoutedEventArgs e)
		{
			if (selectedTask != null)
			{
				TasksPanel.Children.Remove(selectedTask);
				TaskDetailsPanel.Visibility = Visibility.Collapsed;
				selectedTask = null;
			}
		}
	}
}
