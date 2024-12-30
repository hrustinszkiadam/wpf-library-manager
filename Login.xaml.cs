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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace library_manager
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Page
	{
		public Login()
		{
			InitializeComponent();
		}

		private void LoginUser(object sender, RoutedEventArgs e)
		{
			string username = Username.Text;
			string password = Password.Password;

			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			User? matchingUser = User.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

			if (matchingUser == null)
			{
				MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			NavigationService.Navigate(new Dashboard(matchingUser));
		}

		private void RegisterPage(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Register());
		}
	}
}
