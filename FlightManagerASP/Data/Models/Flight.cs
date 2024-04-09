using Microsoft.AspNetCore.DataProtection;
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
        [Required]
        [Display(Name = "Location To")]
        [StringLength(30, ErrorMessage = "Location must be exactly or longer than 1 letter.", MinimumLength = 1)]
        public string LocationTo { get; set; }
        [Required]
        [Display(Name = "Location From")]
        [StringLength(30, ErrorMessage = "Location must be exactly or longer than 1 letter.", MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Location must contain only letters.")]
        public string LocationFrom { get; set; }
        [Required]
        [Display(Name = "Departure Time")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy/ HH-mm}")]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Display(Name = "Arrival Time")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy/ HH-mm}")]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Display(Name = "Plane Type")]
        public string PlaneType { get; set; }
        [Required]
        [MaxLength(5)]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Plane Code must contain only uppercase letters and digits.")]
        [Display(Name = "Plane Code")]
        public string PlaneUniqueNumber { get; set; }
        [Required]
        [Display(Name = "Pilot Name")]
        [StringLength(50, ErrorMessage ="Pilot name must be longer than 2 letters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Pilot name must contain only letters.")]
        public string PilotName { get; set; }
        [Required]
        [Range(0, 1000)]
        [Display(Name = "Capacity")]
        public int PassangerCapacity { get; set; }
        [Required]
        [Range(0, 1000)]
        [Display(Name = "Business Capacity")]
        public int BusinessPassengerCapacity { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public Flight()
        {
            Reservations = new List<Reservation>();    
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DepartureTime >= ArrivalTime)
            {
                yield return new ValidationResult("Arrival time must be after Departure time", new[] { "ArrivalTime" });
            }

            if (LocationTo == LocationFrom)
            {
                yield return new ValidationResult("LocationTo and LocationFrom cannot be the same", new[] { "LocationTo", "LocationFrom" });
            }
        }
    }
}
