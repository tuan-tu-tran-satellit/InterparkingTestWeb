using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application.Domain
{
    public class RouteModificationResult
    {
        /// <summary>
        /// If not success, it (currently) means there was a problem with the routing API.
        /// And the coordinates should be ajusted.
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
