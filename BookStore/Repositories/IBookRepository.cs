namespace BookStore.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBookByIsbn(string isbn);
        void AddBook(Book book);
        void UpdateBook(string isbn,Book book);
        void DeleteBook(string isbn);
    }
}
