using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Item : IItem
  {
    public Item(string name, string description, string action)
    {
      Name = name;
      Description = description;
      Action = action;
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public string Action { get; set; }
  }
}