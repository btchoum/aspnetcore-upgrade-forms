using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SimpleHelpDesk.Data.DataAccess;
using SimpleHelpDesk.Data.Entities;

namespace SimpleHelpDesk.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsRepository _repository;

        public TicketsController() : this(new InMemoryTicketsRepository())
        {

        }

        public TicketsController(ITicketsRepository repository)
        {
            _repository = repository;
        }

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = _repository.GetAll();
            return View(tickets);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        public ActionResult Create(TicketCreateViewModel model)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                var userName = User.Identity?.Name;
                var ticket = new Ticket
                {
                    Summary = model.Summary,
                    Description = model.Description,
                    TicketStatus = TicketStatus.New,
                    SubmitterId = userName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Comments = new List<Comment>
                    {
                        new Comment{ Content = "First comment", CommenterId = userName},
                        new Comment{ Content = "Second comment", CommenterId = userName},
                        new Comment{ Content = "Another comment", CommenterId = userName}
                    }
                };

                _repository.Add(ticket);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tickets/Create
        public ActionResult Details(long id)
        {
            var ticket = _repository.GetById(id);

            if (ticket == null) return HttpNotFound();

            return View(ticket);
        }

        //TODO: change routing to POST tickets/{id:long}/comments
        // POST Tickets/AddComment
        [HttpPost]
        public ActionResult AddComment(AddCommentModel comment)
        {
            var ticket = _repository.GetById(comment.TicketId);

            if (ticket == null) return HttpNotFound();

            var newComment = new Comment
            {
                Content = comment.Content,
                CommenterId = User.Identity?.Name,
                TicketId = comment.TicketId
            };
            ticket.Comments.Add(newComment);
            
            _repository.Update(ticket);

            return PartialView("_Comment", newComment);
        }
    }


    public class AddCommentModel
    {
        public long TicketId { get; set; }
        public string Content { get; set; }
    }

    public class TicketCreateViewModel
    {
        [Required]
        public string Summary { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
