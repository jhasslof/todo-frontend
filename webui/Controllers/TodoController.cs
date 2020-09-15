using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webui.Mapper;
using webui.Service;

namespace webui.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private TodoServiceAgent ServiceAgent;

        public TodoController(ILogger<TodoController> logger, ITodoServiceContext serviceContext)
        {
            ServiceAgent = new TodoServiceAgent(serviceContext);
            _logger = logger;
        }

        // GET: TodoController
        public ActionResult Index()
        {
            var todoItems = ServiceAgent.Todo().ToList().ConvertAll(new Converter<Service.Models.TodoItem, Models.TodoViewModel>(TodoModelViewMapper.Map));
            return View(todoItems);
        }

        // GET: TodoController/Details/5
        public ActionResult Details(int id)
        {
            var todoItem = TodoModelViewMapper.Map(ServiceAgent.Get(id));
            return View(todoItem);
        }

        // GET: TodoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ServiceAgent.Create(TodoItemMapper.Map(collection));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoController/Edit/5
        public ActionResult Edit(int id)
        {
            var originalItem = TodoModelViewMapper.Map(ServiceAgent.Get(id));
            return View(originalItem);
        }

        // POST: TodoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                Service.Models.TodoItem editItem = TodoItemMapper.Map(collection);
                ServiceAgent.Update(editItem);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(TodoModelViewMapper.Map(collection, ex));
            }
        }

        // GET: TodoController/Delete/5
        public ActionResult Delete(int id)
        {
            ServiceAgent.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: TodoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
