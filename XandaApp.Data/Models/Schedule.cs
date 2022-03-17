using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XandaApp.Data.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
