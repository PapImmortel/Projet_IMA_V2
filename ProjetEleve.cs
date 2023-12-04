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




            // MUR DU BAS
            float kBumpinessRect = 0.008f;
            V3 Origine = new V3(0, 0, 0);
            V3 Coté1 = new V3(BitmapEcran.GetWidth(), 0, 0);
            V3 Coté2 = new V3(000, BitmapEcran.GetWidth(), 100);
            //texture

            Texture rectangleTexture = new Texture("gold.jpg");
            Texture rectangleBumpiness = new Texture("bump38.jpg");

            Parallelogramme notreRectangle = new Parallelogramme(Origine, Coté1, Coté2, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            notreScene.addListItem(notreRectangle);
            // MUR DU HAUT
            kBumpinessRect = 0.008f;
            Origine = new V3(0, BitmapEcran.GetWidth(), BitmapEcran.GetHeight() - 100);
            Coté1 = new V3(BitmapEcran.GetWidth(), 0, 0);
            Coté2 = new V3(0, -BitmapEcran.GetWidth(), 100);
            //texture

            rectangleTexture = new Texture("fibre.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            notreRectangle = new Parallelogramme(Origine, Coté1, Coté2, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            notreScene.addListItem(notreRectangle);

            // MUR DU CENTRE
            kBumpinessRect = 0.008f;
            Origine = new V3(100, BitmapEcran.GetWidth(), 100);
            Coté1 = new V3(BitmapEcran.GetWidth() - 200, 0, 0);
            Coté2 = new V3(0, 0, BitmapEcran.GetHeight() - 200);
            //texture

            rectangleTexture = new Texture("lead.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            notreRectangle = new Parallelogramme(Origine, Coté1, Coté2, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            notreScene.addListItem(notreRectangle);
            //MUR DE GAUCHE
            kBumpinessRect = 0.008f;
            Origine = new V3(0, 0, 0);
            Coté1 = new V3(100, BitmapEcran.GetWidth(), 0);
            Coté2 = new V3(0, 0, BitmapEcran.GetHeight());
            //texture

            rectangleTexture = new Texture("rock.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            notreRectangle = new Parallelogramme(Origine, Coté1, Coté2, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            notreScene.addListItem(notreRectangle);

            //MUR DE DROITE
            kBumpinessRect = 0.008f;
            Origine = new V3(BitmapEcran.GetWidth()-100, BitmapEcran.GetWidth(), 0);
            Coté1 = new V3(100, -BitmapEcran.GetWidth(), 0);
            Coté2 = new V3(0, 0, BitmapEcran.GetHeight());
            //texture

            rectangleTexture = new Texture("wood.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            notreRectangle = new Parallelogramme(Origine, Coté1, Coté2, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            notreScene.addListItem(notreRectangle);


            //CERCLE CENTRALE
            //texture
            float kBumpinessSphere = 0.02f;
            V3 CentreSphere = new V3(BitmapEcran.GetWidth()/2, 500, BitmapEcran.GetHeight()/2);
            float vRayon = 150;
            Texture sphereTexture = new Texture("stone2.jpg");
            Texture sphereBumpiness = new Texture("bump38.jpg");
            Sphere notreSphere = new Sphere(CentreSphere, sphereTexture, sphereBumpiness, vRayon, kBumpinessSphere);

            notreScene.addListItem(notreSphere);

            //petit cercle droite
            kBumpinessSphere = 0.02f;
            CentreSphere = new V3((BitmapEcran.GetWidth() / 2)+100, 300, (BitmapEcran.GetHeight() / 2)-100);
            vRayon = 70;
            sphereTexture = new Texture("brick01.jpg");
            sphereBumpiness = new Texture("bump38.jpg");
            notreSphere = new Sphere(CentreSphere, sphereTexture, sphereBumpiness, vRayon, kBumpinessSphere);

            notreScene.addListItem(notreSphere);
             
            //petit cercle gauche
            kBumpinessSphere = 0.02f;
            CentreSphere = new V3(120, 700, 120);
            vRayon = 60;
            sphereTexture = new Texture("brick01.jpg");
            sphereBumpiness = new Texture("bump38.jpg");
            notreSphere = new Sphere(CentreSphere, sphereTexture, sphereBumpiness, vRayon, kBumpinessSphere);

            notreScene.addListItem(notreSphere);
            //raycasting

            for (int x_ecran = 0; x_ecran <= BitmapEcran.GetWidth(); x_ecran++)
            {
                for (int y_ecran = 0; y_ecran <= BitmapEcran.GetHeight(); y_ecran++)
                {
                    V3 PosPixScene = new V3(x_ecran, 0, y_ecran);
                    V3 DirRayon = PosPixScene - notreScene.modifPositionCamera;
                    Couleur couleurEcran = Illumination.raycasting(notreScene.modifPositionCamera, DirRayon, notreScene.getListItem(),notreScene.getListLampe()); //ListObjetScene a definir
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurEcran + notreScene.modifCouleurAmbiante);
                }
            }




            /*for (float u = 0 ; u < 2 * IMA.PI; u += pas)  // echantillonage fnt paramétrique
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {


                    
                    V3[] nosVariables = notreSphere.dessinVariable(u,v,notreScene.modifPositionCamera);

                    Couleur couleurFinale = Illumination.éclairageObjet(notreScene.getListLampe(),notreSphere.getCouleurText(u,v), nosVariables[0], nosVariables[1], kSpecularPower);
                    

                    int x_ecran = (int)nosVariables[2].x;
                    int y_ecran = (int)nosVariables[2].z;

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurFinale + notreScene.modifCouleurAmbiante);
                }

            }*/



            

            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Rectangle 3D  + exemple texture
            /// 
            //////////////////////////////////////////////////////////////////////////


            /*int kSpecularPowerRect = 100;
            float kBumpinessRect = 0.008f;
             

            V3 Origine = new V3(500, 200, 300);
            V3 Coté1 = new V3(300, 000, 000);
            V3 Coté2 = new V3(000, 200, 000);
            //texture

            Texture rectangleTexture = new Texture("gold.jpg");
            Texture rectangleBumpiness = new Texture("bump38.jpg");

            Parallelogramme notreRectangle = new Parallelogramme(Origine,Coté1,Coté2,rectangleTexture,rectangleBumpiness,kBumpinessRect);

            pas = 0.002f;
            for (float u = 0; u < 1; u += pas)  // echantillonage fnt paramétrique
            {
                for (float v = 0; v < 1; v += pas)
                {
                   
                    V3[] nosVariables = notreRectangle.dessinVariable(u, v, notreScene.modifPositionCamera);

                    Couleur couleurFinale = Illumination.éclairageObjet(notreScene.getListLampe(), notreSphere.getCouleurText(u, v), nosVariables[0], nosVariables[1], kSpecularPowerRect);

                    int x_ecran = (int)(nosVariables[2].x);
                    int y_ecran = (int)(nosVariables[2].y);
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurFinale + notreScene.modifCouleurAmbiante);


                }

            }*/
            // Gestion des textures
            // Texture T1 = new Texture("brick01.jpg");
            // Couleur c = T1.LireCouleur(u, v);

        }
    }
}
