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

                /*
                (R0 + t * Rd - C)� = r�

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

                Donc G = 4 * Rd�*r�
                */

                if (item.raycast(DirRayon, posCamera, ref pointIntersection, ref distanceMini, ref valUV)) itemCible = item;
            }

            if (distanceMini >= (float)Double.MaxValue) return Couleur.Black;

            V3[] vecteursDessin = itemCible.calculVariableDessin(valUV[0], valUV[1], posCamera);
            Couleur couleurFinale = itemCible.�clairageObjet(listeLights, vecteursDessin[0], vecteursDessin[1], itemCible.SpecularPower, valUV[0], valUV[1]);
            return couleurFinale;
        }
    }
}