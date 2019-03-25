using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStoreApi.Models{
    public class BookStoreContext : DbContext{
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options){}
        public DbSet<Book> Books { get; set;}
        public DbSet<Author> Authors {get; set;}
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Data Source = Database/bookdb.sqlite");
        }
    }
}