using System.Collections.Generic;
using System.Linq;
using SimpleHelpDesk.Data.Entities;

namespace SimpleHelpDesk.Data.DataAccess
{
    public class InMemoryTicketsRepository : ITicketsRepository
    {
        private static readonly Dictionary<long, Ticket>
            Tickets = new Dictionary<long, Ticket>();

        public IEnumerable<Ticket> GetAll()
        {
            return Tickets.Values.ToList();
        }

        public Ticket GetById(long id)
        {
            return Tickets.ContainsKey(id) ? Tickets[id] : null;
        }

        public long Add(Ticket ticket)
        {
            ticket.Id = Tickets.Keys.Count == 0 ? 1 : Tickets.Keys.Max() + 1;

            Tickets[ticket.Id] = ticket;

            return ticket.Id;
        }

        public void Update(Ticket ticket)
        {
            Tickets[ticket.Id] = ticket;
        }

        public void Remove(long id)
        {
            Tickets.Remove(id);
        }
    }
}
