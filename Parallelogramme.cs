using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_IMA
{
    class Parallelogramme : Item
    {
        private V3 origine;
        private V3 coté1;
        private V3 coté2;
        
        public Parallelogramme(V3 pOrigine,V3 pCote1,V3 pCote2,Texture pRectangleTexture, Texture pRectangleBumpiness, float pKBumpiness) : base(1,pRectangleTexture,pRectangleBumpiness, pKBumpiness)
        {
            this.origine = pOrigine;
            this.coté1 = pCote1;
            this.coté2 = pCote2;
        }

        public V3 positionOrigine { get { return this.origine; } set { this.origine = value; } }
        public V3 positionCote1 { get { return this.coté1; } set { this.coté1 = value; } }
        public V3 positionCote2 { get { return this.coté2; } set { this.coté2 = value; } }

        public override Couleur getCouleurText(float u, float v)
        {
            Couleur cRectangle = TexturePack.LireCouleur(u, v);
            return cRectangle;
        }
        public override V3[] dessinVariable(float u, float v, V3 positionCamera3D)
        {
            V3 P3D = this.origine + u * this.coté1 + v * this.coté2;


            V3 n3D = new V3(0, 0, 60000);
            TextureBumpiness.Bump(u, v, out float dhdu, out float dhdv);



            V3 dmdu = this.coté1;
            V3 dmdv = this.coté2;

            V3 nBump3D = n3D + (modifKBumpiness * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));

            n3D = nBump3D;
            n3D.Normalize();
            Couleur couleurFinale = new Couleur(0, 0, 0);
            V3 d3D = positionCamera3D - P3D;
            d3D.Normalize();

            /*V3[] lesVariables = new[] { n3D, d3D, P3D };*/
            V3[] lesVariables = new[] { n3D, d3D};
            return lesVariables;//, d3D,point3D
        }

        public override bool raycast(V3 DirRayon, V3 pPosCamera, ref V3 pointIntersection, ref float distanceMinim, ref float[] vUetV)
        {
            V3 normale = (V3.prod_vect(this.coté1, this.coté2)) / (V3.prod_vect(this.coté1, this.coté2).Norm());
            float vT = (V3.prod_scal(this.origine - pPosCamera, normale)) / (V3.prod_scal(DirRayon, normale));

            pointIntersection = pPosCamera + vT * DirRayon;

            float u = V3.prod_scal(((V3.prod_vect(this.coté2, normale)) / (V3.prod_scal(V3.prod_vect(this.coté1, this.coté2), normale))), pointIntersection);
            float v = V3.prod_scal(((V3.prod_vect(this.coté1, normale)) / (V3.prod_scal(V3.prod_vect(this.coté2, this.coté1), normale))), pointIntersection);

            if (0 <= u && u <= 1 && 0 <= v && v <= 1)
            {
                if ((pPosCamera - pointIntersection).Norm() < distanceMinim)
                {
                    vUetV = new[] { u, v };
                    distanceMinim = (pPosCamera - pointIntersection).Norm();
                    return true;
                }
            }
            return false;
        }




    }
}