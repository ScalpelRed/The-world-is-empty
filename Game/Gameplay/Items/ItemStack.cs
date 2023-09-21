using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gameplay.Items
{
    public class ItemStack
    {
        public Item? Item { get; protected set; }
        public int Count { get; protected set; }

        public ItemStack(Item item, int count)
        {
            Reset(item, count);
        }

        public ItemStack()
        {
            Reset();
        }

        /// <returns>True, if Count fell below zero.</returns>
        public bool Add(int count)
        {
            Count += count;
            if (Count == 0) Item = null;
            else if (Count < 0)
            {
                Item = null;
                Count = 0;
                return true;
            }
            return false;
        }

        public void Reset(Item item, int count)
        {
            Item = item;
            Count = count;
        }

        public void Reset()
        {
            Item = null;
            Count = 0;
        }
    }
}
