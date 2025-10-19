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
	/// Логика взаимодействия для Registration.xaml
	/// </summary>
	public partial class Registration : Window
	{
		public Registration()
		{
			InitializeComponent();
		}

		//кнопка "назад"
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();

			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "Введите имя пользователя")
			{
				textBox.Text = "";
				textBox.Foreground = Brushes.Black;
			}
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "")
			{
				textBox.Text = "Введите имя пользователя";
				textBox.Foreground = Brushes.Gray;
			}
		}


		private void TextBox_GotFocus2(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "exam@yandex.ru")
			{
				textBox.Text = "";
				textBox.Foreground = Brushes.Black;
			}
		}

		private void TextBox_LostFocus2(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "")
			{
				textBox.Text = "exam@yandex.ru";
				textBox.Foreground = Brushes.Gray;
			}
		}


		private void TextBox_GotFocus3(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "Введите пароль")
			{
				textBox.Text = "";
				textBox.Foreground = Brushes.Black;
			}
		}

		private void TextBox_LostFocus3(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "")
			{
				textBox.Text = "Введите пароль";
				textBox.Foreground = Brushes.Gray;
			}
		}

		private void TextBox_GotFocus4(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "Повторите пароль")
			{
				textBox.Text = "";
				textBox.Foreground = Brushes.Black;
			}
		}

		private void TextBox_LostFocus4(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "")
			{
				textBox.Text = "Повторите пароль";
				textBox.Foreground = Brushes.Gray;
			}
		}
	}
}
