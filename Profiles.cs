using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MusicApp
{
    class Profiles
    {
    }

    public abstract class Profile
    {
        protected string name { get; set; }
        protected string email { get; set; }
        protected string password { get; set; }
        protected string phone { get; set; }
        protected int age { get; set; }
        protected string country { get; set; }
        public abstract void EditProfile();
        public abstract void DeleteProfile();
        public abstract void ViewProfile();
    }

    public class UserProfile : Profile
    {
        private string[] favoriteSongs { get; set; }
        private string[] favoriteArtists { get; set; }
        private string[] playlists { get; set; }

        private readonly string connectionString;

        public UserProfile()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public UserProfile(string name, string email, string password, string phone, int age, string country)
            : this()
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.phone = phone;
            this.age = age;
            this.country = country;
        }

        public void SaveToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO UserProfiles (Name, Email, Password, Phone, Age, Country) VALUES (@Name, @Email, @Password, @Phone, @Age, @Country)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Country", country);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public override void EditProfile()
        {
            Console.WriteLine("What do you want to edit in your profile");
            Console.WriteLine("1-Name\n2-E-mail\n3-Password\n4-Phone\n5-Age\n6-Country");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter new name:");
                    name = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter new email:");
                    email = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Enter new password:");
                    password = Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("Enter new phone:");
                    phone = Console.ReadLine();
                    break;
                case 5:
                    Console.WriteLine("Enter new age:");
                    age = Convert.ToInt32(Console.ReadLine());
                    break;
                case 6:
                    Console.WriteLine("Enter new country:");
                    country = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            SaveToDatabase();
        }

        public override void DeleteProfile()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM UserProfiles WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            name = email = password = phone = country = string.Empty;
            age = 0;
            Console.WriteLine("Profile has been deleted.");
        }

        public override void ViewProfile()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM UserProfiles WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("----- User Profile -----");
                    Console.WriteLine($"Name:     {reader["Name"]}");
                    Console.WriteLine($"Email:    {reader["Email"]}");
                    Console.WriteLine($"Phone:    {reader["Phone"]}");
                    Console.WriteLine($"Age:      {reader["Age"]}");
                    Console.WriteLine($"Country:  {reader["Country"]}");
                    Console.WriteLine("------------------------");
                }
                else
                {
                    Console.WriteLine("Profile not found.");
                }
                connection.Close();
            }
        }
    }

    public class ArtistProfile : Profile
    {
        private int followers { get; set; }

        public ArtistProfile() { }

        public ArtistProfile(string name, string email, string password, string phone, int age, string country)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.phone = phone;
            this.age = age;
            this.country = country;
        }

        public override void EditProfile()
        {
            Console.WriteLine("What do you want to edit in your profile");
            Console.WriteLine("1-Name\n2-E-mail\n3-Password\n4-Phone\n5-Age\n6-Country");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter new name:");
                    name = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter new email:");
                    email = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Enter new password:");
                    password = Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("Enter new phone:");
                    phone = Console.ReadLine();
                    break;
                case 5:
                    Console.WriteLine("Enter new age:");
                    age = Convert.ToInt32(Console.ReadLine());
                    break;
                case 6:
                    Console.WriteLine("Enter new country:");
                    country = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public override void DeleteProfile()
        {
            name = email = password = phone = country = string.Empty;
            age = 0;
            Console.WriteLine("Profile has been deleted.");
        }

        public override void ViewProfile()
        {
            Console.WriteLine("----- Artist Profile -----");
            Console.WriteLine($"Name:     {name}");
            Console.WriteLine($"Email:    {email}");
            Console.WriteLine($"Phone:    {phone}");
            Console.WriteLine($"Age:      {age}");
            Console.WriteLine($"Country:  {country}");
            Console.WriteLine($"Followers: {followers}");
            Console.WriteLine("--------------------------");
        }

        public void Upload(string song)
        {
            Console.WriteLine($"Song {song} uploaded successfully.");
        }
    }
}
