namespace Battleships.Views
{
    public class ConsoleView : IView
    {
        private const int GridStartLeftPoint = 4;
        private const int GridStartTopPoint = 3;
        private const int GridCellLength = 2;
        private const int UserInputPositionOffset = 2;
        private int cursorPositionTop = 0;

        public ConsoleView()
        {
            Console.Title = "Battleship game!";
        }

        public void DisplayGameBoard()
        {
            Console.Clear();
            Console.WriteLine("Hello, let's play a game!");
            Console.WriteLine();
            Console.WriteLine("|__|A|B|C|D|E|F|G|H|I|J|");

            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 12; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Console.Write($"|{j + 1}");
                            break;
                        case 1:
                            Console.Write(j == 9 ? $"|" : $" |");
                            break;
                        default:
                            Console.Write("_|");
                            break;
                    }
                }

                Console.WriteLine();
            }

            Console.Write("\n\n");
            Console.WriteLine("Type ship coordinate (e.g. 'B3'):");
            var cursorPosition = Console.GetCursorPosition();
            (_, cursorPositionTop) = cursorPosition;
        }

        public string ReadUserInput()
        {
            var userInput = Console.ReadLine();
            return userInput ?? string.Empty;
        }

        public void DisplayError(string errorMessage)
        {
            DisplayMessage(errorMessage, ConsoleColor.Red);
        }

        public void DisplayShipHit(int rowNumber, int columnNumber, string shipType)
        {
            DisplayShotResult(rowNumber, columnNumber, $"Hit {shipType}!", ConsoleColor.Green, "X|");
        }

        public void DisplayShipMiss(int rowNumber, int columnNumber)
        {
            DisplayShotResult(rowNumber, columnNumber, "Miss!", ConsoleColor.Red, "O|");
        }

        public void DisplayShipDestroyed(int rowNumber, int columnNumber, string shipType)
        {
            DisplayShotResult(rowNumber, columnNumber, $"Destroyed {shipType}!", ConsoleColor.Green, "X|");
        }

        public void DisplayAlreadyShot()
        {
            DisplayMessage("Was shot already!", ConsoleColor.Yellow);
        }

        public void DisplayEndGameMessage()
        {
            DisplayMessage("Congratulations, you destroyed all ships!", ConsoleColor.Green);
        }

        private void DisplayMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            ClearLine(cursorPositionTop - UserInputPositionOffset);
            Console.Write(message);
            Console.ResetColor();
            ClearLine(cursorPositionTop);
        }

        private void DisplayShotResult(int rowNumber, int columnNumber, string message, ConsoleColor color, string shotSign)
        {
            Console.ForegroundColor = color;
            ClearLine(cursorPositionTop - UserInputPositionOffset);
            Console.Write(message);
            Console.ResetColor();
            DisplayShotSignOnBoard(rowNumber, columnNumber, shotSign);
            ClearLine(cursorPositionTop);
        }

        private void DisplayShotSignOnBoard(int rowNumber, int columnNumber, string shotSign)
        {
            Console.SetCursorPosition(GridStartLeftPoint + (GridCellLength * (columnNumber - 1)), GridStartTopPoint + (rowNumber - 1));
            Console.Write(shotSign);
        }

        private void ClearLine(int lineNumber)
        {
            Console.SetCursorPosition(0, lineNumber);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, lineNumber);
        }
    }
}
