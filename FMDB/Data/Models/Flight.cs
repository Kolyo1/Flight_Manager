using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        public string LocationTo { get; set; }
        [NotNull]
        public string LocationFrom { get; set; }
        [NotNull]
        public DateTime TakeOffTime { get; set; }
        [NotNull]
        public DateTime LandingTime { get; set; }
        [NotNull]
        public string PlaneType { get; set; }
        [NotNull]
        [MaxLength(10)]
        public string PlaneUniqueNumber { get; set; }
        [NotNull]
        public string PilotName { get; set; }
        [NotNull]
        [Range(0, 1000)]
        public int PassangerCapacity { get; set; }
        [NotNull]
        [Range(0, 1000)]
        public int BusinessPassengerCapacity { get; set; }

    }
}
