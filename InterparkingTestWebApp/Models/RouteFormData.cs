using InterparkingTest.Application.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterparkingTestWebApp.Models
{
    public class RouteFormData
    {
        [Required]
        public RouteDefinition Route { get; set; }
        public int? Id { get; set; }
    }
}
