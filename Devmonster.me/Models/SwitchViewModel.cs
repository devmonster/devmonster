using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devmonster.me.Models
{
    public class SwitchViewModel
    {
        public List<NintendoGameItem> Items { get; set; } = new List<NintendoGameItem>();
        public bool isPlaying { get; set; }
        public string NoteText { get; set; }
    }
}