using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BookStoreApi.Models;
using BookStoreApi.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly IBookStoreService _service;

        public BookStoreController(IBookStoreService service){
            _service = service;
        }

        [HttpGet("start")]
        public String GetStart() => "Welcome!";

        [HttpGet("allBooks")]
        public IEnumerable<dynamic>  GetAllBooks() => _service.GetAllBooksService();

        [HttpGet("allAuthors")]
        public IEnumerable<dynamic> GetAllAuthors() => _service.GetAllAuthorsService();

        [HttpGet("findAuthor/{name}")]
        public IEnumerable<Author> GetAuthor(string name) => _service.GetAuthorService(name);

        [HttpGet("findBook/{name}")]
        public IEnumerable<dynamic> GetBook(string name) => _service.GetBookService(name);

        [HttpPost("newBook")]
        public IActionResult PostBook(dynamic data){
            try{
                _service.PostBookService(data);
                return Ok();
            }
            catch(Exception e){
                Console.WriteLine(e);
                return BadRequest();
            } 
        }

        [HttpPost("newAuthor")]
        public IActionResult PostAuthor(dynamic data){
            try{
                _service.PostAuthorService(data);
                return Ok();
            }
            catch(Exception e){
                Console.WriteLine(e);
                return BadRequest();
            } 
        }

        [HttpDelete("deleteBook/{name}")]
        public IActionResult DeleteBook(String name){
            try{
                _service.DeleteBookService(name);
                return NoContent();
            }catch(Exception e){
                Console.WriteLine(e);
                return BadRequest();
            }
        } 

        [HttpDelete("deleteAuthor/{name}")]
        public IActionResult DeleteAuthor(String name){
            try{
                _service.DeleteAuthorService(name);
                return NoContent();
            }catch(Exception e){
                Console.WriteLine(e);
                return BadRequest();
            }
        } 
    }
}










/*
        [HttpGet("findAuthor/{name}")]
        public async Task<ActionResult<Author>> GetAuthor(string name){
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);

            if (author == null){
                return NotFound();
            }
            return author;
        }

        [HttpGet("findBook/{name}")]
        public async Task<ActionResult<Book>> GetBook(string name){
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == name);

            if (book == null){
                return NotFound();
            }
            return book;
        }

        [HttpPost("book")]
        public async Task<ActionResult<Book>> PostBook(dynamic infoBook){

            Author author = await _context.Authors.FindAsync(infoBook.authorId.ToObject<int>());
            Book insertBook = new Book{Name = infoBook.name.ToObject<String>(), Author = author}; 

            _context.Books.Add(insertBook);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("author")]
        public async Task<ActionResult<Book>> PostAuthor(Author infoAuthor){
            _context.Authors.Add(infoAuthor);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */