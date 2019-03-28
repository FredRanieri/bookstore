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

        public BookStoreController(IBookStoreService context){
            _service = context;
        }


        [HttpGet("allBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks(){
            return await _service.GetAllBooksService();
        }


        [HttpGet("allAuthors")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors(){
            return await _service.GetAllAuthorsService();
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