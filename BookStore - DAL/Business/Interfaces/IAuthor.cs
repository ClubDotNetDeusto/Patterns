using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IAuthorService
{
    public interface IAuthor
    {
        IList<Author> GetAuthors();
        Author GetById(int id);
        void CreateAuthor(Author a);
        void Dispose();
        void EditAuthor(Author author);
        void DeleteAuthorById(int id);
    }
}
