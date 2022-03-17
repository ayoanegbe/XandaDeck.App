using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XandaApp.Data.Models
{
    public class SmtpConfig
    {
        [Key]
        public int SmtpSettingId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Host { get; set; }
        [Display(Name = "Host IP")]
        public string HostIP { get; set; }
        [Display(Name = "Port Number")]
        public string PortNumber { get; set; }
        public string Password { get; set; }
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
    }
}
