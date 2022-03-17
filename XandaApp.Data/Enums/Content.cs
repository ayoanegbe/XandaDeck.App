using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XandaApp.Data.Enums
{
    public enum Content
    {
        Layout = 1,
        Playlist = 2,
        Media = 3,
        [Display(Name = "Turned Off")]
        TurnedOff = 4
    }
}
