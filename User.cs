using System.IO;
using System.Text.RegularExpressions;

namespace library_manager
{
    public class User
    {
		public static List<User> Users = new List<User>();
		public string Email { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; init; }

		public User(string email, string username,string password)
		{
			if (Users.Any(u => u.Username == username || u.Email == email)) throw new ArgumentException("A felhasználónév vagy email cím már foglalt.");

			Email = email;
			Username = username;
			Password = password;
			IsAdmin = Users.Count == 0;
		}

		public override string ToString()
		{
			return $"{Username} ({Email})";
		}

	    public static bool ValidateUsername(string username)
		{
			return !Users.Any(u => u.Username == username);
		}
	    
		public static bool ValidateEmail(string email)
		{
			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			return regex.IsMatch(email);
		}

		public static bool CheckDuplicateEmail(string email)
		{
			return !Users.Any(u => u.Email == email);
		}

		public static bool ValidatePassword(string password)
		{
			Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
			return regex.IsMatch(password);
		}

		public static void AddUser(string email, string username, string password)
		{
			if(!ValidateEmail(email)) throw new ArgumentException("Hibás email cím!");
			if (!ValidatePassword(password)) throw new ArgumentException("Nem elég erős a jelszó\n(Min 8 karakter, Min 1 kisbetű, Min 1 nagybetű, Min 1 szám)!");
			if (!ValidateUsername(username)) throw new ArgumentException("A felhasználónév már foglalt!");
			if (!CheckDuplicateEmail(email)) throw new ArgumentException("Az email cím már foglalt!");

			Users.Add(new User(email, username, password));
			User.SaveUsers();
		}

		public static void RemoveUser(User user, User caller)
		{
			if(user.Username == caller.Username) throw new UnauthorizedAccessException("Nem törölheted magadat!");

			Users.Remove(user);
			User.SaveUsers();
		}

		public static void LoadUsers()
		{
			Users.Clear();
			if (!File.Exists("users.txt"))
			{
				File.Create("users.txt").Close();
				return;
			};
			string[] lines = File.ReadAllLines("users.txt");
			foreach (string line in lines)
			{
				string[] parts = line.Split(';');
				User user = new User(parts[0], parts[1], parts[2]);
				Users.Add(user);
			}
		}

		public static void SaveUsers()
		{
			List<string> lines = new List<string>();
			foreach (User user in Users)
			{
				lines.Add($"{user.Email};{user.Username};{user.Password}");
			}
			File.WriteAllLines("users.txt", lines);
		}

	}
}
