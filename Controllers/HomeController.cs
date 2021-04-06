using Bowling.Models;
using Bowling.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        //Constructor
        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }
        //Receive a long that has Team
        public IActionResult Index(long? teamtypeid, string teamtype, int pageNum = 0)
        {
            //This will set the maximum number of teams displayed on each page
            int pageSize = 5;
            //This will allow the user to filter by teams, by using the teamtypeid derived from the TeamTypeViewComponents to filter on TeamId
            return View(new IndexViewModel

            {
                Bowlers = (context.Bowlers
                .Where(t => t.TeamId == teamtypeid || teamtypeid == null)
                .OrderBy(t => t.BowlerLastName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    //If no team has been selected, then get the full count, otherwise, only count the number
                    //from the team type that has been selected
                    TotalNumItems = (teamtypeid == null ? context.Bowlers.Count() : 
                        context.Bowlers.Where(x => x.TeamId == teamtypeid).Count())
                },
                TeamCategory = teamtype
            });
                
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
