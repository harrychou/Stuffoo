using System.Linq;
using System.Web.Mvc;
using Stuffoo.Core.DataAccess;
using Stuffoo.Core.Models;

namespace Stuffoo.MvcWeb.Controllers
{
    [Authorize]
    public class StuffController : Controller
    {
        private IRepository<Stuff> _repository;

        public StuffController(IRepository<Stuff> repository)
        {
            _repository = repository;
        }

        //
        // GET: /Stuff/

        public ActionResult Index()
        {
            var stuffs = _repository.Items.ToList();
            return View(stuffs);
        }

        //
        // GET: /Stuff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Stuff/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Stuff/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Stuff/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Stuff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
