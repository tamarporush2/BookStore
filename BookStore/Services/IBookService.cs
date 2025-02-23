namespace BookStore.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBookByIsbn(string isbn);
        void AddBook(Book book);
        void UpdateBook(string isbn,Book book);
        void DeleteBook(string isbn);
        string GetHtmlReport();
    }
}
