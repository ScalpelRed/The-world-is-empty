using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gameplay.Items
{
    public partial class ItemList
    {
        protected SortedDictionary<string, Item> List = new();

        public ItemList()
        {

        }

        public void Add(params Item[] items)
        {
            foreach (Item item in items)
                List.Add(item.RawName, item);
        }

        public Item? Get(string name)
        {
            if (List.TryGetValue(name, out var item))
                return item;
            return null;
        }

        public bool TryGet(string name, out Item? item)
        {
            return List.TryGetValue(name, out item);
        }

        public bool Exists(string name) => List.ContainsKey(name);
    }
}
