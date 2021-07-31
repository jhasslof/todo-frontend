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
using System.Threading.Tasks;

namespace webui.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private TodoServiceAsyncAgent ServiceAgent;
        public IEnumerable<FeatureFlagViewModel> FeatureFlagsInUse;
        IConfiguration Configuration;

        public TodoController(ILogger<TodoController> logger, ITodoServiceAsyncContext serviceContext, IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = logger;
            ServiceAgent = new TodoServiceAsyncAgent(serviceContext);
            FeatureFlagsInUse = TodoFeatureFlags.GetFeatureFlagsInUse(configuration, ServiceAgent);
        }


        // GET: TodoController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Service.Models.TodoItemDTO> todoItems = await ServiceAgent.Todo();

            TodoViewModel viewModel = new TodoViewModel(FeatureFlagsInUse); 
            viewModel.Map(todoItems.ToList());
            return View(viewModel);
        }

        // GET: TodoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var itemDetails = new TodoItemDetailsViewModel(FeatureFlagsInUse);
            itemDetails.Map(await ServiceAgent.Get(id));
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
        public async Task<IActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                await ServiceAgent.CreateAsync(new Service.Models.TodoItemDTO().Map(collection, FeatureFlagsInUse));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id);
        }

        // POST: TodoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                Service.Models.TodoItemDTO editItem = new Service.Models.TodoItemDTO().Map(collection, FeatureFlagsInUse);
                await ServiceAgent.Update(editItem);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(new TodoItemDetailsViewModel(FeatureFlagsInUse).Map(collection, ex));
            }
        }

        // GET: TodoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await ServiceAgent.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: TodoController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
