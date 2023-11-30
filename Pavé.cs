using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_IMA
{
    class Pavé
    {
        private V3 origine;
        private V3 coté1;
        private V3 coté2;
        private Texture rectangleTexture;
        private Texture rectangleBumpiness;
        
        public Pavé(V3 pOrigine,V3 pCote1,V3 pCote2,Texture pRectangleTexture, Texture pRectangleBumpiness)
        {
            this.origine = pOrigine;
            this.coté1 = pCote1;
            this.coté2 = pCote2;
            this.rectangleTexture = pRectangleTexture;
            this.rectangleBumpiness = pRectangleBumpiness;
        }
        public Texture TexturePack { get { return rectangleTexture; } set { rectangleTexture = value; } }
        public Texture Bumpiness { get { return rectangleBumpiness; } set { rectangleBumpiness = value; } }
        public V3 positionOrigine { get { return this.origine; } set { this.origine = value; } }
        public V3 positionCote1 { get { return this.coté1; } set { this.coté1 = value; } }
        public V3 positionCote2 { get { return this.coté2; } set { this.coté2 = value; } }

        public Couleur getCouleurText(float u, float v)
        {
            Couleur cRectangle = this.rectangleTexture.LireCouleur(u, v);
            return cRectangle;
        }
        public V3[] dessinVariable(float u, float v, float kBumpinessRec, V3 positionCamera3D)
        {
            V3 P3D = this.origine + u * this.coté1 + v * this.coté2;


            V3 n3D = new V3(0, 0, 60000);
            rectangleBumpiness.Bump(u, v, out float dhdu, out float dhdv);



            V3 dmdu = this.coté1;
            V3 dmdv = this.coté2;

            V3 nBump3D = n3D + (kBumpinessRec * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));

            n3D = nBump3D;
            n3D.Normalize();
            Couleur couleurFinale = new Couleur(0, 0, 0);
            V3 d3D = positionCamera3D - P3D;
            d3D.Normalize();

            V3[] lesVariables = new[] { n3D, d3D, P3D };
            return lesVariables;//, d3D,point3D
        }






    }
}