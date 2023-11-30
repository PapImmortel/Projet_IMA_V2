using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_IMA
{
    class Lampe
    {
        private float puissance;
        /*private int x;
        private int y;
        private int z;*/
        private V3 direction;

        public float Puissance { get { return this.puissance; } set { this.puissance = value; } }

        public V3 Direction { get { return this.direction; } set { this.direction = value; } }

        public Lampe(float puissance, V3 direction)
        {
            this.puissance = puissance;
            this.direction = direction;
        }

        public Couleur getCouleur()
        {
            return Couleur.White * puissance;
        }
    }
}