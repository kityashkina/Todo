using System;
using System.Windows;
using System.Windows.Controls;

namespace Desktop
{
	public partial class AddTaskWindow : Window
	{
		public string TaskTitle { get; private set; }
		public string TaskTime { get; private set; }
		public string TaskDate { get; private set; }
		public string TaskDescription { get; private set; }
        public string TaskCategory { get; private set; }

        public AddTaskWindow()
		{
			InitializeComponent();
			DatePicker.SelectedDate = DateTime.Now;
			TimePicker.SelectedIndex = 0;
			CategoryComboBox.SelectedIndex = 0;
		}

		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
			{
				MessageBox.Show("Введите название задачи!");
				return;
			}

			TaskTitle = TitleTextBox.Text;
			TaskTime = (TimePicker.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "9:00am";
			TaskDate = DatePicker.SelectedDate?.ToString("dd MMMM yyyy") ?? DateTime.Now.ToString("dd MMMM yyyy");
			TaskDescription = DescriptionTextBox.Text;
            TaskCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Дом";

            this.DialogResult = true;
			this.Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}