using Battleships.Models;

namespace Battleships.Builders
{
    public interface IShipsBuilder
    {
        List<Ship> GenerateShips();
    }
}