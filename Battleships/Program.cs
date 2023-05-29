using Battleships;
using Battleships.Views;

var gameView = new ConsoleView();
var game = new GameController(gameView);
game.RunGame();

Console.Read();