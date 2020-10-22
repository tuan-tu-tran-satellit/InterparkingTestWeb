using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application.Domain
{
    //This class could just be replaced by a boolean, but later we may want to add 
    //other fields to specify the kind of failure.
    //Since I was already doing the work of introducing this notion of modification result, I figured I'd create a class, instead of just a boolean
    public class RouteModificationResult
    {
        /// <summary>
        /// If not success, it (currently) means there was a problem with the routing API.
        /// And the coordinates should be ajusted.
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
