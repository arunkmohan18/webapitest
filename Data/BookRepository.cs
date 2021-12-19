using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AspNetCoreWebAPI.DataLayer;
using AspNetCoreWebAPI.Models;

namespace AspNetCoreWebAPI.Data
{
    public class BookRepository:IBookRepository
    {
        public BookRepository()
        {
        }

        public async Task<List<Books>> GetBooks()
        {
            List<Books> MyBooks = new List<Books>();

            DataConnection dc = new DataConnection();
            var task = new Task(() =>
            {
                DataTable dt = dc.GetData("SELECT * FROM dbo.Books");

                // POPULATE THE LIST WITH DATA.
                foreach (DataRow dr in dt.Rows)
                {
                    MyBooks.Add(new Books
                    {
                        BookID = Convert.ToInt32(dr["BookID"]),
                        BookName = dr["BookName"].ToString(),
                        Category = dr["Category"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"])
                    });
                }
            });
            task.Start();
            await task;
            return MyBooks;

        }
        public async Task<List<Books>> crud(Books list1)
        {
            List<Books> MyBooks = new List<Books>();
            bool bDone = false;
            DataConnection dc = new DataConnection();
            long ret = 0;
            switch (list1.Operation)
            {
                case "READ":
                    bDone = true;
                    break;

                case "SAVE":

                    if (list1.BookName != "" & list1.Category != "" & list1.Price > 0)
                    {
                        dc.Save("Books", list1, true, ref ret);
                        bDone = true;
                    }

                    break;
                case "UPDATE":

                    if (list1.BookName != "" & list1.Category != "" & list1.Price > 0)
                    {
                        string sqls = "UPDATE dbo.Books SET BookName = '" + list1.BookName.Trim() + "', Category = '" + list1.Category.Trim() + "', " + "Price = '" + list1.Price + "' WHERE BookID = '" + list1.BookID + "'";
                        dc.SetDatabase(sqls);
                    }

                    break;
                case "DELETE":

                    string sql = "DELETE FROM dbo.Books WHERE BookID = '" + list1.BookID + "'";
                    dc.SetDatabase(sql);
                    break;
            }


            if (bDone)
            {
                MyBooks = await GetBooks();
            }

            return MyBooks;
        }
    }
}