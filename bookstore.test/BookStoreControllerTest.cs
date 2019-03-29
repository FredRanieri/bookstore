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
    public class DataBook{
        public string BookName { get; set;}
        public string AuthorName { get; set;}
    }

    public class DataAuthor{
        public int Id { get; set;}
        public string AuthorName { get; set;}
        public List<string> BooksName { get; set;}
    }

    public class BookStoreControllerTest
    {

        public IEnumerable<dynamic> GetFakeBook(int size){
            return A.ListOf<DataBook>(size);
        }

        public IEnumerable<dynamic> GetFakeAuthor(int size){
            return A.ListOf<DataAuthor>(size);
        }

        [Fact]
        public void GetAllBooksTest_BookListValue(){
            // arrange
            var data = GetFakeBook(5);
            String expected = data.First().BookName;
            
            var service = new Mock<IBookStoreService>();
            service.Setup(x => x.GetAllBooksService()).Returns(data);
            var controller = new BookStoreController(service.Object);

             // act
            String actual = controller.GetAllBooks().First().BookName;

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
            String expected = data.First().AuthorName;
            
            var service = new Mock<IBookStoreService>();
            service.Setup(x => x.GetAllAuthorsService()).Returns(data);
            var controller = new BookStoreController(service.Object);

             // act
            String actual = controller.GetAllAuthors().First().AuthorName;

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
    }
}
