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
using Todo.Entities;

namespace Desktop
{
	/// <summary>
	/// Логика взаимодействия для Registration.xaml
	/// </summary>
	public partial class Registration : Window
	{
		private UserRepository userRepository = new UserRepository();
		public Registration()
		{
			InitializeComponent();
		}

		//кнопка "назад"
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			this.Close();
		}

		public class Validator
		{
			public static string CheckFields(string username, string email, string password, string confirmPassword)
			{
				if (username == "Введите имя пользователя" || email == "exam@yandex.ru" ||
					password == "Введите пароль" || confirmPassword == "Повторите пароль")
					return "Не все поля ввода заполнены!";

				if (username.Length < 3)
					return "Ваше имя должно быть не менее 3 символов!";

				if (!email.Contains("@") || !email.Contains("."))
					return "Введена неверная почта!";

				if (email.IndexOf("@") == 0 || email.IndexOf(".") == email.Length - 1)
					return "Введена неверная почта!";

				if (password.Length < 6)
					return "Пароль должен быть не менее 6 символов!";

				if (password != confirmPassword)
					return "Пароли не совпадают! Попробуйте снова.";

				return "Успех";
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string result = Validator.CheckFields(TextBoxUsername.Text, TextBoxEmail.Text, TextBoxPassword.Text, TextBoxConfirmPassword.Text);

			if (result == "Успех")
			{
				var newUser = new UserModel();
				newUser.Username = TextBoxUsername.Text;
				newUser.Email = TextBoxEmail.Text;
				newUser.Password = TextBoxPassword.Text;

				if (userRepository.Register(newUser))
				{
					MessageBox.Show("Регистрация успешна!");
					Main_empty main_Empty = new Main_empty();
					main_Empty.Show();
					this.Close();
				}
				else
				{
					MessageBox.Show("Ошибка: пользователь с таким именем уже существует!");
				}
			}
			else
			{
				MessageBox.Show(result);
			}
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
