using System.IO;

namespace library_manager
{
    public class Book
    {
		public static List<Book> Books = new List<Book>();
		public string Title { get; set; }
		public string Author { get; set; }
		public string Category { get; set; }
		public DateTime DatePublished
		{
			get => DatePublished;
			set { 
				if(value > DateTime.Now) throw new ArgumentException("Date published cannot be in the future.", nameof(value));
				if(value < new DateTime(1800, 1, 1)) throw new ArgumentException("Date published cannot be before 1800.", nameof(value));
				DatePublished = value;
			}
		}

		public Book(string title, string author, string category, DateTime datePublished)
		{
			if(Books.Any(b => b.Title == title && b.Author == author)) throw new ArgumentException("Book already exists.", nameof(title));

			DatePublished = datePublished;
			Title = title;
			Author = author;
			Category = category;
		}

		public override string ToString()
		{
			return $"{Title} - {Author}, {DatePublished.ToShortDateString()} ({Category}) ";
		}

		public static void AddBook(Book book)
		{
			Books.Add(book);
			Book.SaveBooks();
		}

		public static void RemoveBook(Book book, User caller)
		{
			if (!caller.IsAdmin) throw new UnauthorizedAccessException("Only admins can remove books.");

			Books.Remove(book);
			Book.SaveBooks();
		}

		public static List<Book> GetBooksByCategory(string category)
		{
			List<Book> result = new List<Book>();
			foreach (Book book in Books)
			{
				if (book.Category == category) result.Add(book);
			}
			return result;
		}

		public static void LoadBooks()
		{
			Books.Clear();
			if (!File.Exists("books.txt"))
			{
				File.Create("books.txt").Close();
				return;
			};
			string[] lines = File.ReadAllLines("books.txt");
			foreach (string line in lines)
			{
				string[] parts = line.Split(';');
				if(parts.Length != 4) throw new InvalidDataException("Invalid data in books.txt");
				Books.Add(new Book(parts[0], parts[1], parts[2], DateTime.Parse(parts[3])));
			}
		}

		public static void SaveBooks() {
			List<string> lines = new List<string>();
			foreach (Book book in Books)
			{
				lines.Add($"{book.Title};{book.Author};{book.Category};{book.DatePublished}");
			}
			File.WriteAllLines("books.txt", lines);
		}
	}
}
