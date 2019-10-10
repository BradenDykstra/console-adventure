using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using System.Timers;

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
      Timer firstTimer = new Timer(5000);
      firstTimer.AutoReset = false;
      firstTimer.Elapsed += FirstTick;
      Console.Clear();
      Print();
      System.Console.WriteLine("To win the duel, you have to type draw as fast as possible after the screen says 'Draw'. Press enter when you're ready to begin the duel.");
      Console.ReadLine();
      System.Console.WriteLine("The clock strikes high noon...");
      firstTimer.Start();
    }

    public void FirstTick(Object obj, ElapsedEventArgs e)
    {
      System.Console.WriteLine("DRAW!");
      Timer timer = new Timer(1000);
      timer.AutoReset = false;
      timer.Elapsed += TimerTick;
      Drawn = Console.ReadLine().ToLower();
      timer.Start();
    }
    public void TimerTick(Object obj, ElapsedEventArgs e)
    {
      ending = false;
      if (Drawn == "draw")
      {
        System.Console.WriteLine("As soon as you hear the word ring out, you draw your gun and fire. You look up and see the Deadeye clutching his chest. Soon, he falls into the toaster slot, and you push down the lever. The duel is over. You've won, you've saved the town!");
        _gameService.Quit();
      }
      else
      {
        System.Console.WriteLine("You typed: " + Drawn);
        System.Console.WriteLine("You were too slow. Just as you reach for your gun, the Deadeye puts a bullet in you. You fall into the toaster, everything starts heating up, and your vision fades away... \nGAME OVER");
        _gameService.Quit();
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