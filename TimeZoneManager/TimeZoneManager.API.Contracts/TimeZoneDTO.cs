using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TimeZoneManager.API.Contracts
{

    public class TimeZoneDTO
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string Owner { get; set; }

        [Required]
        [MaxLength(250)]
        public string City { get; set; }

        [Required]
        public int GMTOffset { get; set; }

    }
}
