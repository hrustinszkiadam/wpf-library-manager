﻿using System;
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
	/// Interaction logic for Dashboard.xaml
	/// </summary>
	public partial class Dashboard : Page
	{
		public User LoggedInUser;
		public Dashboard(User loggedInUser)
		{
			InitializeComponent();
			LoggedInUser = loggedInUser;
			if(loggedInUser.IsAdmin) AdminControls.Visibility = Visibility.Visible;
		}

		private void AddBook(object sender, RoutedEventArgs e)
		{
			DashboardFrame.Navigate(new BookForm());
		}

		private void BookListPage(object sender, RoutedEventArgs e)
		{
			DashboardFrame.Navigate(new BookList(LoggedInUser));
		}

		private void UserListPage(object sender, RoutedEventArgs e)
		{
			DashboardFrame.Navigate(new UserList(LoggedInUser));
		}

		private void Logout(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Login());
		}
	}
}
