using System.Text.RegularExpressions;

namespace Battleships.Validators
{
    public class CoordinatesValidator : ICoordinatesValidator
    {
        public bool Validate(string userCordinates)
        {
            if (string.IsNullOrWhiteSpace(userCordinates))
            {
                return false;
            }

            string pattern = @"^[A-Ja-j]([1-9]|10)$";
            Regex regex = new Regex(pattern);
            
            Match match = regex.Match(userCordinates);
            
            return match.Success;
        }
    }
}
