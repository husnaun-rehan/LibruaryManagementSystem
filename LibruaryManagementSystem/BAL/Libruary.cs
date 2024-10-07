using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibruaryManagementSystem.DAL;


namespace LibruaryManagementSystem.BAL
{
    public class Library
    {
        // Delegate for notification events
        public delegate void NotificationEventHandler(object sender, NotificationEventArgs e);

        // Event triggered when a notification is sent
        public event NotificationEventHandler NotificationEvent;

        // List to store books in the library
        private List<Book> books = new List<Book>();

        // List to store members of the library
        private List<Member> members = new List<Member>();

        // List to store loans of books
        private List<Loan> loans = new List<Loan>();

        // Adds a new book to the library asynchronously
        public async Task AddBookAsync(Book book)
        {
            await Task.Run(() => books.Add(book)); // Add the book to the list
            Console.WriteLine("Book added successfully.");
        }

        // Adds a new member to the library asynchronously
        public async Task AddMemberAsync(Member member)
        {
            await Task.Run(() => members.Add(member)); // Add the member to the list
            Console.WriteLine("Member added successfully.");
        }

        // Loans a book to a member asynchronously
        public async Task LoanBookAsync(int bookId, int memberId)
        {
            var book = books.Find(b => b.BookId == bookId); // Find the book by its ID
            if (book != null && book.IsAvailable)
            {
                var loan = new Loan
                {
                    LoanId = loans.Count + 1,
                    BookId = bookId,
                    MemberId = memberId,
                    LoanDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14)
                };

                await Task.Run(() => loans.Add(loan)); // Add the loan to the list
                book.IsAvailable = false; // Mark the book as unavailable
                OnNotificationEvent(new NotificationEventArgs { Message = "Book loaned successfully." });
            }
            else
            {
                Console.WriteLine("Book is not available.");
            }
        }

        // Gets the next available book ID
        public int GetNextBookId()
        {
            return books.Count + 1;
        }

        // Gets the next available member ID
        public int GetNextMemberId()
        {
            return members.Count + 1;
        }

        // Gets the list of books
        public List<Book> GetBooks()
        {
            return books;
        }

        // Gets the list of members
        public List<Member> GetMembers()
        {
            return members;
        }

        // Raises the NotificationEvent
        protected virtual void OnNotificationEvent(NotificationEventArgs e)
        {
            NotificationEvent?.Invoke(this, e);
        }
    }

}
