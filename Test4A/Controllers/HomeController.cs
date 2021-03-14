using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Test4A.Models;

namespace Test4A.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISWAPI _swapi;
        private readonly IVoteDb _db;

        public HomeController(ILogger<HomeController> logger, ISWAPI swapi, IVoteDb db)
        {
            _logger = logger;
            _swapi = swapi;
            _db = db;
        }

        public async Task<IActionResult> Index(int? id, int? score)
        {
            dynamic model = new ExpandoObject();
            model.list = await _swapi.List();
            model.film = null;
            if (id != null)
            {
                model.film = await _swapi.Details((int)id);
                model.film.score = score == null ? _db.Vote(model.film.episode_id) : _db.Vote(model.film.episode_id, (int)score);
                model.voted = score != null;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
