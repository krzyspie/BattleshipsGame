namespace Battleships.Helpers
{
    public interface INumberProvider
    {
        int GetNumber(int minValue, int maxValue);
    }
}