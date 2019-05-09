using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeAPICore.Models
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public string booking_start { get; set; }
        public string booking_end { get; set; }
        public string booking_permission { get; set; }
        public int booking_status { get; set; }
        public int cars_id { get; set; }
        public int users_id { get; set; }
    }
}
