using LibruaryManagementSystem.BAL;
using LibruaryManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibruaryManagementSystem
{

    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a new library instance
            Library library = new Library();

            // Subscribe to the notification event
            library.NotificationEvent += Library_NotificationEvent;

            while (true)
            {
                // Display the menu options
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Add Member");
                Console.WriteLine("3. Loan Book");
                Console.WriteLine("4. View Books");
                Console.WriteLine("5. View Members");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                // Handle the user's menu choice
                switch (choice)
                {
                    case "1":
                        await AddBookAsync(library);
                        break;
                    case "2":
                        await AddMemberAsync(library);
                        break;
                    case "3":
                        await LoanBookAsync(library);
                        break;
                    case "4":
                        ViewBooks(library);
                        break;
                    case "5":
                        ViewMembers(library);
                        break;
                    case "6":
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static async Task AddBookAsync(Library library)
        {
            // Prompt the user for book details
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();
            Console.Write("Enter book author: ");
            string author = Console.ReadLine();

            // Create a new book with the provided details
            Book book = new Book { BookId = library.GetNextBookId(), Title = title, Author = author, IsAvailable = true };

            // Add the book to the library asynchronously
            await library.AddBookAsync(book);
        }

        private static async Task AddMemberAsync(Library library)
        {
            // Prompt the user for member details
            Console.Write("Enter member name: ");
            string name = Console.ReadLine();
            Console.Write("Enter member email: ");
            string email = Console.ReadLine();

            // Create a new member with the provided details
            Member member = new Member { MemberId = library.GetNextMemberId(), Name = name, Email = email };

            // Add the member to the library asynchronously
            await library.AddMemberAsync(member);
        }

        private static async Task LoanBookAsync(Library library)
        {
            // Prompt the user for book ID and member ID
            Console.Write("Enter book ID to loan: ");
            int bookId = int.Parse(Console.ReadLine());
            Console.Write("Enter member ID: ");
            int memberId = int.Parse(Console.ReadLine());

            // Loan the book to the member asynchronously
            await library.LoanBookAsync(bookId, memberId);
        }

        private static void ViewBooks(Library library)
        {
            // Display the list of books
            var books = library.GetBooks();
            Console.WriteLine("Books in the Library:");
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Available: {book.IsAvailable}");
            }
        }

        private static void ViewMembers(Library library)
        {
            // Display the list of members
            var members = library.GetMembers();
            Console.WriteLine("Members of the Library:");
            foreach (var member in members)
            {
                Console.WriteLine($"ID: {member.MemberId}, Name: {member.Name}, Email: {member.Email}");
            }
        }

        private static void Library_NotificationEvent(object sender, NotificationEventArgs e)
        {
            // Display the notification message
            Console.WriteLine($"Notification: {e.Message}");
        }
    }
}
