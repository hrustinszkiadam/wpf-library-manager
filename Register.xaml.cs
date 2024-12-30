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
	/// Interaction logic for Register.xaml
	/// </summary>
	public partial class Register : Page
	{
		public Register()
		{
			InitializeComponent();
		}

		private void RegisterUser(object sender, RoutedEventArgs e)
		{
			string username = Username.Text;
			string email = Email.Text;
			string password = Password.Password;

			if (string.IsNullOrEmpty(username) ||
				string.IsNullOrEmpty(email) ||
				string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			try
			{
				User.AddUser(email, username, password);
				MessageBox.Show("Sikeres regisztráció!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			LoginPage(sender, e);
		}

		private void LoginPage(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Login());
		}
	}
}
