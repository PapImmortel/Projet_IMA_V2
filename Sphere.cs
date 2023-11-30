using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_IMA
{
    class Sphere
    {
        private V3 centreSphere;
        private Texture sphereTexture;
        private Texture sphereBumpiness;
        private float aRayon;
        public Sphere(V3 pCentreSphere, Texture pSphereTexture, Texture pSphereBumpiness,float pRayon)
        {
            this.centreSphere = pCentreSphere;
            this.sphereTexture = pSphereTexture;
            this.sphereBumpiness = pSphereBumpiness;
            this.aRayon = pRayon;
        }
        public Texture TexturePack { get { return sphereTexture; } set { sphereTexture = value; } }
        public Texture Bumpiness { get { return sphereBumpiness; } set { sphereBumpiness = value; } }
        public V3 PositionCentre { get { return centreSphere; } set { centreSphere = value; } }

        public Couleur getCouleurText(float u,float v)
        {
            float uNorm = u / (2 * IMA.PI);
            float vNorm = (v / IMA.PI) + 0.5f;
            Couleur CSphere = this.sphereTexture.LireCouleur(uNorm, vNorm);
            return CSphere;
        }
        public V3[] dessinVariable(float u, float v,float kBumpiness, V3 positionCamera3D)
        {
            float nX3D = IMA.Cosf(v) * IMA.Cosf(u);
            float nY3D = IMA.Cosf(v) * IMA.Sinf(u);
            float nZ3D = IMA.Sinf(v);
            float x3D = this.aRayon * nX3D + centreSphere.x; 
            float y3D = this.aRayon * nY3D + centreSphere.y;
            float z3D = this.aRayon * nZ3D + centreSphere.z;
            V3 point3D = new V3(x3D, y3D, z3D);
            V3 n3D = new V3(nX3D, nY3D, nZ3D);

            // projection orthographique => repère écran

            float uNorm = u / (2 * IMA.PI);
            float vNorm = (v / IMA.PI) + 0.5f;


            //bumpmapping
            sphereBumpiness.Bump(uNorm, vNorm, out float dhdu, out float dhdv);
            //T2 = dhdu*(N^DM/Dv)  T3=dhdv*(DM/Du^N)

            V3 dmdu = new V3((IMA.Cosf(vNorm) * (-IMA.Sinf(uNorm))), IMA.Cosf(uNorm) * IMA.Cosf(vNorm), 0);
            V3 dmdv = new V3((-IMA.Sinf(vNorm) * IMA.Cosf(uNorm)), (-IMA.Sinf(vNorm) * IMA.Sinf(uNorm)), IMA.Cosf(vNorm));

            V3 nBump3D = n3D + (kBumpiness * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));
            n3D = nBump3D;
            n3D.Normalize();
            Couleur couleurFinale = new Couleur(0, 0, 0);
            V3 d3D = positionCamera3D - point3D;
            d3D.Normalize();

            V3 [] lesVariables =new [] { n3D,d3D,point3D};
            return lesVariables;//, d3D,point3D
        }



            

        
	}
}