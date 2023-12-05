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

            SceneProjet scene = new SceneProjet(new V3(
                BitmapEcran.GetWidth() / 2,
                -BitmapEcran.GetWidth(),
                BitmapEcran.GetHeight() / 2
            ));

            Parallelogramme rectangle;
            Lampe lampePrincipaleFluxDir3D = new Lampe(0.7f, new V3(1, -1, 1) / IMA.Sqrtf(3));
            Lampe lFill3D = new Lampe(0.3f, new V3(-1, -1, -1) / IMA.Sqrtf(3));

            scene.addLampe(lampePrincipaleFluxDir3D);
            scene.addLampe(lFill3D);

            float kBumpinessRect = 0.008f;
            float kBumpinessSphere = 0.02f;

            // MUR DU BAS
            V3 Origine = new V3(0, 0, 0);
            V3 longueurHorizontale = new V3(BitmapEcran.GetWidth(), 0, 0);
            V3 longueurVerticale = new V3(0, BitmapEcran.GetWidth(), 0);

            // textures
            Texture rectangleTexture = new Texture("gold.jpg");
            Texture rectangleBumpiness = new Texture("bump38.jpg");

            rectangle = new Parallelogramme(Origine, longueurHorizontale, longueurVerticale, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            scene.addListeItem(rectangle);


            // MUR DU HAUT
            Origine = new V3(0, BitmapEcran.GetWidth(), BitmapEcran.GetHeight());
            longueurHorizontale = new V3(BitmapEcran.GetWidth(), 0, 0);
            longueurVerticale = new V3(0, -BitmapEcran.GetWidth(), 0);

            // textures
            rectangleTexture = new Texture("fibre.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            rectangle = new Parallelogramme(Origine, longueurHorizontale, longueurVerticale, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            scene.addListeItem(rectangle);


            // MUR DU CENTRE
            Origine = new V3(0, BitmapEcran.GetWidth(), 0);
            longueurHorizontale = new V3(BitmapEcran.GetWidth(), 0, 0);
            longueurVerticale = new V3(0, 0, BitmapEcran.GetHeight());

            // textures
            rectangleTexture = new Texture("lead.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            rectangle = new Parallelogramme(Origine, longueurHorizontale, longueurVerticale, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            scene.addListeItem(rectangle);


            //MUR DE GAUCHE
            Origine = new V3(0, 0, 0);
            longueurHorizontale = new V3(0, BitmapEcran.GetWidth(), 0);
            longueurVerticale = new V3(0, 0, BitmapEcran.GetHeight());

            // textures
            rectangleTexture = new Texture("rock.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            rectangle = new Parallelogramme(Origine, longueurHorizontale, longueurVerticale, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            scene.addListeItem(rectangle);


            //MUR DE DROITE
            Origine = new V3(BitmapEcran.GetWidth(), BitmapEcran.GetWidth(), 0);
            longueurHorizontale = new V3(0, -BitmapEcran.GetWidth(), 0);
            longueurVerticale = new V3(0, 0, BitmapEcran.GetHeight());

            // textures
            rectangleTexture = new Texture("wood.jpg");
            rectangleBumpiness = new Texture("bump38.jpg");

            rectangle = new Parallelogramme(Origine, longueurHorizontale, longueurVerticale, rectangleTexture, rectangleBumpiness, kBumpinessRect);
            scene.addListeItem(rectangle);


            // SPHERE CENTRALE
            V3 CentreSphere = new V3(BitmapEcran.GetWidth() / 2, 200, BitmapEcran.GetHeight() / 2);
            float vRayon = 150;

            // textures
            Texture sphereTexture = new Texture("stone2.jpg");
            Texture sphereBumpiness = new Texture("bump38.jpg");

            Sphere notreSphere = new Sphere(CentreSphere, sphereTexture, sphereBumpiness, vRayon, kBumpinessSphere);
            scene.addListeItem(notreSphere);


            // SPHERE DROITE
            CentreSphere = new V3((BitmapEcran.GetWidth() / 2) + 100, 100, (BitmapEcran.GetHeight() / 2) - 100);
            vRayon = 70;

            // textures
            sphereTexture = new Texture("brick01.jpg");
            sphereBumpiness = new Texture("bump38.jpg");
            notreSphere = new Sphere(CentreSphere, sphereTexture, sphereBumpiness, vRayon, kBumpinessSphere);

            scene.addListeItem(notreSphere);


            // SPHERE GAUCHE
            CentreSphere = new V3(120, 300, 120);
            vRayon = 60;

            // textures
            sphereTexture = new Texture("brick01.jpg");
            sphereBumpiness = new Texture("bump38.jpg");

            notreSphere = new Sphere(CentreSphere, sphereTexture, sphereBumpiness, vRayon, kBumpinessSphere);
            scene.addListeItem(notreSphere);


            // Raycasting
            for (int x_ecran = 0; x_ecran <= BitmapEcran.GetWidth(); x_ecran++)
            {
                for (int y_ecran = 0; y_ecran <= BitmapEcran.GetHeight(); y_ecran++)
                {
                    V3 PosPixScene = new V3(x_ecran, 0, y_ecran);
                    V3 DirRayon = PosPixScene - scene.PositionCamera;
                    Couleur couleurEcran = Illumination.raycasting(scene.PositionCamera, DirRayon, scene.getListItem(),scene.getListeLampe()); //ListObjetScene a definir
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurEcran + scene.CouleurAmbiante);
                }
            }
        }
    }
}
