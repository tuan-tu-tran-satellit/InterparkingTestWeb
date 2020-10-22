namespace InterparkingTest.Application.Domain
{
    public class Route
    {
        public int Id { get; internal set; }
        public double EngineStartEffort { get; internal set; }
        /// <summary>
        /// In liters/ 100 km
        /// </summary>
        public double CarConsumption { get; internal set; }
        public Coordinates EndPoint { get; internal set; }
        public Coordinates StartPoint { get; internal set; }
        public string StartDescription { get; set; }
        public string EndDescription { get; set; }
        /// <summary>
        /// In km
        /// </summary>
        public double Distance { get; internal set; }
        /// <summary>
        /// In liters
        /// </summary>
        public double FuelConsumption { get; internal set; }
    }
}