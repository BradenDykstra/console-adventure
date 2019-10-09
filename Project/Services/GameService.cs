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
      throw new System.NotImplementedException();
    }
    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      Messages.Add(_game.CurrentRoom.Description);
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup(string playerName)
    {
      Player player = new Player(playerName, new List<Item>());
      Item lockpick = new Item("Lockpick", "A simple toothpick. What idiot left this with the prisoner?");
      Item sixshooter = new Item("Six-Shooter", "This is your gun. Deadeye Doughboy took it from you before he locked you up.");
      Item horse = new Item("Butter Horse", "Peanut the butter horse. By day, a stick of butter. By night, your noble steed.");
      Room breadbox = new Room("Breadbox", "The box where the bread is kept. If the door is unlocked, you can exit to the east.", new List<Item> { lockpick }, new Dictionary<string, IRoom> { });
      Room countertop = new Room("Countertop", "This is where the appliances are. The breadbox is to the west, and the fridge is to the east. You could also go south...", new List<Item> { sixshooter }, new Dictionary<string, IRoom> { });
      Room fridge = new Room("Fridge", "Where the cold things are kept. Most importantly, your horse. You could go back west to the countertop, or ride your horse to the toaster, where the Deadeye waits for the final showdown.", new List<Item> { horse }, new Dictionary<string, IRoom> { });
      Room blender = new Room("Blender", "You've walked off the edge of the countertop, and fallen into a clear cup-shaped object, with blades below you.\nYou look outside, and spot Deadeye Doughboy, staring right at you.\nYou realize you've fallen into the blender, and your nemesis is at the controls...\n GAME OVER", new List<Item>(), new Dictionary<string, IRoom>());
      Room toaster = new Room("Toaster", "There he is, Deadeye Doughboy. This is it, the final battle. If you win, you save the town, but if you lose, you're toast.", new List<Item>(), new Dictionary<string, IRoom>());

      Rooms.Add(breadbox);
      Rooms.Add(countertop);
      Rooms.Add(fridge);
      Rooms.Add(blender);
      Rooms.Add(toaster);
      _game.CurrentRoom = breadbox;
      _game.CurrentPlayer = player;
    }
    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
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
  }
}