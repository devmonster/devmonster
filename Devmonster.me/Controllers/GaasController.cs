using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devmonster.me.Models;

namespace Devmonster.me.Controllers
{
    public class GaasController : Controller
    {
        // GET: Gaas

        public ActionResult operations()
        {
            return View("Operations");
        }


        public ActionResult Xmas(string id, string from)
        {
            ResponseData data = new ResponseData();
            string greeting = "Merry Christmas";

            if (from != null)
            {
                data.Message = $"{greeting} {id}!";
                data.From = from;
            }
            else
            {
                data.Message = $"{greeting}!";
                data.From = id;
            }
      
            return View("GenericWithFrom", data);
        }

        public ActionResult Holidays(string id, string from)
        {

            ResponseData data = new ResponseData();
            string greeting = "Happy Holidays";

            if (from != null)
            {
                data.Message = $"{greeting} {id}!";
                data.From = from;
            }
            else
            {
                data.Message = $"{greeting}!";
                data.From = id;
            }

            return View("GenericWithFrom", data);
        }

        public ActionResult Season(string id, string from)
        {

            ResponseData data = new ResponseData();
            string greeting = "Seasons Greetings";

            if (from != null)
            {
                data.Message = $"{greeting} {id}!";
                data.From = from;
            }
            else
            {
                data.Message = $"{greeting}!";
                data.From = id;
            }

            return View("GenericWithFrom", data);
        }

        public ActionResult NewYear(string id, string from)
        {

            ResponseData data = new ResponseData();
            string greeting = "Happy New Year";

            if (from != null)
            {
                data.Message = $"{greeting} {id}!";
                data.From = from;
            }
            else
            {
                data.Message = $"{greeting}!";
                data.From = id;
            }

            return View("NewYear", data);
        }

        public ActionResult YearEnd(string id, string from)
        {
            ResponseData data = new ResponseData();
            string greeting = "Merry Christmas and a Happy New Year";

            if (from != null)
            {
                data.Message = $"{greeting} {id}!";
                data.From = from;
            }
            else
            {
                data.Message = $"{greeting}!";
                data.From = id;
            }

            return View("NewYear", data);
        }
    }
}