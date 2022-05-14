using System;
using System.Collections.Generic;

namespace SimpleHelpDesk.Data.Entities
{
    public class Ticket
    {
        public long Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string SubmitterId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public string CommenterId { get; set; }
        public string Content { get; set; }
    }

    public enum TicketStatus
    {
        New = 1,
        Active,
        Cancelled,
        Resolved,
        Closed
    }
}