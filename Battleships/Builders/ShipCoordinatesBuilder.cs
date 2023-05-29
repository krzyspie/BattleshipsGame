using Battleships.Enums;
using Battleships.Helpers;
using Battleships.Models;

namespace Battleships.Builders
{
    public class ShipCoordinatesBuilder : IShipCoordinatesBuilder
    {
        public INumberProvider NumberProvider { get; set; }

        public ShipCoordinatesBuilder()
        {
            NumberProvider = new NumberProvider();
        }

        public List<ShipCoordinate> GenerateShipCoordinates(int lenght)
        {
            int direction = NumberProvider.GetNumber(0, 1);

            int startPointRow = direction == (int)ShipDirection.Horizontal
                ? NumberProvider.GetNumber(1, Constants.Constants.Settings.RowCount)
                : NumberProvider.GetNumber(1, Constants.Constants.Settings.RowCount - lenght);

            int startPointColumn = direction == (int)ShipDirection.Horizontal
                ? NumberProvider.GetNumber(1, Constants.Constants.Settings.ColumnCount - lenght)
                : NumberProvider.GetNumber(1, Constants.Constants.Settings.ColumnCount);

            List<ShipCoordinate> coordinates = new(lenght)
            {
                new ShipCoordinate (startPointColumn, startPointRow)
            };

            for (int i = 1; i < lenght; i++)
            {
                var shipCoordinate = direction == (int)ShipDirection.Horizontal
                    ? new ShipCoordinate(startPointColumn + i, startPointRow)
                    : new ShipCoordinate(startPointColumn, startPointRow + i);

                coordinates.Add(shipCoordinate);
            }

            return coordinates;
        }
    }
}
