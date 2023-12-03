using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_IMA
{
    static class Illumination
    {

        public static Couleur �clairageObjet(List<Lampe> listLight, Couleur couleurObjet, V3 n3D,V3 d3D, int kSpecularPower)
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
                    couleurFinale += lampe.getCouleur() * lampe.Puissance * (float)Math.Pow(prodScalRD, kSpecularPower);//ajout sp�culaire
                }

            }
            return couleurFinale;
        }
        public static Couleur raycasting(V3 pPosCamera, V3 DirRayon, List<Item> ListObjetsScene, List<Lampe> pListLights)
        {
            V3 pointIntersection=new V3(0,0,0);
            Item notreItem=ListObjetsScene[0];
            float distanceMinim = (float)Double.MaxValue;
            float[] vUetV = new float[] {};
            DirRayon.Normalize();
            foreach (Item item in ListObjetsScene)
            {

                /*(R0 + t * Rd - C)� = r�

<=>

(R0 + t * Rd - C) * (R0 + t * Rd - C) = r�

<=> (R0� +R0 * t * Rd - R0 * C)+(t * Rd * R0 + t�*Rd�-t * Rd * C)-R0 * C - t * Rd * C + C� = r�

<=> (R0�  +2 * R0 * Rd * t - 2 * R0 * C + Rd�*t� -2 * Rd * C * t + C� = r�
r
<=> Rd�*t� +2 * Rd * (R0 - C) * t - 2 * R0 * C + R0�+C�= r�

A = Rd� 
B = 2 * Rd * (R0 - C)
D = (R0 - C)�-r�


B� = 4 * Rd�*(R0�-2 * R0 * C + C�)

4 * A * D = 4 * Rd�*(R0�+C�-2 * R0 * C - r�)

B - 4AD = 4 * Rd�*(r�)

Donc G = 4 * Rd�*r�*/
                if(item.raycast(DirRayon,pPosCamera,ref pointIntersection, ref distanceMinim, ref vUetV))
                {
                    notreItem = item;
                }
            }
            if(distanceMinim < (float)Double.MaxValue)
            {
                V3[] nosVariables = notreItem.dessinVariable(vUetV[0], vUetV[1], pPosCamera);

                Couleur couleurFinale = Illumination.�clairageObjet(pListLights, notreItem.getCouleurText(vUetV[0], vUetV[1]), nosVariables[0], nosVariables[1], notreItem.modifKSpecularPower);
                return couleurFinale;
            }

            return Couleur.Black;

        }
    }
}