﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InterparkingTest.Application.Domain
{
    public class RouteDefinition
    {
        [Required]
        public string StartDescription { get; set; }
        [Required]
        public string EndDescription { get; set; }
        [Required]
        public Coordinates StartPoint { get; set; }
        [Required]
        public Coordinates EndPoint { get; set; }
        /// <summary>
        /// In liters/ 100 km
        /// </summary>
        [Range(0, Double.MaxValue, ErrorMessage = "The car consumption must be positive.")]
        public double? CarConsumption { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "The engine start effort must be positive.")]
        public double? EngineStartEffort { get; set; }

        internal static RouteDefinition FromRoute(Route route)
        {
            return new RouteDefinition()
            {
                StartDescription = route.StartDescription,
                EndDescription = route.EndDescription,
                StartPoint = route.StartPoint,
                EndPoint = route.EndPoint,
                CarConsumption = route.CarConsumption,
                EngineStartEffort = route.EngineStartEffort,
            };
        }
    }
}
