namespace BookStoreApi.Models{
    public class Book{
        public int BookId { get; set;}
        public string Name { get; set;}
        public Author Author { get; set;}
    }
}