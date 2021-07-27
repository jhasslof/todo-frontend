using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webui.Mapper;
using webui.Service;
using webui.Models;

namespace webui.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private TodoServiceAgent ServiceAgent;
        internal static LaunchDarkly.Sdk.Server.LdClient ldClient = new LaunchDarkly.Sdk.Server.LdClient("YOUR_SDK_KEY");
        internal static readonly LaunchDarkly.Sdk.User ldUser = LaunchDarkly.Sdk.User.WithKey("administrator@test.com");

        public TodoController(ILogger<TodoController> logger, ITodoServiceContext serviceContext)
        {
            ServiceAgent = new TodoServiceAgent(serviceContext);
            _logger = logger;
        }

        // GET: TodoController
        public ActionResult Index()
        {
            TodoViewModel viewModel = new TodoViewModel();
            viewModel.todoItems = ServiceAgent.Todo().ToList().ConvertAll(new Converter<Service.Models.TodoItem, TodoItemViewModel>(TodoItemModelViewMapper.Map));
            viewModel.featureFlags = ServiceAgent.SupportedFeatureFlags().ToList().ConvertAll(new Converter<string, FeatureFlagViewModel>(TodoItemModelViewMapper.Map));
            return View(viewModel);
        }

        // GET: TodoController/Details/5
        public ActionResult Details(int id)
        {
            var itemDetails = new TodoItemDetailsViewModel();
            itemDetails.TodoItem = TodoItemModelViewMapper.Map(ServiceAgent.Get(id));
            itemDetails.featureFlags = ServiceAgent.SupportedFeatureFlags().ToList().ConvertAll(new Converter<string, FeatureFlagViewModel>(TodoItemModelViewMapper.Map));
            return View(itemDetails);
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

                Service.Models.TodoItem editItem = TodoItemMapper.Map(collection);
                ServiceAgent.Update(editItem);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(TodoItemModelViewMapper.Map(collection, ex));
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
