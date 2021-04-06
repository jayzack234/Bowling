using Bowling.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling.Components
{
    // Inherite from the ViewComponent
    public class TeamTypeViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;
        //Constructor
        public TeamTypeViewComponent(BowlingLeagueContext ctx)
        {
            //Brings in the context file
            context = ctx;
        }
        //Tells the program what to do when this is invoked, when an instance of the TeamTypeViewComponent is called
        public IViewComponentResult Invoke()
        {
            //Helps with highlighting whatever link is seleted
            ViewBag.SelectedCategory = RouteData?.Values["teamtype"];

            //Return default view
            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
