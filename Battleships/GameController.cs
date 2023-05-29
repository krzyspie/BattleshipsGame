using Battleships.Builders;
using Battleships.Helpers;
using Battleships.Models;
using Battleships.Validators;
using Battleships.Views;

namespace Battleships
{
    public class GameController
    {
        private readonly IView gameView;

        public ICoordinatesValidator Validator { get; set; }
        public ICoordinateExtractor CoordinateExtractor { get; set; }
        public IShipsBuilder ShipsBuilder { get; set; }

        public GameController(IView view)
        {
            gameView = view;
            Validator = new CoordinatesValidator();
            CoordinateExtractor = new CoordinateExtractor();
            ShipsBuilder = new ShipsBuilder();
        }

        public void RunGame()
        {
            gameView.DisplayGameBoard();

            List<Ship> ships = ShipsBuilder.GenerateShips();

            var shipsCoordinates = ships.SelectMany(x => x.Coordinates).ToList();
            List<ShipCoordinate> userShipCoordinates = new(Constants.Constants.Settings.RowCount * Constants.Constants.Settings.ColumnCount);
            bool continueGame = true;

            do
            {
                string userCoordinates = gameView.ReadUserInput().Trim();

                var validation = Validator.Validate(userCoordinates);
                if (!validation)
                {
                    gameView.DisplayError(Constants.Constants.Messages.InvalidValueError);
                    continue;
                }

                (int columnNumber, int rowNumber) = CoordinateExtractor.GetCoordinates(userCoordinates);

                var userShipCoordinate = new ShipCoordinate(columnNumber, rowNumber);

                var shotShip = ships.FirstOrDefault(x => x.Coordinates.Contains(userShipCoordinate));
                var existingCoordinate = shipsCoordinates.FirstOrDefault(x => x.Column == columnNumber && x.Row == rowNumber);

                if (existingCoordinate is null)
                {
                    gameView.DisplayShipMiss(rowNumber, columnNumber);
                }

                bool isShipHit = existingCoordinate != null && shotShip != null;
                if (isShipHit)
                {
                    shotShip.Coordinates.Remove(userShipCoordinate);

                    bool isShipDestroyed = !shotShip.Coordinates.Any();
                    if (isShipDestroyed)
                    {
                        gameView.DisplayShipDestroyed(rowNumber, columnNumber, $"{shotShip.Type}");
                    }
                    else
                    {
                        gameView.DisplayShipHit(rowNumber, columnNumber, $"{shotShip.Type}");
                    }
                }

                bool wasAlreadyShot = userShipCoordinates.Contains(userShipCoordinate);
                if (wasAlreadyShot)
                {
                    gameView.DisplayAlreadyShot();
                }
                else
                {
                    userShipCoordinates.Add(userShipCoordinate);
                }

                continueGame = ships.SelectMany(x => x.Coordinates).Any();

            } while (continueGame);

            gameView.DisplayEndGameMessage();
        }
    }
}
