using BookStore.Models;
using Business.IAuthorService;
using Infraestructure.Database;
using Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthorService : IAuthor
    {
        private IRepository<Author> db = new Repository<Author>(new ApplicationDbContext());

        public IList<Author> GetAuthors()
        {
            return db.Table.ToList();
        }

        public Author GetById(int id)
        {
            return db.GetById(id);
        }

        public void CreateAuthor(Author a)
        {
            db.Insert(a);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void EditAuthor(Author author)
        {
            db.UpdateByID(new object[] { author.Id}, author);
        }

        public void DeleteAuthorById(int id)
        {
            db.DeleteById(new object[] { id });
        }
    }
}
