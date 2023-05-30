using Battleships.Enums;
using Battleships.Models;

namespace Battleships.Builders
{
    public class ShipsBuilder : IShipsBuilder
    {
        private readonly List<ShipType> shipTypes = new() { ShipType.Battleship, ShipType.Destroyer, ShipType.Destroyer };

        public ShipsBuilder()
        {
            ShipCoordinateBuilder = new ShipCoordinatesBuilder();
        }

        public IShipCoordinatesBuilder ShipCoordinateBuilder { get; set; }

        public List<Ship> GenerateShips()
        {
            var shipCoordinatesCount = shipTypes.Sum(x => (int)x);
            List<ShipCoordinate> allShipsCoordinates = new(shipCoordinatesCount);
            List<Ship> ships = new(shipTypes.Count);

            foreach (var item in shipTypes)
            {
                ships.Add(GenerateShip(item, allShipsCoordinates));
            }

            return ships;
        }

        private Ship GenerateShip(ShipType shipType, List<ShipCoordinate> allShipsCoordinates)
        {
            var shouldRegenerateShip = false;
            var shipLength = (int)shipType;
            Ship ship = new(shipType);

            do
            {
                var generatedCoordinates = ShipCoordinateBuilder.GenerateShipCoordinates(shipLength);
                shouldRegenerateShip = allShipsCoordinates.Any(x => generatedCoordinates.Exists(y => y.Row == x.Row && y.Column == x.Column));
                if (!shouldRegenerateShip)
                {
                    allShipsCoordinates.AddRange(generatedCoordinates);
                    ship.Coordinates.AddRange(generatedCoordinates);
                }

            } while (shouldRegenerateShip);

            return ship;
        }
    }
}
