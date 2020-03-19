using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devmonster.me.Models
{
    public class NintendoGameItem
    {
        public string GameName { get; set; }
        public string GameHours { get; set; }
        public string ArtUrl { get; set; }
        public bool IsHighlighted { get; set; }
    }
}