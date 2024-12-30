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
	/// Interaction logic for BookList.xaml
	/// </summary>
	public partial class BookList : Page
	{
		User LoggedInUser;
		public BookList(User loggedInUser)
		{
			InitializeComponent();
			LoggedInUser = loggedInUser;

			Render();
		}

		private void Render(string? category = null)
		{
			if(Book.Books.Count == 0)
			{
				NoBooks.Visibility = Visibility.Visible;
				BookListBox.Visibility = Visibility.Hidden;
				CategoryTextBlock.Visibility = Visibility.Hidden;
				CategoryComboBox.Visibility = Visibility.Hidden;
				return;
			}

			//ListBox
			BookListBox.Items.Clear();
			foreach (Book book in Book.Books)
			{
				if (category != null && book.Category != category) continue;
				ListBoxItem item = new ListBoxItem();
				item.Content = book;
				item.HorizontalContentAlignment = HorizontalAlignment.Center;
				BookListBox.Items.Add(item);
			}
			BookListBox.SelectedItem = null;

			//ComboBox
			CategoryComboBox.ItemsSource = Book.Books.Select(b => b.Category).Distinct().ToList();
			if (category != null)
			{
				CategoryComboBox.SelectedItem = category;
				DeleteCategoryButton.IsEnabled = true;
			}
			else
			{
				CategoryComboBox.SelectedItem = null;
				DeleteCategoryButton.IsEnabled = false;
			}
		}

		private void BookSelected(object sender, SelectionChangedEventArgs e)
		{
			if (!LoggedInUser.IsAdmin || BookListBox.SelectedItem == null) {
				BookListBox.SelectedItem = null;
				return;
			};

			ListBoxItem item = (ListBoxItem)BookListBox.SelectedItem;
			Book book = (Book)item.Content;
			BookListBox.SelectedItem = null;

			MessageBoxResult result = MessageBox.Show("Szerkeszteni vagy törölni szeretnéd a könyvet?\nIgen-szerkesztés, Nem-Törlés", "Könyv szerkesztése/törlése", MessageBoxButton.YesNoCancel);

			if (result == MessageBoxResult.No)
			{
				try
				{
					Book.RemoveBook(book, LoggedInUser);
					Render();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}

			if(result == MessageBoxResult.Yes)
			{
				NavigationService.Navigate(new BookForm(Book.Books.IndexOf(book)));
			}
		}

		private void CategorySelected(object sender, SelectionChangedEventArgs e)
		{
			string category = (string)CategoryComboBox.SelectedItem;
			if(category == null) return;

			Render(category);
		}

		private void DeleteCategory(object sender, RoutedEventArgs e)
		{
			CategoryComboBox.SelectedItem = null;
			Render();
		}
	}
}
