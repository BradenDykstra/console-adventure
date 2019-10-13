using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    public IPlayer CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      Item lockpick = new Item("Lockpick", "A simple toothpick. What idiot left this with the prisoner?", "unlock");

      Item horse = new Item("Horse", "Peanut the butter horse. By day, a stick of butter. By night, your noble steed.", "ride");

      Item lasso = new Item("Lasso", "A lasso made of string. It's quite useful for a cowboy. You've used it many times before to climb up to the freezer.", "climb");

      Room breadbox = new Room("Breadbox", "You're in the breadbox. The Deadeye locked you in here after he killed the sheriff. If the door is unlocked, you can exit to the east.", new List<Item> { lockpick }, false, true);

      Room countertop = new Room("Countertop", "The countertop, the outside portion of The Kitchen. This is where the appliances are. The breadbox is to the west, and the fridge is to the east. You could also go south...", new List<Item> { lasso }, false, false);

      Room fridge = new Room("Fridge", "The fridge, where the cold things are kept. Most importantly, your horse. You could go back west to the countertop, or ride your horse to the toaster, where the Deadeye waits for the final showdown.", new List<Item> { horse }, false, false);

      Room blender = new Room("Blender", "You've walked off the edge of the countertop, and fallen into a clear cup-shaped object, with blades below you.\nYou look outside, and spot Deadeye Doughboy, staring right at you.\nYou realize you've fallen into the blender, and your nemesis is at the controls...\n GAME OVER", new List<Item>(), true, false);

      breadbox.Exits.Add("east", countertop);
      countertop.Exits.Add("east", fridge);
      countertop.Exits.Add("west", breadbox);
      countertop.Exits.Add("south", blender);
      fridge.Exits.Add("west", countertop);

      CurrentRoom = breadbox;
    }
  }
}