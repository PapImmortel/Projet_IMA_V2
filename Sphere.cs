using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_IMA
{
    class Sphere : Item
    {
        private V3 centreSphere;
        private float rayon;

        public Sphere(V3 centreSphere, Texture sphereTexture, Texture sphereBumpiness, float rayon, float kBumpiness) : base(0, sphereTexture, sphereBumpiness, kBumpiness)
        {
            this.centreSphere = centreSphere;
            this.rayon = rayon;
        }

        public override Couleur getCouleurTexture(float u,float v)
        {
            float uNorm = u / (2 * IMA.PI);
            float vNorm = (v / IMA.PI) + 0.5f;
            return TexturePack.LireCouleur(uNorm, vNorm);
        }

        public override V3[] calculVariableDessin(float u, float v, V3 positionCamera3D)
        {
            float nX3D = IMA.Cosf(v) * IMA.Cosf(u);
            float nY3D = IMA.Cosf(v) * IMA.Sinf(u);
            float nZ3D = IMA.Sinf(v);
            float x3D = this.rayon * nX3D + this.centreSphere.x; 
            float y3D = this.rayon * nY3D + this.centreSphere.y;
            float z3D = this.rayon * nZ3D + this.centreSphere.z;
            V3 point3D = new V3(x3D, y3D, z3D);
            V3 n3D = new V3(nX3D, nY3D, nZ3D);

            // projection orthographique => repère écran
            float uNorm = u / (2 * IMA.PI);
            float vNorm = (v / IMA.PI) + 0.5f;

            // bumpmapping
            TextureBumpiness.Bump(uNorm, vNorm, out float dhdu, out float dhdv);

            V3 dmdu = new V3((IMA.Cosf(vNorm) * (-IMA.Sinf(uNorm))), IMA.Cosf(uNorm) * IMA.Cosf(vNorm), 0);
            V3 dmdv = new V3((-IMA.Sinf(vNorm) * IMA.Cosf(uNorm)), (-IMA.Sinf(vNorm) * IMA.Sinf(uNorm)), IMA.Cosf(vNorm));

            V3 nBump3D = n3D + (Bumpiness * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));
            n3D = nBump3D;
            n3D.Normalize();

            V3 d3D = positionCamera3D - point3D;
            d3D.Normalize();

            return new[] { n3D, d3D};
        }

        public override bool raycast(V3 dirRayon, V3 posCamera, ref V3 pointIntersection, ref float distanceMinim, ref float[] valUV)
        {
            float A = dirRayon * dirRayon;
            float B = 2 * dirRayon * (posCamera - this.centreSphere);
            float D = posCamera * posCamera - 2 * posCamera * this.centreSphere + this.centreSphere * this.centreSphere - this.rayon * this.rayon;
            float racineDelta = (float)Math.Sqrt(B * B - 4 * A * D);

            float vT1 = (-B - racineDelta) / (2 * A);
            float vT2 = (-B + racineDelta) / (2 * A);

            if (vT1 > 0 && vT2>0)
            {
                pointIntersection = posCamera + vT1 * dirRayon;
                if ((posCamera - pointIntersection).Norm() < distanceMinim)
                {
                    IMA.Invert_Coord_Spherique(pointIntersection, this.centreSphere, this.rayon, out float u, out float v);
                    valUV = new[] { u, v };
                    distanceMinim = (posCamera - pointIntersection).Norm();
                    return true;
                }
            }
            else if (vT2 > 0)
            {
                pointIntersection = posCamera + vT2 * dirRayon;
                if ((posCamera - pointIntersection).Norm() < distanceMinim)
                {
                    IMA.Invert_Coord_Spherique(pointIntersection, this.centreSphere, this.rayon, out float u, out float v);
                    valUV = new[] { u, v };
                    distanceMinim = (posCamera - pointIntersection).Norm();
                    return true;
                }
            }
            return false;
        }
    }
}