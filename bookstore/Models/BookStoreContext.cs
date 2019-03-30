using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStoreApi.Models{
    public class BookStoreContext : DbContext{
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options){}
        public BookStoreContext(){ }
        
        public virtual DbSet<Book> Books { get; set;}
        public virtual DbSet<Author> Authors {get; set;}
    }
}