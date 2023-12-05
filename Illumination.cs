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
                (R0 + t * Rd - C)² = r²

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

                Donc G = 4 * Rd²*r²
                */

                if (item.raycast(DirRayon, posCamera, ref pointIntersection, ref distanceMini, ref valUV)) itemCible = item;
            }

            if (distanceMini >= (float)Double.MaxValue) return Couleur.Black;

            V3[] vecteursDessin = itemCible.calculVariableDessin(valUV[0], valUV[1], posCamera);
            Couleur couleurFinale = itemCible.éclairageObjet(listeLights, vecteursDessin[0], vecteursDessin[1], itemCible.SpecularPower, valUV[0], valUV[1]);
            return couleurFinale;
        }
    }
}