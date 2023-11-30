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
    }
}