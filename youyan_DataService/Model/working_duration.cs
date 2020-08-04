using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youyan_DataService.Model
{
    public class working_duration
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public double fan_duration { get; set; }
        public double purify_duration { get; set; }
        public DateTime date { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
