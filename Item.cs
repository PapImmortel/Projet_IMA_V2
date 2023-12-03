using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_IMA
{
    class Item
    {
        private int aType;
        private Texture itemTexture;
        private Texture itemBumpiness;
        public Item(int pType, Texture PitemTexture, Texture pItemBumpiness)
        {
            this.aType = pType;
            this.itemTexture = PitemTexture;
            this.itemBumpiness = pItemBumpiness;
        }

        public Texture TexturePack { get { return this.itemTexture; } protected set { this.itemTexture = value; } }
        public Texture TextureBumpiness { get { return this.itemBumpiness; } protected set { this.itemBumpiness = value; } }
        public int getType { get { return this.aType; } protected set { this.aType = value; } }

    }
}