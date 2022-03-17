using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XandaApp.Data.Models
{
    public class EmailSettings
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Host { get; set; }
        public string HostIP { get; set; }
        public string PortNumber { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string CcEmail { get; set; }        
    }
}
