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
using Microsoft.AspNetCore.Hosting;

namespace webui.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private TodoServiceAsyncAgent _serviceAgent;
        IConfiguration _configuration;
        private string _version;
        private string _environment;
        private IEnumerable<FeatureFlagViewModel> _featureFlagsInUse;

        public TodoController(ILogger<TodoController> logger, ITodoServiceAsyncContext serviceContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceAgent = new TodoServiceAsyncAgent(serviceContext);
            _featureFlagsInUse = FeatureFlags.GetFeatureFlagsInUse(configuration, _serviceAgent);
            _version = configuration["TodoControllerVersion"];
            _environment = webHostEnvironment.EnvironmentName;
        }


        // GET: TodoController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Service.Models.TodoItemDTO> todoItems = await _serviceAgent.Todo();

            TodoViewModel viewModel = new TodoViewModel(_featureFlagsInUse)
            {
                Environment = _environment,
                Version = _version
            };
            viewModel.Map(todoItems.ToList());
            return View(viewModel);
        }

        // GET: TodoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var itemDetails = new TodoItemDetailsViewModel(_featureFlagsInUse);
            itemDetails.Map(await _serviceAgent.Get(id));
            return View(itemDetails);
        }

        // GET: TodoController/Create
        public ActionResult Create()
        {
            return View(new TodoItemDetailsViewModel(_featureFlagsInUse));
        }

        // POST: TodoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                await _serviceAgent.CreateAsync(new Service.Models.TodoItemDTO().Map(collection, _featureFlagsInUse));
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

                Service.Models.TodoItemDTO editItem = new Service.Models.TodoItemDTO().Map(collection, _featureFlagsInUse);
                await _serviceAgent.Update(editItem);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(new TodoItemDetailsViewModel(_featureFlagsInUse).Map(collection, ex));
            }
        }

        // GET: TodoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceAgent.Delete(id);
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
