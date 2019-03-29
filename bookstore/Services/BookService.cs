using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;
using Newtonsoft.Json;

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

        public async Task<ActionResult<IEnumerable<dynamic>>> GetAllBooksService()
        {
            return await _dbContext.Books
                            .Select(b => new { BookName = b.Name, AuthorName = b.Author.Name})
                            .OrderBy(b => b.BookName).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<dynamic>>> GetAllAuthorsService(){
            return await _dbContext.Authors
                            .Select(a => new { AuthorName = a.Name, BooksName = a.Books.Select(b => b.Name)})
                            .OrderBy(a => a.AuthorName).ToListAsync();
        }

        public async Task<ActionResult<Author>> GetAuthorService(string name){
            return await _dbContext.Authors.FirstOrDefaultAsync(a => a.Name == name); 
        }

        public async Task<ActionResult<Book>> GetBookService(string name){
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.Name == name);
        }
    }

    public interface IBookStoreService
    {
        Task<ActionResult<IEnumerable<dynamic>>> GetAllBooksService();
        Task<ActionResult<IEnumerable<dynamic>>> GetAllAuthorsService();
        Task<ActionResult<Author>> GetAuthorService(string name);
        Task<ActionResult<Book>> GetBookService(string name);
    }
}