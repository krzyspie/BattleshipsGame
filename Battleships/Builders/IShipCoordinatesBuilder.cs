using Battleships.Models;

namespace Battleships.Builders
{
    public interface IShipCoordinatesBuilder
    {
        List<ShipCoordinate> GenerateShipCoordinates(int lenght);
    }
}