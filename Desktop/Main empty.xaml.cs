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
    /// Логика взаимодействия для Main_empty.xaml
    /// </summary>
    public partial class Main_empty : Window
    {
        public Main_empty()
        {
            InitializeComponent();
        }


		 private void BackToRegistration_Click(object sender, RoutedEventArgs e)
		{
			Registration registration = new Registration();
			registration.Show();
			this.Close();
		}

		private void CreateFirstTaskButton_Click(object sender, RoutedEventArgs e)
		{
			AddTaskWindow addTaskWindow = new AddTaskWindow();
			if (addTaskWindow.ShowDialog() == true)
			{
				Main mainWindow = new Main();
				mainWindow.CreateTaskInUI(addTaskWindow.TaskTitle, addTaskWindow.TaskTime,
					addTaskWindow.TaskDate, addTaskWindow.TaskDescription);
				mainWindow.Show();
				this.Close();
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Main mainWindow = new Main();
			mainWindow.Show();
			this.Close();
		}

	}
}
