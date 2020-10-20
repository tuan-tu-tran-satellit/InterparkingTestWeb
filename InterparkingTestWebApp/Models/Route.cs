using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterparkingTestWebApp.Models
{
    public class Route
    {
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double StopLatitude { get; set; }
        public double StopLongitude { get; set; }
        public int Id { get; set; }
    }
}
