using System.Xml.Linq;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string xmlPath;
        public BookRepository(IConfiguration configuration)
        {
            xmlPath =Path.Combine(Directory.GetCurrentDirectory(), configuration["XmlFilePath"]);
        }
        public List<Book> GetAllBooks()
        {
            var xmlD = XDocument.Load(xmlPath);
            return xmlD.Descendants("book")?.Select(x => ConvertToDTO(x))?.ToList();
        }
        public Book GetBookByIsbn(string isbn)
        {
            var xmlD = XDocument.Load(xmlPath);

            var book = xmlD.Descendants("book").FirstOrDefault(x => x.Element("isbn")?.Value == isbn);
            if (book == null)
                throw new ArgumentException("Book with isbn " + isbn + " not found");
            return ConvertToDTO(book);
        }
        public void AddBook(Book book)
        {
            var xmlD = XDocument.Load(xmlPath);
            var bookstore = xmlD.Element("bookstore");
            var newBook = new XElement("book",
                new XAttribute("category", book.Category),
                new XElement("isbn", book.Isbn),
                new XElement("title", book.Title),
                book.Author.Select(a => new XElement("author", a)),
                new XElement("year", book.Year),
                new XElement("price", book.Price)
            );

            bookstore?.Add(newBook);
            xmlD.Save(xmlPath);

        }
        public void UpdateBook(string isbn,Book book)
        {
            var xmlD = XDocument.Load(xmlPath);
            var element = xmlD.Descendants("book").FirstOrDefault(x => x.Element("isbn")?.Value == book.Isbn);
            if (element == null)
                throw new ArgumentException("Book with isbn " + book.Isbn + " not found");

            element.SetAttributeValue("category", book.Category);
            element.SetElementValue("title", book.Title);
            element.SetElementValue("year", book.Year);
            element.SetElementValue("price", book.Price);
            element.Elements("author").Remove();
            foreach (var author in book.Author)
            {
                element.Add(new XElement("author", author));
            }

            xmlD.Save(xmlPath);

        }
        public void DeleteBook(string isbn)
        {
            var xmlD = XDocument.Load(xmlPath);
            var element = xmlD.Descendants("book").FirstOrDefault(x => x.Element("isbn")?.Value == isbn);
            if (element == null)
                throw new ArgumentException("Book with isbn " + isbn + " not found");
            element.Remove();
            xmlD.Save(xmlPath);
        }

        private Book ConvertToDTO(XElement book)
        {
            return new Book
            {
                Isbn = book.Element("isbn").Value,
                Category = book.Attribute("category")?.Value,
                Author = book.Elements("author")?.Select(a => a.Value).ToList(),
                Year = Convert.ToInt16(book.Element("year")?.Value),
                Price = Convert.ToDouble(book.Element("price")?.Value),
                Title = book.Element("title")?.Value
            };
        }
    }
}
