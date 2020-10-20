using InterparkingTest.Application.Domain;

namespace InterparkingTest.Application.Services
{
    public interface IRoutingService
    {
        double CalculateDistance(Coordinates startPoint, Coordinates endPoint);
    }
}