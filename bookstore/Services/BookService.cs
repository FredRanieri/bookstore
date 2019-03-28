using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;



namespace BookStoreApi.Services
{
    public class BookStoreService : IBookStoreService
    {
        private readonly BookStoreContext _dbContext;
        public BookStoreService(BookStoreContext dbContext)
        {
            _dbContext = dbContext;

            if(_dbContext.Authors.Count() == 0){
                Author newAt = new Author{ Name = "Walter Isaacson"};

                _dbContext.Authors.Add(new Author{ Name = "Eric Topol"});
                _dbContext.Authors.Add(new Author{ Name = "Max Tegmark"});
                _dbContext.Authors.Add(newAt);
                _dbContext.Books.Add(new Book{ Name = "Steve Jobs", Author = newAt });
                _dbContext.SaveChanges();
            } 
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksService()
        {
            return await _dbContext.Books.Include(b => b.Author).OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthorsService(){
            return await _dbContext.Authors.Include(a => a.Books).ToListAsync();
        }
    }

    public interface IBookStoreService
    {
        Task<ActionResult<IEnumerable<Book>>> GetAllBooksService();
        Task<ActionResult<IEnumerable<Author>>> GetAllAuthorsService();
    }
}