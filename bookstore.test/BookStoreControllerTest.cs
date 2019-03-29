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

        public async Task<ActionResult<IEnumerable<dynamic>>> GetFakeData(){
            return await Task.FromResult(A.ListOf<DataBook>(20));
        }

        [Fact]
        public async void GetAllBooksTest(){
            // arrange
            var data = GetFakeData();
            var service = new Mock<IBookStoreService>();
            service.Setup(x => x.GetAllBooksService()).Returns(data);

            var controller = new BookStoreController(service.Object);

             // act
            var result = await controller.GetAllBooks();
            Console.WriteLine(result);

            // assert
            Assert.Equal("20", result);
        }
    }
}
