using iTrainee.Services.Interfaces;
using iTrainee.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using iTrainee.Models;

namespace iTrainee.APIs.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class SubTopicsController : Controller
    {
        ISubTopicsService _subTopicsService = null;
        public SubTopicsController (ISubTopicsService subTopicsService)
        {
            _subTopicsService = subTopicsService;
        }
        // GET: SubTopicsController
        [HttpGet]
        public IEnumerable<SubTopics> GetAllSubtopics()
        {
            return _subTopicsService.GetAllSubTopics();
        }

        //// GET: SubTopicsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: SubTopicsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SubTopicsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
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

        //// GET: SubTopicsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: SubTopicsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
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

        //// GET: SubTopicsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: SubTopicsController/Delete/5
       [HttpDelete]
        public bool DeleteSubTopics(int id)
        {
            return _subTopicsService.DeleteSubTopics(id);
        }
    }
}
