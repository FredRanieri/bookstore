using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using BookStoreApi.Models;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public BookStoreController(BookStoreContext context){
            _context = context;
        
            if(_context.Authors.Count() == 0){
                Author newAt = new Author{ Name = "Walter Isaacson"};

                _context.Authors.Add(new Author{ Name = "Eric Topol"});
                _context.Authors.Add(new Author{ Name = "Max Tegmark"});
                _context.Authors.Add(newAt);
                _context.Books.Add(new Book{ Name = "Steve Jobs", Author = newAt });
                _context.SaveChanges();
            }
        }

        [HttpGet("allBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks(){
            return await _context.Books.Include(b => b.Author).OrderBy(b => b.Name).ToListAsync();
        }

        [HttpGet("allAuthors")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors(){
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

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

            return StatusCode(201);
        }

        [HttpPost("author")]
        public async Task<ActionResult<Book>> PostAuthor(Author infoAuthor){
            _context.Authors.Add(infoAuthor);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
