using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youyan_DataService.Model
{
    public class device_state
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public bool signal_state { get; set; }
        public double signal_intensity { get; set; }
        public bool fan_state { get; set; }
        public double fan_current { get; set; }
        public bool purify_state { get; set; }
        public double purify_current { get; set; }
        public bool power_state { get; set; }
        public double voltage { get; set; }
        public DateTime date { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
