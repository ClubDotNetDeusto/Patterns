using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthorService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IList<Author> GetAuthors()
        {
            return db.Authors.ToList();
        }

        public Author GetById(int id)
        {
            return db.Authors.Find(id);
        }

        public void CreateAuthor(Author a)
        {
            db.Authors.Add(a);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
