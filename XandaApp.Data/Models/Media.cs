using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XandaApp.Data.Enums;

namespace XandaApp.Data.Models
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public ContentType Type { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
