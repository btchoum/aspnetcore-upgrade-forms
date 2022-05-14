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

        public TicketsController(): this(new InMemoryTicketsRepository())
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
                var ticket = new Ticket
                {
                    Summary = model.Summary,
                    Description = model.Description,
                    TicketStatus = TicketStatus.New,
                    SubmitterId = User.Identity?.Name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Comments = new List<Comment>()
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
    }

    public class TicketCreateViewModel
    {
        [Required]
        public string Summary { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
