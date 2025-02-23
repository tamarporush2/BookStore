using BookStore.Repositories;
using System.Text;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }
        public Book GetBookByIsbn(string isbn)
        {
            return _bookRepository.GetBookByIsbn(isbn);
        }
        public void AddBook(Book book)
        {
             _bookRepository.AddBook(book);
        }
        public void UpdateBook(string isbn,Book book)
        {
            _bookRepository.UpdateBook(isbn,book);
        }
        public void DeleteBook(string isbn)
        {
            _bookRepository.DeleteBook(isbn);
        }
        public string GetHtmlReport()
        {
            StringBuilder html = new StringBuilder();

            html.Append("<html><head><title>Bookstore Report</title></head><body>");
            html.Append("<h2>Bookstore Report</h2>");
            html.Append("<table border='1'><tr><th>ISBN</th><th>Title</th><th>Authors</th><th>Category</th><th>Year</th><th>Price</th></tr>");
           var books= _bookRepository.GetAllBooks();
            foreach (var book in books) {
                string authors = string.Join(", ", book.Author);
                html.Append($"<tr><td>{book.Isbn}</td><td>{book.Title}</td><td>{authors}</td><td>{book.Category}</td><td>{book.Year}</td><td>${book.Price:F2}</td></tr>");
            }
            html.Append("</table></body></html>");
            return html.ToString();
        }
    }
}
