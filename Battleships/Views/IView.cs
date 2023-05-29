namespace Battleships.Views
{
    public interface IView
    {
        void DisplayError(string errorMessage);
        void DisplayGameBoard();
        void DisplayShipHit(int rowNumber, int columnNumber, string shipType);
        void DisplayShipDestroyed(int rowNumber, int columnNumber, string shipType);
        void DisplayAlreadyShot();
        void DisplayShipMiss(int rowNumber, int columnNumber);
        void DisplayEndGameMessage();
        string ReadUserInput();
    }
}