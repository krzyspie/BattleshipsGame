namespace Battleships.Helpers
{
    public class CoordinateExtractor : ICoordinateExtractor
    {
        private readonly Dictionary<char, int> columnMapping = new()
        {
            { 'A', 1 },
            { 'B', 2 },
            { 'C', 3 },
            { 'D', 4 },
            { 'E', 5 },
            { 'F', 6 },
            { 'G', 7 },
            { 'H', 8 },
            { 'I', 9 },
            { 'J', 10 },
        };

        public (int columnNumber, int rowNumber) GetCoordinates(string coordinates)
        {
            int column = columnMapping[char.ToUpper(coordinates[0])];
            int row = int.Parse(coordinates[1..]);

            return (column, row);
        }
    }
}
