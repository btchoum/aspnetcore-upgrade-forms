using System.Collections.Generic;
using SimpleHelpDesk.Data.Entities;

namespace SimpleHelpDesk.Data.DataAccess
{
    public interface ITicketsRepository
    {
        IEnumerable<Ticket> GetAll();
        Ticket GetById(long id);
        long Add(Ticket ticket);
        void Update(Ticket ticket);
        void Remove(long id);
    }
}