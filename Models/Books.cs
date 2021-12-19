
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebAPI.Models
{
    
    public class Books
    {
        [JsonProperty("carrier")]
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Operation { get; set; }
        public Books() => Console.WriteLine("Empty Class");
    }
}    