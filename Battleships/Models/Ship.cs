using Battleships.Enums;

namespace Battleships.Models
{
    public class Ship
    {
        public Ship(ShipType shipType)
        {
            Type = shipType;
        }

        public ShipType Type { get; set; }
        public List<ShipCoordinate> Coordinates { get; set; }
    }
}
