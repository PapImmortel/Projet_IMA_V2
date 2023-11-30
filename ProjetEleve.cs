using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {

            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Sphère en 3D
            /// 
            //////////////////////////////////////////////////////////////////////////

            SceneProjet notreScene = new SceneProjet(new V3(BitmapEcran.GetWidth() / 2, -BitmapEcran.GetWidth(), BitmapEcran.GetHeight() / 2));
            

            Lampe lampePrincipaleFluxDir3D = new Lampe(0.7f, new V3(1, -1, 1) / IMA.Sqrtf(3));
            Lampe lFill3D = new Lampe(0.3f, new V3(-1, -1, 1) / IMA.Sqrtf(3));

            notreScene.addListLight(lampePrincipaleFluxDir3D);
            notreScene.addListLight(lFill3D);
            
            int kSpecularPower = 100;
            float kBumpinessSphere = 0.02f;
            float pas = 0.005f;
            



            //texture
            V3 CentreSphere = new V3(300, 200, 300);
            float vRayon = 150;
            Texture sphereTexture = new Texture("gold.jpg");
            Texture sphereBumpiness = new Texture("bump38.jpg");
            Sphere notreSphere = new Sphere(CentreSphere,sphereTexture, sphereBumpiness,vRayon);
            for (float u = 0 ; u < 2 * IMA.PI; u += pas)  // echantillonage fnt paramétrique
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {


                    
                    V3[] nosVariables = notreSphere.dessinVariable(u,v,kBumpinessSphere,notreScene.modifPositionCamera);
*/
                    Couleur couleurFinale = Illumination.éclairageObjet(notreScene.getListLampe(),notreSphere.getCouleurText(u,v), nosVariables[0], nosVariables[1], kSpecularPower);
                    

                    int x_ecran = (int)nosVariables[2].x;
                    int y_ecran = (int)nosVariables[2].z;

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurFinale + notreScene.modifCouleurAmbiante);
                }

            }


            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Rectangle 3D  + exemple texture
            /// 
            //////////////////////////////////////////////////////////////////////////


            int kSpecularPowerRect = 100;
            float kBumpinessRect = 0.008f;
             

            V3 Origine = new V3(500, 200, 300);
            V3 Coté1 = new V3(300, 000, 000);
            V3 Coté2 = new V3(000, 200, 000);
            //texture

            Texture rectangleTexture = new Texture("gold.jpg");
            Texture rectangleBumpiness = new Texture("bump38.jpg");

            Pavé notreRectangle = new Pavé(Origine,Coté1,Coté2,rectangleTexture,rectangleBumpiness);

            pas = 0.002f;
            for (float u = 0; u < 1; u += pas)  // echantillonage fnt paramétrique
            {
                for (float v = 0; v < 1; v += pas)
                {
                   
                    V3[] nosVariables = notreRectangle.dessinVariable(u, v, kBumpinessRect, notreScene.modifPositionCamera);

                    Couleur couleurFinale = Illumination.éclairageObjet(notreScene.getListLampe(), notreSphere.getCouleurText(u, v), nosVariables[0], nosVariables[1], kSpecularPowerRect);

                    int x_ecran = (int)(nosVariables[2].x);
                    int y_ecran = (int)(nosVariables[2].y);
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurFinale + notreScene.modifCouleurAmbiante);


                }

            }
            // Gestion des textures
            // Texture T1 = new Texture("brick01.jpg");
            // Couleur c = T1.LireCouleur(u, v);

        }
    }
}
