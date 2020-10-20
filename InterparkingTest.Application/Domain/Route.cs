using InterparkingTest.Application.Domain;

namespace InterparkingTest.Application.Commands.CreateRoute
{
    public class Route
    {
        public double EngineStartEffort { get; internal set; }
        public double CarConsumption { get; internal set; }
        public Coordinates EndPoint { get; internal set; }
        public Coordinates StartPoint { get; internal set; }
        public double Distance { get; internal set; }
        public double FuelConsumption { get; internal set; }
    }
}