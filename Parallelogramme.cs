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
        private V3 longueurHorizontale;
        private V3 longueurVerticale;
        
        public Parallelogramme(V3 origine, V3 longueurVerticale, V3 longueurHorizontale, Texture texture, Texture bumpiness, float kBumpiness) : base(1, texture, bumpiness, kBumpiness)
        {
            this.origine = origine;
            this.longueurHorizontale = longueurHorizontale;
            this.longueurVerticale = longueurVerticale;
        }

        public override Couleur getCouleurTexture(float u, float v)
        {
            return TexturePack.LireCouleur(u, v);
        }

        public override V3[] calculVariableDessin(float u, float v, V3 positionCamera3D)
        {
            V3 p3D = this.origine + u * this.longueurHorizontale + v * this.longueurVerticale;
            V3 n3D = new V3(0, 0, 60000);
            TextureBumpiness.Bump(u, v, out float dhdu, out float dhdv);

            V3 dmdu = this.longueurHorizontale;
            V3 dmdv = this.longueurVerticale;
            V3 nBump3D = n3D + (Bumpiness * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));

            n3D = nBump3D;
            n3D.Normalize();

            Couleur couleurFinale = new Couleur(0, 0, 0);
            V3 d3D = positionCamera3D - p3D;
            d3D.Normalize();

            return new[] { n3D, d3D};
        }

        public override bool raycast(V3 dirRayon, V3 posCamera, ref V3 pointIntersection, ref float distanceMinim, ref float[] valUV)
        {
            V3 normale = (V3.prod_vect(this.longueurHorizontale, this.longueurVerticale)) / (V3.prod_vect(this.longueurHorizontale, this.longueurVerticale).Norm());
            float vT = (V3.prod_scal(this.origine - posCamera, normale)) / (V3.prod_scal(dirRayon, normale));

            V3 pointContact = posCamera + vT * dirRayon;

            float u = V3.prod_scal(((V3.prod_vect(this.longueurVerticale, normale)) / (V3.prod_scal(V3.prod_vect(this.longueurHorizontale, this.longueurVerticale), normale))),  pointContact- this.origine);
            float v = V3.prod_scal(((V3.prod_vect(this.longueurHorizontale, normale)) / (V3.prod_scal(V3.prod_vect(this.longueurVerticale, this.longueurHorizontale), normale))), pointContact- this.origine );

            if (0 <= u && u <= 1 && 0 <= v && v <= 1)
            {
                if ((posCamera - pointContact).Norm() < distanceMinim)
                {
                    valUV = new[] { u, v };
                    distanceMinim = (posCamera - pointContact).Norm();
                    pointIntersection = pointContact;

                    return true;
                }
            }
            return false;
        }




    }
}