using System;
using Xunit;
using GenFu;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BookStore.Controllers;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Moq;

namespace bookstore.test
{
    public class BookStoreControllerTest
    {

        public IEnumerable<Book> GetFakeBook(int size){
            return A.ListOf<Book>(size);
        }

        public IEnumerable<Author> GetFakeAuthor(int size){
            return A.ListOf<Author>(size);
        }

        private Mock<BookStoreContext> CreateDbContext(IEnumerable<Author> data){
            var author = data.AsQueryable();

            var dbSet = new Mock<DbSet<Author>>();
            dbSet.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(author.Provider);
            dbSet.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(author.Expression);
            dbSet.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(author.ElementType);
            dbSet.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(author.GetEnumerator());

            var context = new Mock<BookStoreContext>();
            context.Setup(c => c.Authors).Returns(dbSet.Object);
            return context;
        }

        [Fact]
        public void GetAllBooksTest_BookListValue(){
            // arrange
            var data = GetFakeBook(5);
            String expected = data.First().Name;
            
            var service = new Mock<IBookStoreService>();
            service.Setup(x => x.GetAllBooksService()).Returns(data);
            var controller = new BookStoreController(service.Object);

             // act
            String actual = controller.GetAllBooks().First().Name;

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllBooksTest_BookListSize(){
            // arrange
            int expected = 10;
            var data = GetFakeBook(expected);

            var service = new Mock<IBookStoreService>();
            service.Setup(x => x.GetAllBooksService()).Returns(data);
            var controller = new BookStoreController(service.Object);

             // act
            int actual = controller.GetAllBooks().Count();

            // assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetAllAuthorsTest_AuthorListValue(){
            // arrange
            var data = GetFakeAuthor(5);
            String expected = data.First().Name;
            
            var service = new Mock<IBookStoreService>();
            service.Setup(x => x.GetAllAuthorsService()).Returns(data);
            var controller = new BookStoreController(service.Object);

             // act
            String actual = controller.GetAllAuthors().First().Name;

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllAuthorsTest_AuthorListSize(){
            // arrange
            int expected = 10;
            var data = GetFakeAuthor(expected);

            var service = new Mock<IBookStoreService>();
            service.Setup(x => x.GetAllAuthorsService()).Returns(data);
            var controller = new BookStoreController(service.Object);

             // act
            int actual = controller.GetAllAuthors().Count();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAuthorByNameTest_AuthorFirstInQuery(){
            var data = GetFakeAuthor(50);
            var expected = data
                            .Where(a => a.Name.Contains("a"))
                            .ToList();
            
            var context = CreateDbContext(data);
            var service = new BookStoreService(context.Object);

            // act
            var actual = service.GetAuthorService("a");

            // assert
            Assert.Equal(expected.First(), actual.First());
        }

        [Fact]
        public void PostAuthorTest_InsertNewAuthor(){
            var data = GetFakeAuthor(10);
            var expected = "New Actor";
            dynamic jsonObject = new JObject();
            jsonObject.name = expected;

            var context = CreateDbContext(data);
            var service = new BookStoreService(context.Object);
        
            // act
            service.PostAuthorService(jsonObject);
            var actual = service.GetAuthorService(expected).Count();

            // assert
            Assert.Equal(1, actual);
        }
    }
}
