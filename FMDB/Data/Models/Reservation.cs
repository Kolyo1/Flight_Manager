using Data.Enums;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Index(nameof(Reservation.EGN), IsUnique = true)]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [NotNull]
        [MaxLength(30)]
        public string MiddleName { get; set; }
        [NotNull]
        [MaxLength(30)]
        public string LastName { get; set; }
        [NotNull]
        [MaxLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "EGN must contain only digits")]
        public string EGN { get; set; }
        [NotNull]
        [MaxLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must contain only digits")]
        public string PhoneNumber { get; set; }
        [NotNull]
        public string Nationality { get; set; }
        [NotNull]
        public TicketType TicketType { get; set; }

        [ForeignKey("FlightId")]
        public int FlightId { get; set; }

    }
}
