using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using System.Threading;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();

    private string Drawn = "";

    private bool playing = true;

    private bool ending = false;

    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      Console.Clear();
      Console.WriteLine("You are a slice of bread in the small town of The Kitchen.\nThe dastardly villain, Deadeye Doughboy, has killed the sheriff and locked you, the deputy, in the breadbox.\nYou must break out and kill the criminal mastermind before he takes over the town.\nWhat is your name, brave hero?");
      string name = Console.ReadLine();
      _gameService.Setup(name);
      Console.Clear();
      _gameService.Look();
      Print();
      while (playing)
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
          playing = false;
          break;
        case "inventory":
          _gameService.Inventory();
          Print();
          break;
        case "take":
          _gameService.TakeItem(option);
          Print();
          break;
        case "use":
          if (_gameService.UseItem(option))
          {
            EndGame();
          };
          Print();
          break;
        case "help":
          _gameService.Help();
          Print();
          break;
      }
    }

    private void EndGame()
    {
      // Timer firstTimer = new Timer(FirstTick, null, 5000, 5000);
      Console.Clear();
      Print();
      System.Console.WriteLine("To win the duel, you have to type draw as fast as possible after the screen says 'Draw'. Press enter when you're ready to begin the duel.");
      Console.ReadLine();
      Console.WriteLine("The clock strikes high noon...");
      int time = 1;
      while (true)
      {
        if (Console.KeyAvailable)
        {
          if (_gameService.DoesntHaveGun())
          {
            Console.WriteLine("You reach for the gun, but quickly realize it isn't there. You remember seeing your gun on the countertop. You have little time for regrets as the Deadeye shoots you between the eyes.\nGAME OVER");
            _gameService.Quit();
          }
          string d = Console.ReadLine();
          if (d.ToLower() == "draw")
          {
            Console.WriteLine("As soon as you hear the word ring out, you draw your gun and fire. You look up and see the Deadeye clutching his chest. Soon, he falls into the toaster slot, and you push down the lever. The duel is over. You've won, you've saved the town!");
            _gameService.Quit();
          }
        }
        Thread.Sleep(1000);
        if (time < 2)
        {
          Console.WriteLine("DRAW!");
        }
        if (time++ > 3)
        {
          Console.WriteLine("You were too slow. Just as you reach for your gun, the Deadeye puts a bullet in you. You fall into the toaster, everything starts heating up, and your vision fades away... \nGAME OVER");
          _gameService.Quit();
        }
      }
    }

    public void FirstTick(Object obj)
    {
      System.Console.WriteLine("DRAW!");
      Timer timer = new Timer(TimerTick, null, 1000, 1000);
      Drawn = Console.ReadLine().ToLower();
      Console.WriteLine(Drawn);
    }
    public void TimerTick(Object obj)
    {
      ending = false;
      if (Drawn == "draw")
      {

      }
      else
      {
        System.Console.WriteLine("You typed: " + Drawn);

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