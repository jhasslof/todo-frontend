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
        private Settings Settings { get; set; }

        public TodoController(ILogger<TodoController> logger, ITodoServiceContext serviceContext, IConfiguration configuration)
        {
            ServiceAgent = new TodoServiceAgent(serviceContext);
            Settings = new Settings(configuration);
            FeatureFlagsInUse = InitializeFeatureFlagsInUse(ServiceAgent.SupportedFeatureFlags().ToList().ConvertAll(new Converter<string, FeatureFlagViewModel>(ViewModelFeatureFlagMapper.Map)));
            _logger = logger;
        }

        private IEnumerable<FeatureFlagViewModel> InitializeFeatureFlagsInUse(IEnumerable<FeatureFlagViewModel> serviceSupportedFeatureFlags)
        {
            //We can only use feature flags implemented by the controller
            //UiOnly == true : Feature Flag is not used by the backend service
            //UiOnly == false : We can only activate feature flags implemented by the controller and the backend service
            var featurFlagsInUse = new List<FeatureFlagViewModel>();
            foreach (var featureFlag in Settings.ControllerSupportedFeatureFlags)
            {
                if (featureFlag.UiOnly)
                {
                    featurFlagsInUse.Add(new FeatureFlagViewModel { Key = featureFlag.Key, UiOnly = true });
                }
                else if (serviceSupportedFeatureFlags.Contains(featureFlag))
                {
                    featurFlagsInUse.Add(new FeatureFlagViewModel { Key = featureFlag.Key });
                }
            }
            foreach (var featureFlag in featurFlagsInUse)
            {
                featureFlag.State = Settings.LaunchDarklyCredentials.LdClient.BoolVariation(featureFlag.Key, Settings.LaunchDarklyCredentials.LdUser, false);
            }
            return featurFlagsInUse;
        }

         // GET: TodoController
        public ActionResult Index()
        {
            TodoViewModel viewModel = new TodoViewModel();
            viewModel.Map(ServiceAgent.Todo().ToList(), FeatureFlagsInUse);
            return View(viewModel);
        }

        // GET: TodoController/Details/5
        public ActionResult Details(int id)
        {
            var itemDetails = new TodoItemDetailsViewModel();
            itemDetails.Map(ServiceAgent.Get(id), FeatureFlagsInUse);
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
                return View(new TodoItemDetailsViewModel().Map(collection, FeatureFlagsInUse, ex));
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
