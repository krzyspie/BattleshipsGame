namespace Battleships.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string Error { get; set; }

        public ValidationResult(bool isValid, string error = "")
        {
            IsValid = isValid;
            Error = error;
        }
    }
}
