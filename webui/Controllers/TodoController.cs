using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webui.Mapper;
using webui.Service;
using webui.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace webui.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private TodoServiceAgent ServiceAgent;
        public IEnumerable<FeatureFlagViewModel> FeatureFlagsInUse;

        public TodoController(ILogger<TodoController> logger, ITodoServiceContext serviceContext, IConfiguration configuration)
        {
            _logger = logger;
            ServiceAgent = new TodoServiceAgent(serviceContext);
            FeatureFlagsInUse = TodoFeatureFlags.GetFeatureFlagsInUse(configuration, ServiceAgent);
        }

        // GET: TodoController/FeatureFlags
        public ActionResult FeatureFlags()
        {
            return View(FeatureFlagsInUse);
        }

        // GET: TodoController
        public ActionResult Index()
        {
            TodoViewModel viewModel = new TodoViewModel(FeatureFlagsInUse);
            viewModel.Map(ServiceAgent.Todo().ToList());
            return View(viewModel);
        }

        // GET: TodoController/Details/5
        public ActionResult Details(int id)
        {
            var itemDetails = new TodoItemDetailsViewModel(FeatureFlagsInUse);
            itemDetails.Map(ServiceAgent.Get(id));
            return View(itemDetails);
        }

        // GET: TodoController/Create
        public ActionResult Create()
        {
            return View(new TodoItemDetailsViewModel(FeatureFlagsInUse));
        }

        // POST: TodoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ServiceAgent.Create(new Service.Models.TodoItem().Map(collection, FeatureFlagsInUse));
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
            return Details(id);
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

                Service.Models.TodoItem editItem = new Service.Models.TodoItem().Map(collection, FeatureFlagsInUse);
                ServiceAgent.Update(editItem);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(new TodoItemDetailsViewModel(FeatureFlagsInUse).Map(collection, ex));
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
