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
        private float kBumpiness;
        private int kSpecularPower;

        public Item(int pType, Texture PitemTexture, Texture pItemBumpiness, float pKBumpiness)
        {
            this.aType = pType;
            this.itemTexture = PitemTexture;
            this.itemBumpiness = pItemBumpiness;
            this.kBumpiness = pKBumpiness;
            this.kSpecularPower = 100;
        }
        public virtual Couleur getCouleurText(float u, float v)
        {

            return Couleur.White;
        }
        public virtual V3[] dessinVariable(float u, float v, V3 positionCamera3D)
        {
            return new V3[0];
        }
        public Texture TexturePack { get { return this.itemTexture; } protected set { this.itemTexture = value; } }
        public Texture TextureBumpiness { get { return this.itemBumpiness; } protected set { this.itemBumpiness = value; } }
        public float modifKBumpiness { get { return this.kBumpiness; } protected set { this.kBumpiness = value; } }
        public int modifKSpecularPower { get { return this.kSpecularPower; } protected set { this.kSpecularPower = value; } }

        public int getType { get { return this.aType; } protected set { this.aType = value; } }

    }
        
}