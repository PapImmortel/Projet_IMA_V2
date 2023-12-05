using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_IMA
{
    class Item
    {
        private int type;
        private Texture itemTexture;
        private Texture itemBumpiness;
        private float kBumpiness;
        private int kSpecularPower;

        public Item(int type, Texture itemTexture, Texture itemBumpiness, float kBumpiness)
        {
            this.type = type;
            this.itemTexture = itemTexture;
            this.itemBumpiness = itemBumpiness;
            this.kBumpiness = kBumpiness;
            this.kSpecularPower = 100;
        }

        public virtual Couleur getCouleurTexture(float u, float v)
        {

            return Couleur.White;
        }

        public virtual V3[] dessinVariable(float u, float v, V3 positionCamera3D)
        {
            return new V3[0];
        }

        public Texture TexturePack { get { return this.itemTexture; } protected set { this.itemTexture = value; } }
        public Texture TextureBumpiness { get { return this.itemBumpiness; } protected set { this.itemBumpiness = value; } }
        public float Bumpiness { get { return this.kBumpiness; } protected set { this.kBumpiness = value; } }
        public int SpecularPower { get { return this.kSpecularPower; } protected set { this.kSpecularPower = value; } }

        public virtual bool raycast(V3 DirRayon, V3 pPosCamera, ref V3 pointIntersection, ref float distanceMinim, ref float[] vUetV)
        {
            return false;
        }
    }
}