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
	/// Interaction logic for BookForm.xaml
	/// </summary>
	public partial class BookForm : Page
	{
		int BookIndex;
		public BookForm(int bookIndex = -1)
		{
			InitializeComponent();
			CategoryComboBox.ItemsSource = Book.Categories;
			BookIndex = bookIndex;
			if (bookIndex == -1) return;

			Book book = Book.Books[bookIndex];
			TitleTextBox.Text = book.Title;
			AuthorTextBox.Text = book.Author;
			CategoryComboBox.SelectedItem = book.Category;
			DatePublishedPicker.SelectedDate = book.DatePublished;
		}

		private void SaveBook(object sender, RoutedEventArgs e)
		{
			if(string.IsNullOrEmpty(TitleTextBox.Text) ||
				string.IsNullOrEmpty(AuthorTextBox.Text) ||
				CategoryComboBox.SelectedItem == null ||
				DatePublishedPicker.SelectedDate == null)
			{
				MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (DatePublishedPicker.SelectedDate > DateTime.Now || DatePublishedPicker.SelectedDate < new DateTime(1800, 1, 1))
			{
				MessageBox.Show("A megjelenés dátuma nem lehet a jövőben vagy 1800 előtt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			try
			{
				if (BookIndex == -1)
				{
					Book.AddBook(new Book(TitleTextBox.Text, AuthorTextBox.Text, (string)CategoryComboBox.SelectedItem, (DateTime)DatePublishedPicker.SelectedDate));
				}
				else
				{
					Book.Books[BookIndex].Title = TitleTextBox.Text;
					Book.Books[BookIndex].Author = AuthorTextBox.Text;
					Book.Books[BookIndex].Category = (string)CategoryComboBox.SelectedItem;
					Book.Books[BookIndex].DatePublished = (DateTime)DatePublishedPicker.SelectedDate;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			TitleTextBox.Text = "";
			AuthorTextBox.Text = "";
			CategoryComboBox.SelectedItem = null;
			DatePublishedPicker.SelectedDate = null;
		}
	}
}
