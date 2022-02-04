
using Microsoft.AspNetCore.Mvc;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Data;

namespace AspNetCoreWebAPI.Controllers

{
    
    
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repo;
        //BookRepository _repo=new BookRepository();
         public BooksController(IBookRepository repo)
        {
            _repo = repo;
        } 
        List<Books> MyBooks = new List<Books>();
        [HttpPost]
        public async Task<IActionResult> Perform_CRUD(Books list)
        {
            MyBooks = await _repo.crud(list);
            return Ok(MyBooks);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            MyBooks = await _repo.GetBooks();
            return Ok(MyBooks);
        }

        
    }
}