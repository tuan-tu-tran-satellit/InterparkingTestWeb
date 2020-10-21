using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InterparkingTest.Application.Domain
{
    public class Coordinates
    {
        [Required]
        [Range(-90,90)]
        public double Latitude { get; set; }
        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }
    }
}
