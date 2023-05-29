using Battleships.Models;

namespace Battleships.Validators
{
    public interface ICoordinatesValidator
    {
        bool Validate(string userCordinates);
    }
}