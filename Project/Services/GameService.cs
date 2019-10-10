using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    private IGame _game { get; set; }

    public List<string> Messages { get; set; }

    public List<Room> Rooms { get; set; }

    public GameService()
    {
      _game = new Game();
      Messages = new List<string>();
      Rooms = new List<Room>();
    }
    public void Setup(string playerName)
    {
      Player player = new Player(playerName, new List<Item>());
      _game.CurrentPlayer = player;
      _game.Setup();
    }
    public void Go(string direction)
    {
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        if (_game.CurrentRoom.Locked)
        {
          Messages.Add("You try to go " + direction + " but the door is locked, so you are unable to continue.");
        }
        else
        {
          _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
          Look();
        }
      }
    }
    public void Help()
    {
      Messages.Add(@"Commands:
inventory
look
go <direction>
quit
take <item>
use <item>
help");
    }

    public void Inventory()
    {
      Messages.Add("You are holding:");
      foreach (Item i in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{i.Name} - {i.Description}");
      }
    }

    public void Look()
    {
      Messages.Add(_game.CurrentRoom.Description);
      if (_game.CurrentRoom.Items.Count > 0)
      {
        Messages.Add("This area contains:");
      }
      foreach (Item i in _game.CurrentRoom.Items)
      {
        Messages.Add(i.Name);
      }
    }

    public void Quit()
    {
      Environment.Exit(0);
    }
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      throw new System.NotImplementedException();
    }
    public bool CheckTrap()
    {
      return _game.CurrentRoom.Trapped;
    }
    public bool DoesntHaveGun()
    {
      bool gun = true;
      foreach (Item i in _game.CurrentPlayer.Inventory)
      {
        if (i.Name == "Gun")
        {
          gun = false;
        }
      }
      return gun;
    }

    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      Item item = new Item(null, null, null);
      foreach (Item i in _game.CurrentRoom.Items)
      {
        if (i.Name.ToLower() == itemName)
        {
          item = i;
        }
      }
      if (item.Name != null)
      {
        _game.CurrentPlayer.Inventory.Add(item);
        _game.CurrentRoom.Items.Remove(item);
      }
      else
      {
        Messages.Add("There's no item called " + itemName + " in this area.");
      }
    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public bool UseItem(string itemName)
    {
      Item item = new Item(null, null, null);
      foreach (Item i in _game.CurrentPlayer.Inventory)
      {
        if (i.Name.ToLower() == itemName)
        {
          item = i;
        }
      }
      if (item.Name == null)
      {
        Messages.Add("You don't have an item called " + itemName + ".");
      }
      else if (item.Action == "unlock" && _game.CurrentRoom.Locked)
      {
        _game.CurrentRoom.Locked = false;
        Messages.Add("Door unlocked!");
      }
      else if (item.Action == "ride")
      {
        _game.CurrentRoom = new Room("Toaster", "You stand atop the toaster, staring down the heel of the loaf, Deadeye Doughboy. \"This kitchen ain't big enough for the two of us,\" He tells you. The duel is at high noon, and the clock is just about there. If you win, you save the town. If you lose, you're toast.", new List<Item>(), false, false);
        Messages.Add(_game.CurrentRoom.Description);
        return true;
      }
      return false;
    }
  }
}