using System;
using Xunit;
using GenFu;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using BookStore.Controllers;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Moq;

namespace bookstore.test
{
    public class BookStoreControllerTest
    {
        public IEnumerable<Book> GetTestBooks(){
            var i = 1;
            var books = A.ListOf<Book>(26);
            books.ForEach(x => x.BookId = i++);
            return books.Select(_ => _);
        }

         private Mock<BookStoreContext> CreateDbContext(){
            var persons = GetTestBooks().AsQueryable();

            var dbSet = new Mock<DbSet<Book>>();
            dbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(persons.Provider);
            dbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(persons.Expression);
            dbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            dbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(persons.GetEnumerator());

            var context = new Mock<BookStoreContext>();
            context.Setup(c => c.Books).Returns(dbSet.Object);
            return context;
        }

        [Fact]
        public void GetAllBooksTest()
        {
            // arrange
            var context = CreateDbContext();

            var service = new BookStoreService(context.Object); 

             // act
            var results = service.GetAllBooksService();

            var count = results.Id;

            // assert
            Assert.Equal(1, count);
        }
    }
}
