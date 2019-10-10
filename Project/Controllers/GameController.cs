using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();

    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      Console.Clear();
      Console.WriteLine("You are a slice of bread in the small town of The Kitchen.\nThe dastardly villain, Deadeye Doughboy, has killed the sheriff and locked you, the deputy, in the breadbox.\nYou must break out and kill the criminal mastermind before he takes over the town.\nWhat is your name, brave hero?");
      string name = Console.ReadLine();
      _gameService.Setup(name);
      _gameService.Look();
      Print();
      while (true)
      {
        GetUserInput();
      }
    }

    //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
    public void GetUserInput()
    {
      Console.WriteLine("What would you like to do?");
      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();
      Console.Clear();
      //NOTE this will take the user input and parse it into a command and option.
      //IE: take silver key => command = "take" option = "silver key"
      switch (command)
      {
        case "look":
          _gameService.Look();
          Print();
          break;
        case "go":
          _gameService.Go(option);
          Print();
          if (_gameService.CheckTrap())
          {
            _gameService.Quit();
          }
          break;
        case "quit":
          _gameService.Quit();
          break;
        case "inventory":
          _gameService.Inventory();
          Print();
          break;
        case "take":
          _gameService.TakeItem(option);
          break;
        case "use":
          _gameService.UseItem(option);
          break;
      }
    }

    //NOTE this should print your messages for the game.
    private void Print()
    {
      foreach (string message in _gameService.Messages)
      {
        Console.WriteLine(message);
      }
      _gameService.Messages.Clear();
    }

  }
}