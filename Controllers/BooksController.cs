using System.Runtime.InteropServices;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreWebAPI.Models;
using Microsoft.Data.SqlClient;
using AspNetCoreWebAPI.Data;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        //private readonly IBookRepository _repo;
        BookRepository _repo=new BookRepository();
        /* public BooksController(IBookRepository repo)
        {
            _repo = repo;
        } */
        List<Books> MyBooks = new List<Books>();
        [HttpPost]
        public async Task<IActionResult> Perform_CRUD(Books list)
        {
            MyBooks = await _repo.crud(list);
            return Ok(MyBooks);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            MyBooks = await _repo.GetBooks();
            int g=int.Parse("fgsdf");
            return Ok(MyBooks);
        }

        
    }
}