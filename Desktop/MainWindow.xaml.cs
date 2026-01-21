using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		//кнопка "зарегистрироваться"
		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			Registration registrationWindow = new Registration();
			registrationWindow.Show();

			this.Close();
		}

		//при нажатии на текстбокс текст пропадает
		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "Введите почту")
			{
				textBox.Text = "";
				textBox.Foreground = Brushes.Black;
			}
		}

		private void TextBox_GotFocus2(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "Введите пароль")
			{
				textBox.Text = "";
				textBox.Foreground = Brushes.Black;
			}
		}

		//если убираем курсор с текстбокса и оставляем пустым, то текст возвращается
		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "")
			{
				textBox.Text = "Введите почту";
				textBox.Foreground = Brushes.Gray;
			}
		}

		private void TextBox_LostFocus2(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "")
			{
				textBox.Text = "Введите пароль";
				textBox.Foreground = Brushes.Gray;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("Вы не зарегистрированы!");
        }
	}
}