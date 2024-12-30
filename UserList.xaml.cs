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
	/// Interaction logic for UserList.xaml
	/// </summary>
	public partial class UserList : Page
	{
		User LoggedInUser;
		public UserList(User loggedInUser)
		{
			InitializeComponent();
			LoggedInUser = loggedInUser;

			Render();
		}

		private void Render()
		{
			UserListBox.Items.Clear();
			foreach (User user in User.Users)
			{
				ListBoxItem item = new ListBoxItem();
				item.Content = user;
				item.HorizontalContentAlignment = HorizontalAlignment.Center;
				UserListBox.Items.Add(item);
			}
			UserListBox.SelectedItem = null;
		}

		private void UserSelected(object sender, SelectionChangedEventArgs e)
		{
			if (UserListBox.SelectedItem == null) return;

			ListBoxItem item = (ListBoxItem)UserListBox.SelectedItem;
			UserListBox.SelectedItem = null;

			MessageBoxResult result = MessageBox.Show("Biztosan törölni akarod ezt a felhasználót?", "Felhasználó Törlése", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if(!(result == MessageBoxResult.Yes)) return;

			User user = (User)item.Content;
			if(user == null) return;

			try
			{
				User.RemoveUser(user, LoggedInUser);
				UserListBox.Items.Remove(item);
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
