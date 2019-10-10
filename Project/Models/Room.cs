using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {
    public Room(string name, string description, List<Item> items, bool trapped, bool locked)
    {
      Name = name;
      Description = description;
      Items = items;
      Exits = new Dictionary<string, IRoom>();
      Trapped = trapped;
      Locked = locked;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }

    public bool Trapped { get; set; }

    public bool Locked { get; set; }


  }
}