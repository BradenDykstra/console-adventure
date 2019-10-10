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
    public void Go(string direction)
    {
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        Messages.Add(_game.CurrentRoom.Description);
      }
    }
    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      foreach (Item i in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{i.Name} - {i.Description}");
      }
    }

    public void Look()
    {
      Messages.Add(_game.CurrentRoom.Description);
      Messages.Add("This area contains:");
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

    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      Item item = new Item(null, null);
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
    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
    public void Setup(string playerName)
    {
      Player player = new Player(playerName, new List<Item>());
      Item lockpick = new Item("Lockpick", "A simple toothpick. What idiot left this with the prisoner?");
      Item sixshooter = new Item("Six-Shooter", "This is your gun. Deadeye Doughboy took it from you before he locked you up.");
      Item horse = new Item("Butter Horse", "Peanut the butter horse. By day, a stick of butter. By night, your noble steed.");
      Room breadbox = new Room("Breadbox", "You're in the breadbox. The Deadeye locked you in here after he killed the sheriff. If the door is unlocked, you can exit to the east.", new List<Item> { lockpick }, false);
      Room countertop = new Room("Countertop", "The countertop, the outside portion of The Kitchen. This is where the appliances are. The breadbox is to the west, and the fridge is to the east. You could also go south...", new List<Item> { sixshooter }, false);
      Room fridge = new Room("Fridge", "The fridge, where the cold things are kept. Most importantly, your horse. You could go back west to the countertop, or ride your horse to the toaster, where the Deadeye waits for the final showdown.", new List<Item> { horse }, false);
      Room blender = new Room("Blender", "You've walked off the edge of the countertop, and fallen into a clear cup-shaped object, with blades below you.\nYou look outside, and spot Deadeye Doughboy, staring right at you.\nYou realize you've fallen into the blender, and your nemesis is at the controls...\n GAME OVER", new List<Item>(), true);
      Room toaster = new Room("Toaster", "You stand atop the toaster, staring down the heel of the loaf, Deadeye Doughboy. \"This kitchen ain't big enough for the two of us,\" He tells you. The clock strikes high noon, and you draw your gun. If you win, you save the town. If you lose, you're toast.", new List<Item>(), false);
      breadbox.Exits.Add("east", countertop);
      countertop.Exits.Add("east", fridge);
      countertop.Exits.Add("west", breadbox);
      countertop.Exits.Add("south", blender);
      fridge.Exits.Add("west", countertop);
      fridge.Exits.Add("south", toaster);
      Rooms.Add(breadbox);
      Rooms.Add(countertop);
      Rooms.Add(fridge);
      Rooms.Add(blender);
      Rooms.Add(toaster);
      _game.CurrentRoom = breadbox;
      _game.CurrentPlayer = player;
    }
  }
}