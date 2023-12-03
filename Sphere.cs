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
        private float aRayon;
        public Sphere(V3 pCentreSphere, Texture pSphereTexture, Texture pSphereBumpiness,float pRayon, float pKBumpiness) : base(0,pSphereTexture,pSphereBumpiness, pKBumpiness)
        {
            this.centreSphere = pCentreSphere;
            this.aRayon = pRayon;
        }

        public V3 PositionCentre { get { return this.centreSphere; } set { this.centreSphere = value; } }
        public float TailleRayon { get { return this.aRayon; } set { this.aRayon = value; } }

        public override Couleur getCouleurText(float u,float v)
        {
            float uNorm = u / (2 * IMA.PI);
            float vNorm = (v / IMA.PI) + 0.5f;
            Couleur CSphere = TexturePack.LireCouleur(uNorm, vNorm);
            return CSphere;
        }
        public override V3[] dessinVariable(float u, float v, V3 positionCamera3D)
        {
            float nX3D = IMA.Cosf(v) * IMA.Cosf(u);
            float nY3D = IMA.Cosf(v) * IMA.Sinf(u);
            float nZ3D = IMA.Sinf(v);
            float x3D = this.aRayon * nX3D + this.centreSphere.x; 
            float y3D = this.aRayon * nY3D + this.centreSphere.y;
            float z3D = this.aRayon * nZ3D + this.centreSphere.z;
            V3 point3D = new V3(x3D, y3D, z3D);
            V3 n3D = new V3(nX3D, nY3D, nZ3D);

            // projection orthographique => repère écran

            float uNorm = u / (2 * IMA.PI);
            float vNorm = (v / IMA.PI) + 0.5f;


            //bumpmapping
            TextureBumpiness.Bump(uNorm, vNorm, out float dhdu, out float dhdv);
            //T2 = dhdu*(N^DM/Dv)  T3=dhdv*(DM/Du^N)

            V3 dmdu = new V3((IMA.Cosf(vNorm) * (-IMA.Sinf(uNorm))), IMA.Cosf(uNorm) * IMA.Cosf(vNorm), 0);
            V3 dmdv = new V3((-IMA.Sinf(vNorm) * IMA.Cosf(uNorm)), (-IMA.Sinf(vNorm) * IMA.Sinf(uNorm)), IMA.Cosf(vNorm));

            V3 nBump3D = n3D + (modifKBumpiness * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));
            n3D = nBump3D;
            n3D.Normalize();
            Couleur couleurFinale = new Couleur(0, 0, 0);
            V3 d3D = positionCamera3D - point3D;
            d3D.Normalize();

            //V3 [] lesVariables =new [] { n3D,d3D,point3D};
            V3[] lesVariables = new[] { n3D, d3D};
            return lesVariables;//, d3D,point3D
        }
        public override bool raycast(V3 DirRayon,V3 pPosCamera,ref V3 pointIntersection,ref float distanceMinim,ref float[] vUetV)
        {
            float A = DirRayon * DirRayon;
            float B = 2 * DirRayon * (pPosCamera - this.centreSphere);
            float racineDelta = (float)Math.Sqrt(4 * DirRayon * DirRayon * this.aRayon * this.aRayon);
            float vT1 = (-B - racineDelta) / (2 * A);
            float vT2 = (-B + racineDelta) / (2 * A);

            if (vT1 > 0)
            {

                pointIntersection = pPosCamera + vT1 * DirRayon;
                if ((pPosCamera - pointIntersection).Norm() < distanceMinim)
                {
                    IMA.Invert_Coord_Spherique(pointIntersection, this.centreSphere, this.aRayon, out float u, out float v);
                    vUetV = new[] { u, v };
                    distanceMinim = (pPosCamera - pointIntersection).Norm();
                    return true;
                }


            }
            else if (vT2 > 0)
            {
                pointIntersection = pPosCamera + vT2 * DirRayon;
                if ((pPosCamera - pointIntersection).Norm() < distanceMinim)
                {
                    IMA.Invert_Coord_Spherique(pointIntersection, this.centreSphere, this.aRayon, out float u, out float v);
                    vUetV = new[] { u, v };
                    distanceMinim = (pPosCamera - pointIntersection).Norm();
                    return true;
                }

            }
            return false;
        }
    }
}