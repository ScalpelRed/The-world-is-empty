using Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Util;
using Game.Main;

namespace Game.Gameplay.Items
{
    public abstract class Item
    {
        public readonly Texture Icon;
        public readonly TranslatableString Name;
        public string RawName { get => Name.String; }

        public Item(string rawName, GameCore core)
        {
            Name = new TranslatableString(rawName);
            //Icon = core.Assets.GetTexture("item/" + rawName);
        }
    }
}
