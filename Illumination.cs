using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_IMA
{
    static class Illumination
    {

        public static Couleur éclairageObjet(List<Lampe> listLight, Couleur couleurObjet, V3 n3D,V3 d3D, int kSpecularPower)
        {
            
            Couleur couleurFinale = new Couleur(0, 0, 0);
            foreach (Lampe lampe in listLight)
            {
                // diffus
                float prodScalNL = V3.prod_scal(n3D, lampe.Direction);
                float cosAlpha = prodScalNL / (lampe.Direction.Norm() * n3D.Norm());
                if (prodScalNL >= 0)
                {
                    couleurFinale += (couleurObjet *lampe.getCouleur()*lampe.Puissance) * prodScalNL; // ajout diffus
                }

                //spec
                V3 r3D = 2 * cosAlpha * n3D - lampe.Direction;
                float prodScalRD = V3.prod_scal(r3D, d3D);

                if (prodScalNL >= 0)
                {
                    couleurFinale += lampe.getCouleur() * lampe.Puissance * (float)Math.Pow(prodScalRD, kSpecularPower);//ajout spéculaire
                }

            }
            return couleurFinale;
        }
        public static Couleur raycasting(V3 pPosCamera, V3 DirRayon, List<Item> ListObjetsScene, List<Lampe> pListLights)
        {
            V3 pointIntersection;
            Item notreItem=ListObjetsScene[0];
            float distanceMinim = (float)Double.MaxValue;
            float[] vUetV = new float[] {};

            foreach (Item item in ListObjetsScene)
            {

                /*(R0 + t * Rd - C)² = r²

<=>

(R0 + t * Rd - C) * (R0 + t * Rd - C) = r²

<=> (R0² +R0 * t * Rd - R0 * C)+(t * Rd * R0 + t²*Rd²-t * Rd * C)-R0 * C - t * Rd * C + C² = r²

<=> (R0²  +2 * R0 * Rd * t - 2 * R0 * C + Rd²*t² -2 * Rd * C * t + C² = r²
r
<=> Rd²*t² +2 * Rd * (R0 - C) * t - 2 * R0 * C + R0²+C²= r²

A = Rd² 
B = 2 * Rd * (R0 - C)
D = (R0 - C)²-r²


B² = 4 * Rd²*(R0²-2 * R0 * C + C²)

4 * A * D = 4 * Rd²*(R0²+C²-2 * R0 * C - r²)

B - 4AD = 4 * Rd²*(r²)

Donc G = 4 * Rd²*r²*/
                if (item.getType == 0)//si c'est une sphère
                {
                    Sphere laSphere = (Sphere)item;
                    float A = DirRayon * DirRayon;
                    float B = 2 * DirRayon * (pPosCamera - laSphere.PositionCentre);
                    float racineDelta = (float)Math.Sqrt(4 * DirRayon*DirRayon * laSphere.TailleRayon* laSphere.TailleRayon);
                    float vT1 = (-B - racineDelta) / (2 * A);
                    float vT2 = (-B + racineDelta) / (2 * A);

                    if (vT1 > 0)
                    {

                        pointIntersection= pPosCamera + vT1 * DirRayon;
                        if((pPosCamera-pointIntersection).Norm() < distanceMinim)
                        {
                            IMA.Invert_Coord_Spherique(pointIntersection, laSphere.PositionCentre, laSphere.TailleRayon, out float u, out float v);
                            vUetV = new[] { u, v };
                            notreItem = laSphere;
                            distanceMinim= (pPosCamera - pointIntersection).Norm();
                        }
                        

                    }
                    else if (vT2 > 0)
                    {
                        pointIntersection = pPosCamera + vT2 * DirRayon;
                        if ((pPosCamera - pointIntersection).Norm() < distanceMinim)
                        {
                            IMA.Invert_Coord_Spherique(pointIntersection, laSphere.PositionCentre, laSphere.TailleRayon, out float u, out float v);
                            vUetV = new[] { u, v };
                            notreItem = laSphere;
                            distanceMinim = (pPosCamera - pointIntersection).Norm();
                        }

                    }
                }
                else if (item.getType == 1)//Parallélogramme
                {
                    Parallelogramme leRect = (Parallelogramme)item;
                    V3 normale = (V3.prod_vect(leRect.positionCote1, leRect.positionCote2)) / (V3.prod_vect(leRect.positionCote1, leRect.positionCote2).Norm());
                    float vT = (V3.prod_scal(leRect.positionOrigine - pPosCamera, normale)) / (V3.prod_scal(DirRayon, normale));

                    pointIntersection = pPosCamera + vT * DirRayon;

                    float u = V3.prod_scal(((V3.prod_vect(leRect.positionCote2, normale))/(V3.prod_scal(V3.prod_vect(leRect.positionCote1, leRect.positionCote2),normale))),pointIntersection);
                    float v = V3.prod_scal(((V3.prod_vect(leRect.positionCote1, normale)) / (V3.prod_scal(V3.prod_vect(leRect.positionCote2, leRect.positionCote1), normale))), pointIntersection);

                    if (0 <= u && u <= 1 && 0 <= v && v <= 1)
                    {
                        if ((pPosCamera - pointIntersection).Norm() < distanceMinim)
                        {
                            vUetV = new[] { u, v };
                            notreItem = leRect;
                            distanceMinim = (pPosCamera - pointIntersection).Norm();
                        }
                    }
                }

            }
            if(distanceMinim < (float)Double.MaxValue)
            {
                V3[] nosVariables = notreItem.dessinVariable(vUetV[0], vUetV[1], pPosCamera);

                Couleur couleurFinale = Illumination.éclairageObjet(pListLights, notreItem.getCouleurText(vUetV[0], vUetV[1]), nosVariables[0], nosVariables[1], notreItem.modifKSpecularPower);
                return couleurFinale;
            }

            return Couleur.Black;

        }
    }
}