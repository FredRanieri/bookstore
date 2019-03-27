using System;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BookStore.Controllers;
using Moq;

namespace bookstore.test
{
    public class BookStoreControllerTest
    {
        [Fact]
        public async Task PostNewBook_ReturnStatusCode_isTrue(){
            //Arrange
            var mock = new Mock<BookStoreController>();
            mock.Setup(m => m.ControllerContext);

            BookStoreController controller = new BookStoreController(null); // Need the Context to work with database
            int expected = 201;
            dynamic book = new JObject();
            book.name = "Test Book";
            book.authorId = 1;

            //Act
            int actual = await controller.PostBook(book);

            //Assert
            Assert.Equal(expected, actual);

        }
    }
}
