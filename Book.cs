using System.IO;

namespace library_manager
{
    public class Book
    {
		public static List<Book> Books = new List<Book>();
		public static List<string> Categories = new List<string>() {
			"Fantasy", "Sci-fi", "Horror", "Romance", "Thriller", "Mystery", "Biography", "History", "Science", "Cookbook", "Poetry", "Art", "Self-help", "Travel", "Children", "Young Adult", "Classic", "Drama", "Comedy", "Crime", "Adventure", "Health", "Fitness", "Business", "Economics", "Philosophy", "Religion", "Spirituality", "Politics", "Social Sciences", "Education", "Parenting", "Family", "Relationships", "Humor", "Entertainment", "Music", "Sports", "Hobbies", "Crafts", "Home", "Garden", "DIY", "Fashion", "Beauty", "Photography", "Design", "Architecture", "Graphic Novels", "Manga", "Comics", "Textbooks", "Reference", "Test Prep", "Study Guides", "Science Fiction & Fantasy", "Self-Help", "Cookbooks", "History", "Biographies", "Romance", "Mystery", "Thriller", "Horror", "Children's", "Young Adult", "Science", "Art", "Travel", "Health", "Fitness", "Business", "Economics", "Philosophy", "Religion", "Spirituality", "Politics", "Social Sciences", "Education", "Parenting", "Family", "Relationships", "Humor", "Entertainment", "Music", "Sports", "Hobbies", "Crafts", "Home", "Garden", "DIY", "Fashion", "Beauty", "Photography", "Design", "Architecture", "Graphic Novels", "Manga", "Comics", "Textbooks", "Reference", "Test Prep", "Study Guides"
		};
		public string Title { get; set; }
		public string Author { get; set; }
		public string Category { get; set; }

		private DateTime datePublished;
		public DateTime DatePublished
		{
			get => datePublished;
			set { 
				if(value > DateTime.Now) throw new ArgumentException("A létrehozás dátuma nem lehet a jövőben!");
				if(value < new DateTime(1800, 1, 1)) throw new ArgumentException("A létrehozás dátuma nem lehet 1800 előtt!");
				datePublished = value;
			}
		}

		public Book(string title, string author, string category, DateTime datePublished)
		{
			if(Books.Any(b => b.Title == title && b.Author == author)) throw new ArgumentException("A könvy már létezik!");

			DatePublished = datePublished;
			Title = title;
			Author = author;
			Category = category;
		}

		public override string ToString()
		{
			return $"{Title} - {Author} ({Category}, {DatePublished.ToShortDateString()})";
		}

		public static void AddBook(Book book)
		{
			if (Books.Any(b => b.Title == book.Title && b.Author == book.Author)) throw new ArgumentException("A könvy már létezik!");

			Books.Add(book);
			Book.SaveBooks();
		}

		public static void RemoveBook(Book book, User caller)
		{
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
				if(parts.Length != 4) continue;
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
