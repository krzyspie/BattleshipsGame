namespace Battleships.Helpers
{
    public interface ICoordinateExtractor
    {
        (int columnNumber, int rowNumber) GetCoordinates(string coordinates);
    }
}