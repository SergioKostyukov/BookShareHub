using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShareHub.Core.Domain.Entities;


namespace BookShareHub.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetBooksByUserId(string userId);
        Task AddBook(Book book);
    }
}
