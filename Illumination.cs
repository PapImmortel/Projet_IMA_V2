using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projet_IMA
{
    static class Illumination
    {
        public static Couleur raycasting(V3 posCamera, V3 DirRayon, List<Item> listeObjetsScene, List<Lampe> listeLights)
        {
            V3 pointIntersection = new V3(0,0,0);
            Item itemCible = listeObjetsScene[0];
            float distanceMini = (float)Double.MaxValue;
            float[] valUV = new float[] {};
            DirRayon.Normalize();

            foreach (Item item in listeObjetsScene)
            {


                if (item.raycast(DirRayon, posCamera, ref pointIntersection, ref distanceMini, ref valUV))
                {
                    itemCible = item;
                }
            }

            if (distanceMini < (float)Double.MaxValue)
            {
                
                V3[] vecteursDessin = itemCible.calculVariableDessin(valUV[0], valUV[1], posCamera);
                Couleur couleurFinale = itemCible.éclairageObjet(listeLights, vecteursDessin[0], vecteursDessin[1], itemCible.SpecularPower, valUV[0], valUV[1]);
                return couleurFinale;
            }
            return Couleur.Black;
        }
    }
}