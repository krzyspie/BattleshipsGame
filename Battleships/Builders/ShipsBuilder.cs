using Battleships.Enums;
using Battleships.Models;

namespace Battleships.Builders
{
    public class ShipsBuilder : IShipsBuilder
    {
        private readonly List<ShipType> shipTypes = new() { ShipType.Battleship, ShipType.Battleship, ShipType.Destroyer };
        private readonly Dictionary<ShipType, short> shipsMapping = new Dictionary<ShipType, short>()
        {
            { ShipType.Battleship, 5 },
            { ShipType.Destroyer, 4 }
        };

        public ShipsBuilder()
        {
            ShipCoordinateBuilder = new ShipCoordinatesBuilder();
        }

        public IShipCoordinatesBuilder ShipCoordinateBuilder { get; set; }

        public List<Ship> GenerateShips()
        {
            var shipCoordinatesCount = shipTypes.Sum(x => shipsMapping[x]);
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
            var shipLength = shipsMapping[shipType];
            Ship ship = new(shipType)
            {
                Coordinates = new List<ShipCoordinate>(shipLength)
            };

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
