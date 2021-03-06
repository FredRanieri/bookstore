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
                Author JK = new Author{ Name = "J. K. Rowling"};
                _dbContext.Authors.Add(JK);
                _dbContext.Books.Add(new Book{ Name = "Harry Potter - The Philosopher's Stone", Author = JK });
                _dbContext.Books.Add(new Book{ Name = "Harry Potter - The Chamber of Secrets", Author = JK });
                _dbContext.Books.Add(new Book{ Name = "Harry Potter - The Prisoner of Azkaban", Author = JK });

                Author JRR = new Author{ Name = "J. R. R. Tolkien"};
                _dbContext.Authors.Add(JRR);
                _dbContext.Books.Add(new Book{ Name = "Lord of the Rings - The Fellowship of the Ring", Author = JRR });
                _dbContext.Books.Add(new Book{ Name = "Lord of the Rings - The Two Towers", Author = JRR });
                _dbContext.Books.Add(new Book{ Name = "Lord of the Rings - The Return of the King", Author = JRR });
                
                Author rick = new Author{ Name = "Rick Riordan"};
                _dbContext.Authors.Add(rick);
                _dbContext.Books.Add(new Book{ Name = "Percy Jackson - The Lightning Thief", Author = rick });
                _dbContext.Books.Add(new Book{ Name = "Percy Jackson - The Sea of Monsters", Author = rick });
                _dbContext.Books.Add(new Book{ Name = "Percy Jackson - The Titan's Curse", Author = rick });

                _dbContext.SaveChanges();
            } 
        }

        public IEnumerable<dynamic> GetAllBooksService(){
            return  _dbContext.Books
                            .Select(b => new { BookName = b.Name, AuthorName = b.Author.Name})
                            .OrderBy(b => b.BookName).ToList();
        }

        public IEnumerable<dynamic> GetAllAuthorsService(){
            return _dbContext.Authors
                            .Select(a => new { AuthorName = a.Name, BooksName = a.Books.Select(b => b.Name)})
                            .OrderBy(a => a.AuthorName).ToList();
        }

        public IEnumerable<Author> GetAuthorService(string name){
            return _dbContext.Authors
                            .Where(a => a.Name.Contains(name))
                            .Include(a => a.Books)
                            .ToList(); 
        }

        public IEnumerable<dynamic> GetBookService(string name){
            return _dbContext.Books
                            .Where(b => b.Name.Contains(name))
                            .Select(b => new { Name = b.Name, Author = b.Author.Name })
                            .ToList();
        }

        public void PostBookService(dynamic data){
            int id = data.authorId.ToObject<int>();
            String name =  data.name.ToObject<String>();

            Author author = _dbContext.Authors.Find(id);
            _dbContext.Books.Add(new Book{ Name = name, Author = author });
            _dbContext.SaveChanges();
        }

        public void PostAuthorService(dynamic data){
            String name =  data.name.ToObject<String>();
            _dbContext.Authors.Add(new Author{ Name = name });
            _dbContext.SaveChanges();
        }

        public void DeleteBookService(String name){
            var book = _dbContext.Books.Single(b => b.Name == name);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

        public void DeleteAuthorService(String name){
            var author = _dbContext.Authors.Include(a => a.Books).Single(a => a.Name == name);
            var books = _dbContext.Books.Where(b => b.Author.Name == name);

            foreach(Book book in books){
                _dbContext.Books.Remove(book);  
            }
            
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }

    public interface IBookStoreService
    {
        IEnumerable<dynamic> GetAllBooksService();
        IEnumerable<dynamic> GetAllAuthorsService();
        IEnumerable<Author> GetAuthorService(string name);
        IEnumerable<dynamic> GetBookService(string name);
        void PostBookService(dynamic data);
        void PostAuthorService(dynamic data);
        void DeleteAuthorService(String name);
        void DeleteBookService(String name);
    }
}