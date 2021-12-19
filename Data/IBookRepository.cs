using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreWebAPI.Models;

namespace AspNetCoreWebAPI.Data
{
    public interface IBookRepository
    {
           Task<List<Books>> GetBooks();
           Task<List<Books>> crud(Books list1);
    }
}