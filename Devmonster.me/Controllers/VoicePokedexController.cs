using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Devmonster.me.Controllers
{
    public class VoicePokedexController : Controller
    {

        [HttpPost]
        public JsonResult Pokemon()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();

            Models.VoiceWebhookQuery query = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.VoiceWebhookQuery>(json);
            Models.VoiceWebhookResponse response = new Models.VoiceWebhookResponse();
            response.source = "Devmonster Pokedex";
            response.displayText = "Test";

            if (query?.result?.metadata?.intentName?.ToLower() == "ask.location")
            {
                if (query?.result?.parameters?.Pokemon?.ToLower() == "pikachu")
                {
                    if (query?.result?.parameters?.Region?.ToLower() == "alola")
                    {
                        response.speech = "You can evolve Pichu, or find wild Pokemon on Route 1 (SOS Battle)";
                    }

                    if (query?.result?.parameters?.Region?.ToLower() == "kalos")
                    {
                        response.speech = "Look in either Santalune Forest, Route 3, or get Pikachu with Friend Safari (Electric)";
                    }

                }
                else
                {
                    response.speech = "I can't help you with that Pokemon yet";
                }
            }

            if (query?.result?.metadata?.intentName?.ToLower() == "ask.info")
            {
                if (query?.result?.parameters?.Pokemon?.ToLower() == "pikachu")
                {
                    response.speech = "This Pokémon has electricity-storing pouches on its cheeks. These appear to become electrically charged during the night while Pikachu sleeps. It occasionally discharges electricity when it is dozy after waking up.";
                }

                if (query?.result?.parameters?.Pokemon?.ToLower() == "bulbasaur")
                {
                    response.speech = "Bulbasaur can be seen napping in bright sunlight. There is a seed on its back. By soaking up the sun’s rays, the seed grows progressively larger.";
                }
            }




            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}